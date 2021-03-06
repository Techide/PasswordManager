﻿using PasswordManager.Models.Data.Commands;
using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using System;

namespace PasswordManager.ViewModels {

    public class CreateProfileViewModel : ABindableBase, IViewModel {
        private ISeparatedCommandHandler<CreateProfileCommand> _createProfileHandler;
        private string _profile;
        private string _account;
        private string _password;
        private INavigationService _navigation;

        public string Profile {
            get { return _profile; }
            set {
                Set(ref _profile, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Account {
            get { return _account; }
            set {
                Set(ref _account, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password {
            get { return _password; }
            set {
                Set(ref _password, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public CreateProfileViewModel(INavigationService navigation, ISeparatedCommandHandler<CreateProfileCommand> createProfileHandler) {
            _navigation = navigation;
            _createProfileHandler = createProfileHandler;
            SaveCommand = new DelegateCommand(SaveCommandExecuted, SaveCommandCanExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecuted);
        }

        public DelegateCommand SaveCommand { get; set; }

        private bool SaveCommandCanExecute(object parameter) {
            return !string.IsNullOrWhiteSpace(_profile) && !string.IsNullOrWhiteSpace(_account) && !string.IsNullOrWhiteSpace(_password);
        }

        private void SaveCommandExecuted() {
            var command = new CreateProfileCommand {
                Profile = _profile,
                Account = _account,
                Password = _password
            };
            try {
                _createProfileHandler.Execute(command);
                CancelCommandExecuted();
            }
            catch (Exception ex) {
                //Log.Error(ex.Message, ex);
            }
        }

        public DelegateCommand CancelCommand { get; set; }

        private void CancelCommandExecuted() {
            _navigation.GoBack(typeof(MainViewModel));
        }
    }
}