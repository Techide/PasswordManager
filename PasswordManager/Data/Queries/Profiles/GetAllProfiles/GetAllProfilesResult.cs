using PasswordManager.BE;
using PasswordManager.Util.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Queries.Profiles.GetAllProfiles {
    public class GetAllProfilesResult : ABindableBase {

        public GetAllProfilesResult() {
            Profiles = new List<ProfileListItemEntity>();
        }

        public List<ProfileListItemEntity> Profiles { get; set; }
    }
}
