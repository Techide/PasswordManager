using PasswordManager.Models.DTO;
using PasswordManager.Util.MVVM;
using System.Collections.Generic;

namespace PasswordManager.Models.Data.Queries {

    public class GetAllProfilesResult : ABindableBase {

        public GetAllProfilesResult() {
            Profiles = new List<ProfileListItemEntity>();
        }

        public List<ProfileListItemEntity> Profiles { get; set; }
    }
}