using PasswordManager.BE;
using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profile.GetAllProfiles;
using PasswordManager.Data.Queries.Profile.GetProfileDetail;
using PasswordManager.Util.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PasswordManager.Logic.Profile {
    public class ProfileListViewModel : ABindableBase, IViewModel<ProfileListViewModel> {

        private IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> _getAllProfilesHandler;
        private ProfileListItemEntity _selectedItem;
        private ObservableCollection<ProfileListItemEntity> _profiles;
        private IQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> _getProfileDetailHandler;

        //private GetProfileDetailResult _currentProfile;

        public ProfileListViewModel(
            IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> getAllProfilesHandler,
            IQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> getProfileDetailHandler
            ) {
            Debug.WriteLine("[Instantiated] ProfileListViewModel");
            _getAllProfilesHandler = getAllProfilesHandler;
            _getProfileDetailHandler = getProfileDetailHandler;
            _profiles = new ObservableCollection<ProfileListItemEntity>(_getAllProfilesHandler.Execute(new GetAllProfilesQuery()));
        }

        public ObservableCollection<ProfileListItemEntity> Profiles { get { return _profiles; } set { Set(ref _profiles, value); } }

        public ProfileListItemEntity SelectedListItem {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }

        public GetProfileDetailResult ProfileDetail {
            get {
                if (_selectedItem == null) {
                    return null;
                }
                return _getProfileDetailHandler.Execute(new GetProfileDetailQuery { ProfileId = _selectedItem.Id });
            }
        }
    }
}
