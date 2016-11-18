using PasswordManager.BE;
using PasswordManager.Data.Commands;
using PasswordManager.Data.Commands.Profile.UpdateProfileDetail;
using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profile.GetAllProfiles;
using PasswordManager.Util.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PasswordManager.Logic.Profile {
    public class ProfileListViewModel : ABindableBase, IViewModel<ProfileListViewModel> {

        private ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> _getAllProfilesHandler;
        private ISeparatedCommandHandler<UpdateProfileDetailsCommand> _updateProfileDetailsHandler;
        private ProfileDetailViewModel _profileDetailViewModel;
        private ObservableCollection<ProfileListItemEntity> _profiles;
        private ProfileListItemEntity _selectedItem;

        public ProfileListViewModel(
            ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> getAllProfilesHandler,
            ISeparatedCommandHandler<UpdateProfileDetailsCommand> updateProfileDetailsHandler,
            ProfileDetailViewModel profileDetailsViewModel
        ) {
            Debug.WriteLine("[Instantiated] ProfileListViewModel");
            _getAllProfilesHandler = getAllProfilesHandler;
            _updateProfileDetailsHandler = updateProfileDetailsHandler;
            _profileDetailViewModel = profileDetailsViewModel;
            _profiles = new ObservableCollection<ProfileListItemEntity>(_getAllProfilesHandler.Execute(new GetAllProfilesQuery()).Profiles);
        }

        public ObservableCollection<ProfileListItemEntity> Profiles {
            get { return _profiles; }
            set { Set(ref _profiles, value); }
        }

        public ProfileListItemEntity SelectedListItem {
            get { return _selectedItem; }
            set {
                Set(ref _selectedItem, value);
                _updateProfileDetailsHandler.Execute(new UpdateProfileDetailsCommand(_selectedItem.Id, _profileDetailViewModel));
            }
        }

        public ProfileDetailViewModel DetailViewModel {
            get { return _profileDetailViewModel; }
            set { Set(ref _profileDetailViewModel, value); }
        }

    }
}
