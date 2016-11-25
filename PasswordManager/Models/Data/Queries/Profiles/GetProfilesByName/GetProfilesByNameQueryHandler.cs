using PasswordManager.Data.EF;
using PasswordManager.Models.DTO;
using System.Linq;

namespace PasswordManager.Data.Queries.Profiles.GetProfilesByName {

    public class GetProfilesByNameQueryHandler : ISeparatedQueryHandler<GetProfilesByNameQuery, GetProfilesByNameResult> {

        public GetProfilesByNameResult Execute(GetProfilesByNameQuery query) {
            var result = new GetProfilesByNameResult();
            using (var db = new PasswordManagerContext()) {
                result.Items
                    .AddRange(
                    db.Profiles
                    .Where(x => x.Name.Contains(query.QueryText))
                    ?.Select(x => new ProfileListItemEntity { Id = x.Id, Name = x.Name }));
            }
            return result;
        }
    }
}