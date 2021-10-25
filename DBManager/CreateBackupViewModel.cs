using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.WindowsAPICodePack.Shell.Interop;

namespace DBManager
{
    public class CreateBackupViewModel : NotificationObject
    {
        public DelegateCommand SaveCommand   { get; }
        public DelegateCommand CancelCommand { get; }

        private string _name;
        private string _desc;
        private bool   _copy;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Desc
        {
            get => _desc;
            set
            {
                _desc = value;
                RaisePropertyChanged(() => Desc);
            }
        }

        public bool Copy
        {
            get => _copy;
            set
            {
                _copy = value;
                RaisePropertyChanged(() => Copy);
            }
        }

        public  Action     Close;
        public  bool       Cancel;
        public  DataBase   DB;
        private bool       _moveIsChecked;
        private bool       _copyIsChecked;
        private Visibility _opVis;
        public  bool       FromFile { get; set; }

        public bool CopyIsChecked
        {
            get => _copyIsChecked;
            set
            {
                _copyIsChecked = value;
                RaisePropertyChanged(() => CopyIsChecked);
            }
        }

        public bool MoveIsChecked
        {
            get => _moveIsChecked;
            set
            {
                _moveIsChecked = value;
                RaisePropertyChanged(() => MoveIsChecked);
            }
        }

        public Visibility OpVis
        {
            get => _opVis;
            set
            {
                _opVis = value;
                RaisePropertyChanged(() => OpVis);
            }
        }

        public CreateBackupViewModel(Action close, DataBase db, bool fromFile = false)
        {
            OpVis         = fromFile ? Visibility.Visible : Visibility.Collapsed;
            MoveIsChecked = true;
            Cancel        = true;
            DB            = db;
            Close         = close;
            SaveCommand   = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        private void OnSave()
        {
            Cancel = false;
            if (Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || Name.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                MessageBox.Show($"В названии имеются недопустимые символы");
                return;
            }
            if (Directory.Exists($"{g.Settings.DirForDbData}\\{DB.Instance}_{DB.Name}\\{Name}"))
            {
                if(MessageBox.Show("Уже есть. Перезаписать?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No) return;
            }
            Close?.Invoke();
        }

        private void OnCancel()
        {
            Close?.Invoke();
        }
    }
}
