using PasswordManager.Models.DTO;
using PasswordManager.Util.MVVM;
using System.Collections.Generic;

namespace PasswordManager.Data.Queries.Profiles.GetAllProfiles {

    public class GetAllProfilesResult : ABindableBase {

        public GetAllProfilesResult() {
            Profiles = new List<ProfileListItemEntity>();
        }

        public List<ProfileListItemEntity> Profiles { get; set; }
    }
}