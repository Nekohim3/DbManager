using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.SqlServer.Management.Smo;

namespace DBManager
{
    public class DBManagerViewModel : NotificationObject
    {
        public DelegateCommand SaveCommand   { get; }
        public DelegateCommand CancelCommand { get; }
        
        private string                       _selectedInstance;
        private string                       _selectedDatabase;
        private ObservableCollection<string> _databaseList;
        public  ObservableCollection<string> InstanceList { get; set; }

        public ObservableCollection<string> DatabaseList
        {
            get => _databaseList;
            set
            {
                _databaseList = value;
                RaisePropertyChanged(() => DatabaseList);
            }
        }

        public string SelectedInstance
        {
            get => _selectedInstance;
            set
            {
                _selectedInstance = value;

                if (_selectedInstance != null)
                {
                    try
                    {
                        DatabaseList = new ObservableCollection<string>(SQLService.GetDatabases($"{g.CompName}\\{_selectedInstance}"));
                    }
                    catch(Exception e)
                    {
                        DatabaseList     = null;
                        SelectedDatabase = null;
                        MessageBox.Show("Не удалось подключиться к бд");
                    }

                }
                RaisePropertyChanged(() => SelectedInstance);
            }
        }

        public string SelectedDatabase
        {
            get => _selectedDatabase;
            set
            {
                _selectedDatabase = value;
                RaisePropertyChanged(() => SelectedDatabase);
            }
        }

        public Action Close;
        public bool   Cancel { get; set; }
        
        public DBManagerViewModel(Action close)
        {
            Close        = close;

            SaveCommand   = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(OnCancel);
            
            InstanceList = new ObservableCollection<string>(SQLService.GetSQLInstances());
        }

        private void OnSave()
        {
            Close?.Invoke();
        }

        private void OnCancel()
        {
            Cancel = true;
            Close?.Invoke();
        }

    }
}