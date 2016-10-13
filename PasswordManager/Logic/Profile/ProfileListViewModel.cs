using PasswordManager.BE;
using PasswordManager.Data.EF.Entities;
using PasswordManager.Data.Queries;
using PasswordManager.Data.Queries.Profile.GetAllProfiles;
using PasswordManager.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Logic.Profile {
    public class ProfileListViewModel : IViewModel<ProfileListUserControl> {

        private IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> _handler;

        public ProfileListViewModel(IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> handler) {
            Debug.WriteLine("[Instantiated] ProfileListViewModel");
            var profiles = new GetAllProfilesQuery();
            _handler = handler;
            //var test = _handler.Execute(profiles);
            //var stop = true;
        }

        public List<ProfileListItemEntity> Profiles { get { return _handler.Execute(new GetAllProfilesQuery()); } }

    }
}
