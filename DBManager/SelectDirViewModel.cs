using System;
using System.Windows.Documents;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

using Microsoft.WindowsAPICodePack.Dialogs;
namespace DBManager
{
    public class SelectDirViewModel : NotificationObject
    {
        public DelegateCommand SelectDirCommand { get; }
        public DelegateCommand SaveCommand      { get; }
        public DelegateCommand CancelCommand    { get; }
        
        private string _dir;

        public string Dir
        {
            get => _dir;
            set
            {
                _dir = value;
                RaisePropertyChanged(() => Dir);
            }
        }
        
        public bool Cancel { get; set; }
        
        public Action Close { get; set; }

        public SelectDirViewModel(Action close)
        {
            Cancel           = true;
            Close            = close;
            SelectDirCommand = new DelegateCommand(OnSelectDir);
            SaveCommand      = new DelegateCommand(OnSave);
            CancelCommand    = new DelegateCommand(OnCancel);
            Dir              = g.Settings.DirForDbData;
        }

        private void OnSelectDir()
        {
            var dialog = new CommonOpenFileDialog
            {
                    IsFolderPicker = true
            };

            dialog.ShowDialog();
            Dir = dialog.FileName;
        }

        private void OnSave()
        {
            Cancel = false;
            Close?.Invoke();
        }

        private void OnCancel()
        {
            Close?.Invoke();
        }
        
    }
}