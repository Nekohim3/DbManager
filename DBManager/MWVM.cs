using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace DBManager
{
    public class MWVM : NotificationObject
    {
        public  DelegateCommand                AddDatabaseCommand    { get; }
        public  DelegateCommand                RemoveDatabaseCommand { get; }
        private DataBase                       _selectedDatabase;
        private ObservableCollection<DataBase> _databaseList;

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
                RaisePropertyChanged(() => SelectedDatabase);
            }
        }

        public MWVM()
        {
            g.Settings = Settings.Load() ?? new Settings();

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
            
            AddDatabaseCommand    = new DelegateCommand(OnAddDatabase);
            RemoveDatabaseCommand = new DelegateCommand(OnRemoveDatabase);
        }

        private void OnAddDatabase()
        {
            var f  = new DBManagerForm();
            var vm = new DBManagerViewModel(f.Close);
            f.DataContext = vm;
            f.ShowDialog();

            if (!vm.Cancel)
            {
                g.Settings.AddDb(new DataBase() { Instance = vm.SelectedInstance, Name = vm.SelectedDatabase });
                DatabaseList     = new ObservableCollection<DataBase>(g.Settings.DbList);
                SelectedDatabase = DatabaseList.Count > 0 ? DatabaseList.FirstOrDefault() : null;

                if (SelectedDatabase != null)
                    if (!Directory.Exists($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}"))
                        Directory.CreateDirectory($"{g.Settings.DirForDbData}\\{SelectedDatabase.Instance}_{SelectedDatabase.Name}");
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
        
    }
}