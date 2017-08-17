using PasswordManager.Models.Data.EF;
using PasswordManager.Models.DTO;
using System.Linq;

namespace PasswordManager.Models.Data.Queries {

    public class GetProfilesByNameQueryHandler : ABaseSeparatedQueryHandler<GetProfilesByNameQuery, GetProfilesByNameResult> {

        public GetProfilesByNameQueryHandler(PasswordManagerContext context) : base(context) {
        }

        public override GetProfilesByNameResult Execute(GetProfilesByNameQuery query) {
            var result = new GetProfilesByNameResult();
            result.Items
                .AddRange(
                _context.Profiles
                .Where(x => x.Name.Contains(query.QueryText))
                ?.Select(x => new ProfileListItemEntity { Id = x.Id, Name = x.Name }));
            return result;
        }
    }
}