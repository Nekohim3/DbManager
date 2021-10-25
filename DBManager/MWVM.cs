using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.WindowsAPICodePack.Dialogs;

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
        public  DelegateCommand                   ChangeDataDirCommand               { get; }
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
            ChangeDataDirCommand         = new DelegateCommand(ChangeDataDir);


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

        private void OnRemoveBackup()
        {
            ProcessText = "Удаление бэкапа, плис вэит...";
            ProcessVis  = Visibility.Visible;
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

        private void ChangeDataDir()
        {
            var oldDir = g.Settings.DirForDbData;

            var f  = new SelectDirForm();
            var vm = new SelectDirViewModel(f.Close);
            f.DataContext = vm;
            f.ShowDialog();
            if (!Directory.Exists(vm.Dir)) return;

            g.Settings.DirForDbData = vm.Dir;
            if (Directory.Exists(g.Settings.DirForDbData))
                g.Settings.Save();

            //if (oldDir[0] != g.Settings.DirForDbData[0])
            //{
            //    CopyFilesRecursively(oldDir, g.Settings.DirForDbData);
            //}
            //else
            //{
            //    Directory.C(oldDir, g.Settings.DirForDbData);
            //}

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