using PasswordManager.BE;
using PasswordManager.Data.EF;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Data.Queries.Profile.GetAllProfiles {
    public class GetAllProfilesQueryHandler : IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> {

        public List<ProfileListItemEntity> Execute(GetAllProfilesQuery query) {
            var result = new List<ProfileListItemEntity>();
            using (var db = new PasswordManagerContext()) {
                result.AddRange(db.Profiles.Select(x => new ProfileListItemEntity {
                    Id = x.Id,
                    Name = x.Name
                }));
            }
            return result;
        }
    }
}
