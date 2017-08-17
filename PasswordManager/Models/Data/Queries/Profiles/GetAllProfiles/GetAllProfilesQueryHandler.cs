using PasswordManager.Models.Data.EF;
using PasswordManager.Models.DTO;
using System.Linq;

namespace PasswordManager.Models.Data.Queries {

    public class GetAllProfilesQueryHandler : ABaseSeparatedQueryHandler<GetAllProfilesQuery, GetAllProfilesResult> {

        public GetAllProfilesQueryHandler(PasswordManagerContext context) : base(context) {
        }

        public override GetAllProfilesResult Execute(GetAllProfilesQuery query) {
            var result = new GetAllProfilesResult();

            result.Profiles.AddRange(_context.Profiles.Select(x => new ProfileListItemEntity {
                Id = x.Id,
                Name = x.Name
            }));

            return result;
        }
    }
}