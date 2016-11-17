using PasswordManager.Data.Queries.Profile.GetProfileDetail;
using PasswordManager.Util.MVVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Logic.Profile {
    public class ProfileDetailViewModel : ABindableBase, IViewModel<ProfileDetailViewModel> {
        private GetProfileDetailResult _profileDetail;

        public ProfileDetailViewModel() {
            Debug.WriteLine("[Instantiated] ProfileDetailViewModel");
            CopyAccountCommand = new DelegateCommand(CopyAccountCommand_Execute, CopyAccountCommand_CanExecute);
        }

        #region Public

        public GetProfileDetailResult ProfileDetail {
            get { return _profileDetail; }
            set {
                Set(ref _profileDetail, value);
                CopyAccountCommand.RaiseCanExecuteChanged();
            }
        }

        #region Commands
        public DelegateCommand CopyAccountCommand { get; set; }
        public DelegateCommand CopyPasswordCommand { get; set; }

        private bool CopyAccountCommand_CanExecute(object parameter) {
            return !string.IsNullOrWhiteSpace(_profileDetail?.Account);
        }

        private void CopyAccountCommand_Execute(object parameter) {
            Debug.WriteLine("Mooh");
        }

        #endregion //Commands

        #endregion // Public
    }
}
