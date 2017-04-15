using PasswordManager.Util.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModels.MasterPassword {

    public class MasterPasswordCreateViewModel : ABindableBase, IViewModel {
        public string Password { get; set; }

        public MasterPasswordCreateViewModel() {
            CreateMasterPasswordCommand = new DelegateCommand(CreateMasterPassword, CreateMasterPasswordCheck);
        }

        private bool CreateMasterPasswordCheck(object obj) {
            return string.IsNullOrWhiteSpace(Password);
        }

        public DelegateCommand CreateMasterPasswordCommand { get; internal set; }

        public void CreateMasterPassword() {
        }
    }
}