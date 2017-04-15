using MetroLog;
using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profiles.GetAllProfiles;
using PasswordManager.Data.Queries.Profiles.GetProfilesByName;
using PasswordManager.Models.Data.Commands;
using PasswordManager.Models.Data.Commands.Profiles.DeleteProfile;
using PasswordManager.Models.Data.Commands.Profiles.LoadProfileDetail;
using PasswordManager.Models.DTO;
using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PasswordManager.ViewModels {

    public class ProfileListViewModel : ABindableBase, IViewModel {
        private readonly ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> _getAllProfilesHandler;
        private readonly ISeparatedCommandHandler<LoadProfileDetailsCommand> _updateProfileDetailsHandler;
        private readonly ISeparatedQueryHandler<GetProfilesByNameQuery, GetProfilesByNameResult> _getProfilesByNameHandler;
        private readonly ISeparatedCommandHandler<DeleteProfileCommand> _deleteProfileHandler;
        private ProfileDetailViewModel _profileDetailViewModel;
        private ObservableCollection<ProfileListItemEntity> _profiles;
        private List<ProfileListItemEntity> _profilesCache;
        private ProfileListItemEntity _selectedItem;
        private string _queryText;

        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<ProfileListViewModel>();

        public ProfileListViewModel(
            ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> getAllProfilesHandler,
            ISeparatedQueryHandler<GetProfilesByNameQuery, GetProfilesByNameResult> getProfilesByNameHandler,
            ISeparatedCommandHandler<LoadProfileDetailsCommand> updateProfileDetailsHandler,
            ISeparatedCommandHandler<DeleteProfileCommand> deleteProfileHandler,
            ProfileDetailViewModel profileDetailViewModel
        ) {
            _getAllProfilesHandler = getAllProfilesHandler;
            _getProfilesByNameHandler = getProfilesByNameHandler;
            _updateProfileDetailsHandler = updateProfileDetailsHandler;
            _profileDetailViewModel = profileDetailViewModel;
            _deleteProfileHandler = deleteProfileHandler;
            CreateProfileCommand = new DelegateCommand(CreateProfileCommandExecute);
            DeleteProfileCommand = new DelegateCommand<int>(DeleteProfileCommandExecute);
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
                        Log.Error(ex.Message, ex);
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

        public DelegateCommand CreateProfileCommand { get; private set; }

        private void CreateProfileCommandExecute() {
            NavigationService.Navigate(typeof(CreateProfileViewModel));
        }

        public DelegateCommand<int> DeleteProfileCommand { get; set; }

        private void DeleteProfileCommandExecute(int parameter) {
            _deleteProfileHandler.Execute(new DeleteProfileCommand { Id = parameter });
            RefreshProfiles();
        }

        #endregion Commands

        private void RefreshProfiles() {
            var profiles = _getAllProfilesHandler.Execute(new GetAllProfilesQuery()).Profiles;
            Profiles = new ObservableCollection<ProfileListItemEntity>(profiles);
            _profilesCache = new List<ProfileListItemEntity>(profiles);
        }
    }
}