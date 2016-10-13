using PasswordManager.BE;
using PasswordManager.Data.EF.Entities;
using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profile.GetAllProfiles;
using PasswordManager.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Logic.Profile {
    public class ProfileListViewModel : IViewModel<ProfileListViewModel>, INotifyPropertyChanged {

        private IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> _handler;
        private ProfileListItemEntity _selectedItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileListViewModel(IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> handler) {
            Debug.WriteLine("[Instantiated] ProfileListViewModel");
            _handler = handler;
            PropertyChanged += ProfileListViewModel_PropertyChanged;
        }

        private void ProfileListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (sender is ProfileListItemEntity) {
                var item = (ProfileListItemEntity)sender;
                Debug.WriteLine("Should query for profile with Id: " + item.Id);
            }
        }

        public List<ProfileListItemEntity> Profiles { get { return _handler.Execute(new GetAllProfilesQuery()); } }

        public ProfileListItemEntity SelectedListItem {
            get { return _selectedItem; }
            set {
                if (_selectedItem != value) {
                    _selectedItem = value;
                    OnPropertyChanged(value);
                }
            }
        }

        private void OnPropertyChanged(ProfileListItemEntity item) {
            PropertyChanged?.Invoke(item, new PropertyChangedEventArgs("SelectedListItem"));
        }
    }
}
