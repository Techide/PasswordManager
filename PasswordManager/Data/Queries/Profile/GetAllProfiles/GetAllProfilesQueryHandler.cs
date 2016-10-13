using PasswordManager.BE;
using PasswordManager.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Queries.Profile.GetAllProfiles {
    public class GetAllProfilesQueryHandler : IQueryHandler<GetAllProfilesQuery, List<ProfileListItemEntity>> {

        private IQuery<List<ProfileListItemEntity>> _query;

        public GetAllProfilesQueryHandler(IQuery<List<ProfileListItemEntity>> query) {
            _query = query;
        }

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
