using MetroLog;
using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profiles.GetAllProfiles;
using PasswordManager.Data.Queries.Profiles.GetProfilesByName;
using PasswordManager.Models.Data.Commands;
using PasswordManager.Models.Data.Commands.Profiles.LoadProfileDetail;
using PasswordManager.Models.DTO;
using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using PasswordManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.ViewModels {

    public class ProfileListViewModel : ABindableBase, IViewModel {
        private ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> _getAllProfilesHandler;
        private ISeparatedCommandHandler<LoadProfileDetailsCommand> _updateProfileDetailsHandler;
        private ISeparatedQueryHandler<GetProfilesByNameQuery, GetProfilesByNameResult> _getProfilesByNameHandler;
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
            ProfileDetailViewModel profileDetailViewModel
        ) {
            _getAllProfilesHandler = getAllProfilesHandler;
            _getProfilesByNameHandler = getProfilesByNameHandler;
            _updateProfileDetailsHandler = updateProfileDetailsHandler;
            _profileDetailViewModel = profileDetailViewModel;
            CreateProfileCommand = new DelegateCommand(CreateProfileCommandExecute);
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

        public DelegateCommand CreateProfileCommand { get; set; }

        private void CreateProfileCommandExecute(object parameter) {
            NavigationService.Navigate(typeof(CreateProfileViewModel));
        }

        #endregion Commands

        private void RefreshProfiles() {
            var profiles = _getAllProfilesHandler.Execute(new GetAllProfilesQuery()).Profiles;
            Profiles = new ObservableCollection<ProfileListItemEntity>(profiles);
            _profilesCache = new List<ProfileListItemEntity>(profiles);
            //var arg = NavigationService.GetContext(GetType()) as int?;
            //if (arg.HasValue) {
            //    _updateProfileDetailsHandler.Execute(new LoadProfileDetailsCommand(arg.Value, _profileDetailViewModel));
            //}
        }
    }
}