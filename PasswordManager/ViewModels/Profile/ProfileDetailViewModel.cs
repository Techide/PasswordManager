﻿using PasswordManager.Models.Data.Queries;
using PasswordManager.Models.DTO;
using PasswordManager.Services.Navigation;
using PasswordManager.Services.Settings;
using PasswordManager.Util.MVVM;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace PasswordManager.ViewModels {

    public class ProfileDetailViewModel : ABindableBase, IViewModel {
        private ISeparatedQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> _getProfileDetailHandler;
        private GetProfileDetailResult _profileDetail;
        private string _accountDetail;
        private string _passwordDetail;
        private bool _detailVisibility;
        private INavigationService _navigation;

        private GetProfileDetailResult ProfileDetail {
            get { return _profileDetail; }
            set {
                Set(ref _profileDetail, value);
                EditProfileCommand.RaiseCanExecuteChanged();
            }
        }

        public ProfileDetailViewModel(INavigationService navigation, ISeparatedQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> getProfileDetailHandler) {
            _navigation = navigation;
            _getProfileDetailHandler = getProfileDetailHandler;
            CopyAccountCommand = new DelegateCommand(CopyAccountCommand_Execute, CopyAccountCommand_CanExecute);
            CopyPasswordCommand = new DelegateCommand(CopyPasswordCommand_Execute, CopyPasswordCommand_CanExecute);
            EditProfileCommand = new DelegateCommand(EditProfileCommand_Execute, EditProfileCommand_CanExecute);
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

        public bool ShowDetails { get { return _detailVisibility; } set { Set(ref _detailVisibility, value); } }

        public void LoadDetails(int profileId) {
            try {
                ProfileDetail = _getProfileDetailHandler.Execute(new GetProfileDetailQuery { ProfileId = profileId, PublicKey = AppSettings.MasterPassword });
                AccountDetail = _profileDetail.Account;
                PasswordDetail = _profileDetail.Password;
                ShowDetails = _profileDetail != null;
            }
            catch (Exception ex) {
                //Log.Error(ex.Message, ex);
            }
        }

        #endregion Public

        #region Commands

        #region CopyAccountCommand

        public DelegateCommand CopyAccountCommand { get; set; }

        private bool CopyAccountCommand_CanExecute(object parameter) {
            return !string.IsNullOrWhiteSpace(_accountDetail);
        }

        private void CopyAccountCommand_Execute() {
            CopyToClipboard(_accountDetail);
        }

        #endregion CopyAccountCommand

        #region CopyPasswordCommand

        public DelegateCommand CopyPasswordCommand { get; set; }

        private bool CopyPasswordCommand_CanExecute(object parameter) {
            return !string.IsNullOrWhiteSpace(_passwordDetail);
        }

        private void CopyPasswordCommand_Execute() {
            CopyToClipboard(_passwordDetail);
        }

        #endregion CopyPasswordCommand

        #region EditProfileCommand

        public DelegateCommand EditProfileCommand { get; set; }

        public bool EditProfileCommand_CanExecute(object parameter) {
            return _profileDetail != null;
        }

        public void EditProfileCommand_Execute() {
            var dto = new ProfileDTO {
                ID = _profileDetail.Id,
                Profile = _profileDetail.Profile,
                Account = _profileDetail.Account,
                Password = _profileDetail.Password
            };
            _navigation.Navigate(typeof(EditProfileViewModel), dto);
        }

        #endregion EditProfileCommand

        #endregion Commands

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
                //Log.Error(ex, ex.Message);
                //Log.Error(ex.Message, ex);
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

        #endregion Private
    }
}