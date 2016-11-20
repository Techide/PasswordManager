using PasswordManager.BE;
using PasswordManager.Data.Commands;
using PasswordManager.Data.Commands.Profiles.LoadProfileDetail;
using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profiles.GetAllProfiles;
using PasswordManager.Data.Queries.Profiles.GetProfilesByName;
using PasswordManager.Util;
using PasswordManager.Util.MVVM;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;

namespace PasswordManager.Logic.Profile {

    public class ProfileListViewModel : ABindableBase, IViewModel<ProfileListViewModel> {
        private ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> _getAllProfilesHandler;
        private ISeparatedCommandHandler<LoadProfileDetailsCommand> _updateProfileDetailsHandler;
        private ISeparatedQueryHandler<GetProfilesByNameQuery, GetProfilesByNameResult> _getProfilesByNameHandler;
        private ProfileDetailViewModel _profileDetailViewModel;
        private ObservableCollection<ProfileListItemEntity> _profiles;
        private List<ProfileListItemEntity> _profilesCache;
        private ProfileListItemEntity _selectedItem;
        private string _queryText;

        public ProfileListViewModel(
            ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> getAllProfilesHandler,
            ISeparatedQueryHandler<GetProfilesByNameQuery, GetProfilesByNameResult> getProfilesByNameHandler,
            ISeparatedCommandHandler<LoadProfileDetailsCommand> updateProfileDetailsHandler,
            ProfileDetailViewModel profileDetailsViewModel
        ) {
            _getAllProfilesHandler = getAllProfilesHandler;
            _getProfilesByNameHandler = getProfilesByNameHandler;
            _updateProfileDetailsHandler = updateProfileDetailsHandler;
            _profileDetailViewModel = profileDetailsViewModel;
            AddProfileCommand = new DelegateCommand(AddProfileCommandExecute);
            RefreshProfiles();
        }

        public ObservableCollection<ProfileListItemEntity> Profiles {
            get { return _profiles; }
            set { Set(ref _profiles, value); }
        }

        public string QueryText {
            get { return _queryText; }
            set {
                Set(ref _queryText, value);
                if (!string.IsNullOrWhiteSpace(value)) {
                    var query = value.Trim().ToLowerInvariant();
                    SelectedListItem = null;
                    try {
                        Profiles = new ObservableCollection<ProfileListItemEntity>(_profilesCache.Where(x => x.Name.ToLowerInvariant().Contains(query)));
                    }
                    catch (Exception ex) {
                        Log.Error(ex, ex.Message);
                    }
                }
                else {
                    RefreshProfiles();
                }
            }
        }

        public ProfileListItemEntity SelectedListItem {
            get { return _selectedItem; }
            set {
                Set(ref _selectedItem, value);
                if (value != null) {
                    _updateProfileDetailsHandler.Execute(new LoadProfileDetailsCommand(_selectedItem.Id, _profileDetailViewModel));
                }
            }
        }

        public ProfileDetailViewModel DetailViewModel {
            get { return _profileDetailViewModel; }
            set { Set(ref _profileDetailViewModel, value); }
        }

        #region Commands

        public DelegateCommand AddProfileCommand { get; set; }

        private void AddProfileCommandExecute(object parameter) {
        }

        #endregion Commands

        private void RefreshProfiles() {
            var profiles = _getAllProfilesHandler.Execute(new GetAllProfilesQuery()).Profiles;
            Profiles = new ObservableCollection<ProfileListItemEntity>(profiles);
            _profilesCache = new List<ProfileListItemEntity>(profiles);
        }
    }
}