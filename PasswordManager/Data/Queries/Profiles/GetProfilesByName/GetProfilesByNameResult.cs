using PasswordManager.BE;
using System.Collections.Generic;

namespace PasswordManager.Data.Queries.Profiles.GetProfilesByName {
    public class GetProfilesByNameResult {

        public GetProfilesByNameResult() {
            Items = new List<ProfileListItemEntity>();
        }

        public List<ProfileListItemEntity> Items { get; set; }
    }
}