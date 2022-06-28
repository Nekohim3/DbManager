using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;
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
            g.Init();
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
            var context = new SOFTMARINE_COMPANYEntities();
            context.Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");
            context.Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'");
        }

        private void OnInsertIntoDB()
        {
            using (var dbContext = new SOFTMARINE_COMPANYEntities())
            {
                dbContext.Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");
                var     str    = File.ReadAllText("qwe.txt");
                dynamic lst    = JsonConvert.DeserializeObject(str);
                var     dbSets = dbContext.GetType().GetProperties().Where(p => p.PropertyType.Name.StartsWith("DbSet"));
                var tableCounter = 0;
                foreach (var dbSetProps in dbSets)
                {
                    var dbSet = dbContext.Set(dbSetProps.PropertyType.GenericTypeArguments[0]);



                    //var dbSet     = dbSetProps.GetValue(dbContext, null);
                    var dbSetType = dbSet.GetType().GetGenericArguments().First();

                    //var rows      = ((IEnumerable)dbSet).Cast<object>().ToArray();
                    ////var lst1 = new List<object>();
                    var rowCounter = 0;
                    var rowCount   = lst[tableCounter].Count;
                    var lst1        = new List<object>();
                    for (var i = 0; i < rowCount; i++)
                    {
                        var q = Activator.CreateInstance(dbSetType);
                        ObjectExtensions.CopyProperties(lst[tableCounter][rowCounter], q);
                        lst1.Add(q);
                        //dbContext.Set(dbSetType).Add(q);

                        rowCounter++;
                    }

                    dbSet.AddRange(lst1);
                    dbContext.SaveChanges();
                    //foreach (var x in rows)
                    //{
                    //    //dynamic q = new ExpandoObject();
                    //    ObjectExtensions.CopyProperties(x, q);
                    //    rowCounter++;
                    //    //lst1.Add(q);
                    //}

                    tableCounter++;
                    //lst.Add(lst1);
                }
                dbContext.Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'");
                //var str = JsonConvert.SerializeObject(lst, Formatting.Indented);
                //File.WriteAllText("qwe.txt", str);
            }
        }

        private void OnGrabData()
        {
            var counter = 0;
            using (var dbContext = new SOFTMARINE_COMPANYEntities())
            {
                
                var dbSets = dbContext.GetType().GetProperties().Where(p => p.PropertyType.Name.StartsWith("DbSet"));
                var lst    = new List<object>();
                foreach (var dbSetProps in dbSets)
                {
                    var dbSet     = dbContext.Set(dbSetProps.PropertyType.GenericTypeArguments[0]);
                    var dbSetType = dbSet.GetType().GetGenericArguments().First();
                    var we        = dbSet.ToListAsync().Result;
                    var rows      = ((IEnumerable)dbSet).Cast<object>().ToArray();
                    var lst1      = new List<object>();
                    foreach (var x in rows)
                    {
                        dynamic q = new ExpandoObject();
                        ObjectExtensions.CopyProperties(x, q);
                        lst1.Add(q);
                        counter++;
                    }
                    lst.Add(lst1);
                }

                var str = JsonConvert.SerializeObject(lst, Formatting.Indented, new JsonSerializerSettings()
                                                                                {
                                                                                    TypeNameHandling = TypeNameHandling.All
                                                                                });
                File.WriteAllText("qwe.txt", str);
            }



            //var context = new SOFTMARINE_COMPANYEntities();

            

            //var mrv = context.mrv_FuelType.ToList();
            //var lst = new List<mrv_FuelType>();
            //foreach (var x in mrv)
            //{
            //    var q = new mrv_FuelType();
            //    ObjectExtensions.CopyProperties(x, q);
            //    lst.Add(q);
            //}
            ////var str = JsonConvert.SerializeObject(lst);
            ////context.mrv_FuelType.Add(new mrv_FuelType() { Id = 5489, EmissionFactor = 33, IdChangeInfo = 4, Explanation = "qqq", Name = "test", ShortName = "shtest"});
            ////context.SaveChanges();


            //using (var dataContext = new SOFTMARINE_COMPANYEntities())
            //using (var transaction = dataContext.Database.BeginTransaction())
            //{
            //    var fuel = new mrv_FuelType() { Id = 12, EmissionFactor = 33, IdChangeInfo = 111112, Explanation = "qqq", Name = "test", ShortName = "shtest" };

            //    //dataContext.Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");
            //    dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[mrv_FuelType] ON");

            //    dataContext.mrv_FuelType.Add(fuel);
            //    dataContext.SaveChanges();

            //    dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[mrv_FuelType] OFF");
            //    //dataContext.Database.ExecuteSqlCommand("EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'");

            //    transaction.Commit();
            //}
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
}