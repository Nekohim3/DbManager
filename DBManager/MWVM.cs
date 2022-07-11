using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;
using FastMember;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;

namespace DBManager
{
    public class MWVM : NotificationObject
    {
        public  DelegateCommand                   AddDatabaseCommand           { get; }
        public  DelegateCommand                   RemoveDatabaseCommand        { get; }
        public  DelegateCommand                   CreateBackupFromDbCommand    { get; }
        public  DelegateCommand                   RestoreSelectedBackupCommand { get; }
        public  DelegateCommand                   DeleteSelectedBackupCommand  { get; }
        public  DelegateCommand                   AddBackupFromFileCommand     { get; }
        public  DelegateCommand                   ClearDBCommand               { get; }
        public  DelegateCommand                   GrabSelectedDBCommand        { get; }
        public  DelegateCommand                   InsertIntoSelectedDBCommand  { get; }
        public  DelegateCommand                   ChangeCredCommand            { get; }
        private DataBase                          _selectedDatabase;
        private ObservableCollection<DataBase>    _databaseList;
        private BackupClass                       _selectedBackup;
        private ObservableCollection<BackupClass> _backupList;
        private Visibility                        _processVis;
        private string                            _processText;

        public ObservableCollection<DataBase> DatabaseList
        {
            get => _databaseList;
            set { _databaseList = value;  RaisePropertyChanged(() => DatabaseList);}
        }

        public DataBase SelectedDatabase
        {
            get => _selectedDatabase;
            set
            {
                _selectedDatabase = value;
                if (_selectedDatabase != null)
                {
                    BackupList = new ObservableCollection<BackupClass>(SQLService.GetBackups(_selectedDatabase));
                }

                RaisePropertyChanged(() => SelectedDatabase);
                RaiseCanExecChange();
            }
        }

        public ObservableCollection<BackupClass> BackupList
        {
            get => _backupList;
            set
            {
                _backupList = value;
                RaisePropertyChanged(() => BackupList);
                var usedDb = SQLService.GetUsedBackup(SelectedDatabase);
                foreach (var item in _backupList)
                    item.InUse = item.Name == usedDb;
            }
        }

        public BackupClass SelectedBackup
        {
            get => _selectedBackup;
            set
            {
                _selectedBackup = value;
                RaisePropertyChanged(() => SelectedBackup);
                RaiseCanExecChange();
            }
        }

        public Visibility ProcessVis
        {
            get => _processVis;
            set
            {
                _processVis = value;
                RaisePropertyChanged(() => ProcessVis);
            }
        }

        public string ProcessText
        {
            get => _processText;
            set
            {
                _processText = value;
                RaisePropertyChanged(() => ProcessText);
            }
        }

        public MWVM()
        {
            ProcessVis                   = Visibility.Hidden;
            AddDatabaseCommand           = new DelegateCommand(OnAddDatabase);
            RemoveDatabaseCommand        = new DelegateCommand(OnRemoveDatabase);
            CreateBackupFromDbCommand    = new DelegateCommand(OnCreateBackupFromDb,    () => SelectedDatabase != null);
            RestoreSelectedBackupCommand = new DelegateCommand(OnRestoreSelectedBackup, () => SelectedBackup   != null);
            AddBackupFromFileCommand     = new DelegateCommand(OnAddBackupFromFile,     () => SelectedDatabase != null);
            DeleteSelectedBackupCommand  = new DelegateCommand(OnRemoveBackup,          () => SelectedBackup   != null);
            ChangeCredCommand            = new DelegateCommand(ChangeCred);

            ClearDBCommand              = new DelegateCommand(OnClearDB);
            GrabSelectedDBCommand       = new DelegateCommand(OnGrabData);
            InsertIntoSelectedDBCommand = new DelegateCommand(OnInsertIntoDB);


            g.Settings                = Settings.Load() ?? new Settings();

            while (string.IsNullOrEmpty(g.Settings.DirForDbData))
            {
                var f  = new SelectDirForm();
                var vm = new SelectDirViewModel(f.Close);
                f.DataContext = vm;
                f.ShowDialog();

                if (vm.Cancel) continue;

                g.Settings.DirForDbData = vm.Dir;
                if(Directory.Exists(g.Settings.DirForDbData))
                    g.Settings.Save();
            }

            if(string.IsNullOrEmpty(g.Settings.Login) || g.Settings.Pass == null)
                ChangeCred();


            DatabaseList          = new ObservableCollection<DataBase>(g.Settings.DbList);
            SelectedDatabase      = DatabaseList.Count > 0 ? DatabaseList.FirstOrDefault() : null;
        }

        private void RaiseCanExecChange()
        {
            CreateBackupFromDbCommand.RaiseCanExecuteChanged();
            RestoreSelectedBackupCommand.RaiseCanExecuteChanged();
            AddBackupFromFileCommand.RaiseCanExecuteChanged();
            DeleteSelectedBackupCommand.RaiseCanExecuteChanged();
        }

        private void OnAddDatabase()
        {
            var f  = new DBAddForm();
            var vm = new DBAddViewModel(f.Close);
            f.DataContext = vm;
            f.ShowDialog();

            if (!vm.Cancel)
            {
                g.Settings.AddDb(new DataBase() { Instance = vm.SelectedInstance, Name = vm.SelectedDatabase });
                DatabaseList     = new ObservableCollection<DataBase>(g.Settings.DbList);
                SelectedDatabase = DatabaseList.Count > 0 ? DatabaseList.FirstOrDefault() : null;
            }
        }

        private void OnRemoveDatabase()
        {
            if (SelectedDatabase == null) return;
            
            if (MessageBox.Show("Точно удалить?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No) return;

            g.Settings.RemoveDb(SelectedDatabase);
            DatabaseList     = new ObservableCollection<DataBase>(g.Settings.DbList);
            SelectedDatabase = DatabaseList.Count > 0 ? DatabaseList.FirstOrDefault() : null;
        }

        private void OnCreateBackupFromDb()
        {
            var f  = new CreateBackupForm();
            var vm = new CreateBackupViewModel(f.Close, SelectedDatabase);
            f.DataContext = vm;
            f.ShowDialog();
            if (!vm.Cancel)
            {
                ProcessText = "Создание бэкапа, плис вэит...";
                ProcessVis  = Visibility.Visible;
                ThreadPool.QueueUserWorkItem(x =>
                                             {
                                                 Directory
                                                    .CreateDirectory($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}");
                                                 if(File.Exists($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak"))
                                                     File.Delete($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak");
                                                 var bak = new BackupClass() { CreateTime = DateTime.Now, Name = vm.Name, Desc = vm.Desc };
                                                 SQLService.BackupDatabase($"{g.CompName}\\{SelectedDatabase.Instance}", SelectedDatabase.Name,
                                                                           $"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak",
                                                                           this);
                                                 bak.Save($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\data.xml");
                                                 BackupList = new ObservableCollection<BackupClass>(SQLService.GetBackups(_selectedDatabase));
                                                 ProcessVis = Visibility.Hidden;
                                             });
            }
        }

        private void OnRestoreSelectedBackup()
        {
            ProcessText = "Восстановление бэкапа, плис вэит...";
            ProcessVis  = Visibility.Visible;
            ThreadPool.QueueUserWorkItem(x =>
                                         {
                                             SQLService.RestoreDatabase($"{g.CompName}\\{SelectedDatabase.Instance}", SelectedDatabase.Name,
                                                                        $"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{SelectedBackup.Name}\\{SelectedBackup.Name}.bak",
                                                                        $"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{SelectedBackup.Name}",
                                                                        this);

                                             ProcessVis = Visibility.Hidden;
                                         });
            var selected = SelectedDatabase;
            DatabaseList     = new ObservableCollection<DataBase>(g.Settings.DbList);
            SelectedDatabase = selected;

        }

        private void OnAddBackupFromFile()
        {
            var dialog = new CommonOpenFileDialog
                         {
                             IsFolderPicker = false,
                             Filters = { new CommonFileDialogFilter("DB BAK", "*.bak") }
                         };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (File.Exists(dialog.FileName))
                {
                    var f  = new CreateBackupForm();
                    var vm = new CreateBackupViewModel(f.Close, SelectedDatabase, true)
                             {
                                 Name     = Path.GetFileNameWithoutExtension(dialog.FileName)
                             };
                    f.DataContext = vm;
                    f.ShowDialog();
                    if (!vm.Cancel)
                    {
                        if (g.Settings.DirForDbData[0] != dialog.FileName[0])
                        {
                            //разные диски

                            ProcessText = "Копирование бэкапа, плис вэит...";
                            ProcessVis = Visibility.Visible;
                            ThreadPool.QueueUserWorkItem(x =>
                            {
                                Directory
                                   .CreateDirectory($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}");
                                if (
                                    File
                                       .Exists($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak"))
                                    File
                                       .Delete($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak");
                                var bak = new BackupClass()
                                { CreateTime = DateTime.Now, Name = vm.Name, Desc = vm.Desc };
                                File.Copy(dialog.FileName,
                                          $"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak");
                                bak.Save($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\data.xml");
                                BackupList =
                                    new ObservableCollection<BackupClass>(SQLService.GetBackups(_selectedDatabase));
                                ProcessVis = Visibility.Hidden;
                            });

                        }
                        else
                        {
                            ProcessText = vm.MoveIsChecked ? "Перемещение бэкапа, плис вэит..." : "Копирование бэкапа, плис вэит...";

                            ProcessVis  = Visibility.Visible;
                            ThreadPool.QueueUserWorkItem(x =>
                                                         {
                                                             Directory
                                                                .CreateDirectory($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}");
                                                             if (
                                                                 File
                                                                    .Exists($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak"))
                                                                 File
                                                                    .Delete($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak");
                                                             var bak = new BackupClass()
                                                                       { CreateTime = DateTime.Now, Name = vm.Name, Desc = vm.Desc };
                                                             if (vm.MoveIsChecked)
                                                                 File.Move(dialog.FileName,
                                                                           $"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak");
                                                             if(vm.CopyIsChecked)
                                                                 File.Copy(dialog.FileName,
                                                                           $"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\{vm.Name}.bak");
                                                             bak.Save($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{vm.Name}\\data.xml");
                                                             BackupList =
                                                                 new ObservableCollection<BackupClass>(SQLService.GetBackups(_selectedDatabase));
                                                             ProcessVis = Visibility.Hidden;
                                                         });
                        }
                    }
                }
            }
        }

        private void OnClearDB()
        {
            //CheckSchema();
            var conn = new SqlConnection($"Data Source={g.CompName}\\{SelectedDatabase.Instance};Integrated Security=False;User ID=sa;Password=SoftMarine_14;Initial Catalog={SelectedDatabase.Name};");
            conn.Open();

            var comNoCheck = new SqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'", conn);
            comNoCheck.ExecuteNonQuery();

            var comClear = new SqlCommand("EXEC sp_msforeachtable 'DELETE FROM ?'", conn);
            comClear.ExecuteNonQuery();

            var comCheck = new SqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'", conn);
            comCheck.ExecuteNonQuery();

            ShrinkLogAndFileStream(conn, SelectedDatabase.Name);

            conn.Close();
        }

        private void ShrinkLogAndFileStream(SqlConnection conn, string dbName)
        {
            var comCheck1 = new SqlCommand($"ALTER DATABASE {dbName} SET RECOVERY SIMPLE", conn);
            comCheck1.ExecuteNonQuery();
            var comCheck2 = new SqlCommand("DBCC SHRINKFILE (SOFTMARINE_log, 5)", conn);
            comCheck2.ExecuteNonQuery();

            for (var i = 0; i < 5; i++)
                new SqlCommand("EXEC sp_filestream_force_garbage_collection", conn).ExecuteNonQuery();

            var comCheck3 = new SqlCommand($"ALTER DATABASE {dbName} SET RECOVERY FULL", conn);
            comCheck3.ExecuteNonQuery();
        }

        private List<string> CheckSchema()
        {
            var schemaFromDatabase = GetSchemaFromDatabase();
            var schemaFromFile     = GetSchemaFromFile();

            var noneLst    = new List<string>();
            var redLst     = new List<string>();
            var noneSchLst = new List<(string, string)>();
            var redSchLst  = new List<(string, string)>();
            var wrongType  = new List<(string, string)>();

            foreach (var x in schemaFromDatabase)
            {
                if (schemaFromFile.Count(c => c.TableName == x.TableName) == 0)
                    noneLst.Add(x.TableName);
                else
                {
                    var sch = schemaFromFile.First(c => c.TableName == x.TableName).Schema;
                    foreach (var c in x.Schema)
                    {
                        if (sch.Count(v => v.Name == c.Name) == 0)
                            noneSchLst.Add((x.TableName, c.Name));
                        else
                        if (sch.First(v => v.Name == c.Name).Type != c.Type)
                            wrongType.Add((x.TableName, c.Name));
                    }
                }
            }

            foreach (var x in schemaFromFile)
            {
                if (schemaFromDatabase.Count(c => c.TableName == x.TableName) == 0)
                    redLst.Add(x.TableName);
                else
                {
                    var sch = schemaFromDatabase.First(c => c.TableName == x.TableName).Schema;
                    foreach (var c in x.Schema)
                    {
                        if (sch.Count(v => v.Name == c.Name) == 0)
                            redSchLst.Add((x.TableName, c.Name));
                        else
                        if (sch.First(v => v.Name == c.Name).Type != c.Type)
                            wrongType.Add((x.TableName, c.Name));
                    }
                }
            }

            var str = $"";
            if (noneLst.Count > 0)
                str = noneLst.Aggregate($"{str}\nВ копии данных не хватает таблиц, которые есть в БД:\n", (current, x) => current + $"{x}\n");
            if (noneSchLst.Count > 0)
                str = noneSchLst.Aggregate($"{str}\nВ копии данных не хватает полей, которые есть в БД:\n", (current, x) => current + $"[{x.Item1}] -> {x.Item2}\n");
            if (redLst.Count > 0)
                str = redLst.Aggregate($"{str}\nВ БД не хватает таблиц, которые есть в копии данных:\n", (current, x) => current + $"{x}\n");
            if (redSchLst.Count > 0)
                str = redSchLst.Aggregate($"{str}\nВ БД не хватает полей, которые есть в копии данных:\n", (current, x) => current + $"[{x.Item1}] -> {x.Item2}\n");
            if (wrongType.Count > 0)
                str = wrongType.Aggregate($"{ str}\nНесоответствие данных:\n", (current, x) => current + $"[{x.Item1}] -> {x.Item2}\n");

            if (noneLst.Count == 0 && redLst.Count == 0 && noneSchLst.Count == 0 && redSchLst.Count == 0 && wrongType.Count == 0) return new List<string>();

            if (MessageBox.Show($"{str}\n\n\nИгнорировать эти таблицы?", "", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
            {
                var lst = new List<string>();
                lst.AddRange(noneLst);
                lst.AddRange(redLst);
                lst.AddRange(noneSchLst.Select(x => x.Item1));
                lst.AddRange(redSchLst.Select(x => x.Item1));
                lst.AddRange(wrongType.Select(x => x.Item1));
                return lst;
            }
            else
            {
                return null;
            }
        }

        private List<SqlTable1> GetSchemaFromDatabase()
        {
            var conn = new SqlConnection($"Data Source={g.CompName}\\{SelectedDatabase.Instance};Integrated Security=True;Initial Catalog={SelectedDatabase.Name};");
            conn.Open();
            var schema    = conn.GetSchema("Tables");
            var lstlst    = conn.GetSchema("Columns");

            var tableNames = (from DataRow row in schema.Rows select row[2].ToString()).ToList();

            var tables = new List<SqlTable1>();
            foreach (var table in tableNames.OrderBy(x => x).ToList())
            {
                var com        = new SqlCommand($"SELECT * FROM {table}", conn);
                var reader     = com.ExecuteReader();
                var sch        = reader.GetSchemaTable();
                var idColIndex = -1;
                var listType   = new List<SqlType>();
                for (var j = 0; j < reader.FieldCount; j++)
                {
                    var type     = ((Type)sch.Rows[j].ItemArray[12]).FullName;
                    var nullable = ((bool)sch.Rows[j].ItemArray[13]);
                    var name     = sch.Rows[j].ItemArray[0].ToString();
                    listType.Add(new SqlType(type, nullable, name));
                }

                var tbl = new SqlTable1(table, listType);

                tables.Add(tbl);
                reader.Close();
            }
            conn.Close();
            return tables;
        }

        private List<SqlTable1> GetSchemaFromFile()
        {
            return JsonConvert.DeserializeObject<List<SqlTable1>>(File.ReadAllText("asd.txt"));
        }

        private void OnInsertIntoDB()
        {
            var op   = CheckSchema();
            if(op == null) return;
            var conn = new SqlConnection($"Data Source={g.CompName}\\{SelectedDatabase.Instance};Integrated Security=True;Initial Catalog={SelectedDatabase.Name};");
            conn.Open();
            var comNoCheck = new SqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'", conn);
            comNoCheck.ExecuteNonQuery();
            List<SqlTable1> lst = JsonConvert.DeserializeObject<List<SqlTable1>>(File.ReadAllText("asd.txt"));

            var files = new List<MarFile>();
            using (var fs = new FileStream("Files.data", FileMode.Open))
            using(var br = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                {
                    var tabNameLen = br.ReadInt32();
                    var tableName  = Encoding.UTF8.GetString(br.ReadBytes(tabNameLen));
                    var id         = br.ReadInt32();
                    var dataLen    = br.ReadInt32();
                    var data       = br.ReadBytes(dataLen);
                    files.Add(new MarFile(tableName, id, data));
                }
            }

            foreach (var x in lst)
            {
                
                var hasIdentity = true;
                try
                {
                    var comIdentityInsertOn = new SqlCommand($"SET IDENTITY_INSERT [dbo].[{x.TableName}] ON", conn);
                    comIdentityInsertOn.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    hasIdentity = false;
                }

                var dt = new DataTable();
                foreach (var c in x.Schema)
                {
                    dt.Columns.Add(c.Name, Type.GetType(c.Type));
                }
                if (op.Contains(x.TableName))
                {
                    dt.Columns.Add("UseAltNumeration", Type.GetType("System.Boolean"));
                    dt.Columns.Add("UseAltNumerationStartDate", Type.GetType("System.DateTime"));
                }

                var idInd = -1;

                foreach (var c in x.Data)
                {
                    var row = dt.NewRow();
                    for (var i = 0; i < c.Count; i++)
                    {
                        if (x.Schema[i].Type == "System.Byte[]")
                        {
                            if (idInd == -1)
                                idInd = x.Schema.IndexOf(x.Schema.First(v => v.Name == "Id"));
                            var id       = Convert.ToInt32(c[idInd]);
                            var fileData = files.FirstOrDefault(v => v.Id == id && v.Table == x.TableName);
                            if (fileData == null)
                                row[i] = DBNull.Value;
                            else
                                row[i] = fileData.Data;
                        }
                        else
                            row[i] = c[i] ?? DBNull.Value;
                    }

                    dt.Rows.Add(row);
                }

                using (var bulk = new SqlBulkCopy(conn.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    bulk.DestinationTableName = x.TableName;

                    bulk.WriteToServer(dt);
                }

                if (hasIdentity)
                {
                    var comIdentityInsertOff = new SqlCommand($"SET IDENTITY_INSERT [dbo].[{x.TableName}] OFF", conn);
                    comIdentityInsertOff.ExecuteNonQuery();
                }
            }



            var comCheck = new SqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'", conn);
            comCheck.ExecuteNonQuery();

            ShrinkLogAndFileStream(conn, SelectedDatabase.Name);

            conn.Close();
        }

        private void OnGrabData()
        {
            var conn = new SqlConnection($"Data Source={g.CompName}\\{SelectedDatabase.Instance};Integrated Security=True;Initial Catalog={SelectedDatabase.Name};");
            conn.Open();
            var schema    = conn.GetSchema("Tables");
            var lstlst    = conn.GetSchema("Columns");
            var colSchema = (from DataRow x in lstlst.Rows select x.ItemArray).ToList();

            var tableNames = (from DataRow row in schema.Rows select row[2].ToString()).ToList();

            var tables = new List<SqlTable1>();
            var files  = new List<MarFile>();
            foreach (var table in tableNames.OrderBy(x => x).ToList())
            {
                var com        = new SqlCommand($"SELECT * FROM {table}", conn);
                var reader     = com.ExecuteReader();
                var prms       = colSchema.Where(x => x[2].ToString() == table).ToList();
                var idColIndex = -1;
                var listType   = new List<SqlType>();
                var sch        = reader.GetSchemaTable();
                for (var j = 0; j < reader.FieldCount; j++)
                {
                    var type     = ((Type)sch.Rows[j].ItemArray[12]).FullName;
                    var nullable = ((bool)sch.Rows[j].ItemArray[13]);
                    var name     = sch.Rows[j].ItemArray[0].ToString();
                    listType.Add(new SqlType(type, nullable, name));
                }

                var tbl = new SqlTable1(table, listType);
                while (reader.Read())
                {
                    var row = new List<object>();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(tbl.Schema[i].Type == "System.Byte[]" ? null : reader.GetValue(i));
                        if (tbl.Schema[i].Type == "System.Byte[]")
                        {
                            if(idColIndex == -1)
                                idColIndex = prms.IndexOf(prms.First(x => x[3].ToString() == "Id"));
                            files.Add(new MarFile(table, (int)reader.GetValue(idColIndex), (byte[])reader.GetValue(i)));
                        }
                    }

                    tbl.AddRow(row);
                }

                tables.Add(tbl);
                reader.Close();
            }

            File.WriteAllText("asd.txt", JsonConvert.SerializeObject(tables, Formatting.Indented));
            using (var fs = new FileStream("Files.data", FileMode.Create, FileAccess.Write))
            using(var bw = new BinaryWriter(fs))
            {
                foreach (var x in files)
                {
                    var tableNameInBytes = System.Text.Encoding.UTF8.GetBytes(x.Table);
                    var tabNameLen       = BitConverter.GetBytes(tableNameInBytes.Length);
                    var idInBytes        = BitConverter.GetBytes(x.Id);
                    var fileLen          = BitConverter.GetBytes(x.Data.Length);

                    bw.Write(tabNameLen);
                    bw.Write(tableNameInBytes);
                    bw.Write(idInBytes);
                    bw.Write(fileLen);
                    bw.Write(x.Data);
                }
            }

            conn.Close();
        }

        private void OnRemoveBackup()
        {
            ProcessText = "Удаление бэкапа, плис вэит...";
            ProcessVis = Visibility.Visible;
            ThreadPool.QueueUserWorkItem(x =>
                                         {
                                             if (File.Exists($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{SelectedBackup.Name}\\{SelectedBackup.Name}.mdf"))
                                             {
                                                 SQLService.RemoveDatabase($"{g.CompName}\\{SelectedDatabase.Instance}", SelectedDatabase.Name);
                                                 //MessageBox.Show("Впадлу");
                                                 //return;
                                             }

                                             if (MessageBox.Show("Точно", "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) ==
                                                 MessageBoxResult.No) return;
                                             foreach (var item in
                                                 Directory
                                                    .GetFiles($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{SelectedBackup.Name}\\"))
                                                 File.Delete(item);
                                             Directory
                                                .Delete($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}\\{SelectedBackup.Name}",
                                                        true);

                                             BackupList =
                                                 new ObservableCollection<BackupClass>(SQLService.GetBackups(_selectedDatabase));
                                             ProcessVis = Visibility.Hidden;
                                         });
        }
        private void ChangeCred()
        {
            var f  = new Login();
            var vm = new LoginViewModel(f.Close);
            f.DataContext = vm;
            f.ShowDialog();
            g.Settings.Login = vm.Login;
            g.Settings.Pass  = vm.Pass;
            g.Settings.Save();
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        

    }

    public class SqlTable1
    {
        public List<SqlType>      Schema    { get; set; }
        public List<List<object>> Data      { get; set; }
        public string             TableName { get; set; }

        public SqlTable1(string tableName, List<SqlType> schema)
        {
            TableName = tableName;
            Schema = schema;
            Data = new List<List<object>>();
        }
        //public SqlTable1(string tableName)
        //{
        //    TableName = tableName;
        //    //Schema    = schema;
        //    Data      = new List<List<object>>();
        //}

        public void AddRow(List<object> objList)
        {
            Data.Add(objList);
        }
    }

    public class SqlType
    {
        public SqlType(string type, bool nullable, string name)
        {
            Type     = type;
            Nullable = nullable;
            Name     = name;
        }
        public string Type     { get; set; }
        public bool   Nullable { get; set; }
        public string Name     { get; set; }
    }

    public class MarFile
    {
        public string Table { get; set; }
        public int    Id    { get; set; }
        public byte[] Data  { get; set; }

        public MarFile(string table, int id, byte[] data)
        {
            Table = table;
            Id    = id;
            Data  = data;
        }
    }
}