using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profile.GetProfileDetail;
using PasswordManager.Util.MVVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace PasswordManager.Logic.Profile {
    public class ProfileDetailViewModel : ABindableBase, IViewModel<ProfileDetailViewModel> {
        private GetProfileDetailResult _profileDetail;
        private string _accountDetail;
        private string _passwordDetail;
        private ISeparatedQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> _getProfileDetailHandler;

        public ProfileDetailViewModel(ISeparatedQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> getProfileDetailHandler) {
            Debug.WriteLine("[Instantiated] ProfileDetailViewModel");
            _getProfileDetailHandler = getProfileDetailHandler;
            CopyAccountCommand = new DelegateCommand(CopyAccountCommand_Execute, CopyAccountCommand_CanExecute);
            CopyPasswordCommand = new DelegateCommand(CopyPasswordCommand_Execute, CopyPasswordCommand_CanExecute);
        }

        #region Public

        public string AccountDetail {
            get { return _accountDetail; }
            set {
                Set(ref _accountDetail, value);
                CopyAccountCommand.RaiseCanExecuteChanged();
            }
        }

        public string PasswordDetail {
            get { return _passwordDetail; }
            set {
                Set(ref _passwordDetail, value);
                CopyPasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public void LoadDetails(int profileId) {
            _profileDetail = _getProfileDetailHandler.Execute(new GetProfileDetailQuery { ProfileId = profileId });
            AccountDetail = _profileDetail.Account;
            PasswordDetail = _profileDetail.Password;
        }

        #endregion // Public


        #region Commands

        #region CopyAccountCommand
        public DelegateCommand CopyAccountCommand { get; set; }

        private bool CopyAccountCommand_CanExecute(object parameter) {
            return !string.IsNullOrWhiteSpace(_accountDetail);
        }

        private void CopyAccountCommand_Execute(object parameter) {
            CopyToClipboard(_accountDetail);
        }
        #endregion //CopyAccountCommand

        #region CopyPasswordCommand
        public DelegateCommand CopyPasswordCommand { get; set; }

        private bool CopyPasswordCommand_CanExecute(object parameter) {
            return !string.IsNullOrWhiteSpace(_passwordDetail);
        }

        private void CopyPasswordCommand_Execute(object parameter) {
            CopyToClipboard(_passwordDetail);
        }
        #endregion //CopyPasswordCommand

        #endregion //Commands

        #region Private

        private void CopyToClipboard(string text) {
            try {
                var content = Clipboard.GetContent();

                if (ShouldCommitToClipboard(text, content)) {
                    var pck = new DataPackage();
                    pck.RequestedOperation = DataPackageOperation.Copy;
                    pck.SetText(text);
                    Clipboard.SetContent(pck);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool ShouldCommitToClipboard(string text, DataPackageView content) {
            if (content.Contains(StandardDataFormats.Text)) {
                var contentTask = content.GetTextAsync().AsTask();
                var contentText = contentTask.Result;
                if (!text.Equals(contentTask)) {
                    Clipboard.Clear();
                    return true;
                }
            }
            else {
                return true;
            }
            return false;
        }
        
        #endregion //Private
    }
}
