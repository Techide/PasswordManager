using PasswordManager.BE;
using PasswordManager.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
