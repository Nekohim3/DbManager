using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace DBManager
{
    public class LoginViewModel : NotificationObject
    {
        public DelegateCommand SaveCommand   { get; }
        public DelegateCommand CancelCommand { get; }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
            }
        }

        public string Pass
        {
            get => _pass;
            set
            {
                _pass = value;
                RaisePropertyChanged(() => Pass);
            }
        }

        public  bool   Cancel { get; set; }
        private Action Close;
        private string _pass;
        private string _login;

        public LoginViewModel(Action close)
        {
            Login         = g.Settings.Login;
            Pass          = g.Settings.Pass;
            Close         = close;
            Cancel        = true;
            SaveCommand   = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(OnCancel);
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
