using PasswordManager.Data.EF;
using PasswordManager.Models.DTO;
using System.Linq;

namespace PasswordManager.Data.Queries.Profiles.GetAllProfiles {

    public class GetAllProfilesQueryHandler : ISeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> {

        public GetAllProfilesResult Execute(GetAllProfilesQuery query) {
            //var result = new List<ProfileListItemEntity>();
            var result = new GetAllProfilesResult();
            using (var db = new PasswordManagerContext()) {
                result.Profiles.AddRange(db.Profiles.Select(x => new ProfileListItemEntity {
                    Id = x.Id,
                    Name = x.Name
                }));
            }
            return result;
        }
    }
}