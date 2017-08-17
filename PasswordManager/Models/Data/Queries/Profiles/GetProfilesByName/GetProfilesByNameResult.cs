using PasswordManager.Models.DTO;
using System.Collections.Generic;

namespace PasswordManager.Models.Data.Queries {

    public class GetProfilesByNameResult {

        public GetProfilesByNameResult() {
            Items = new List<ProfileListItemEntity>();
        }

        public List<ProfileListItemEntity> Items { get; set; }
    }
}