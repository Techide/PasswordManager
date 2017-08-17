
using PasswordManager.Models.Data.EF;

namespace PasswordManager.Models.Data.Queries {

    public abstract class ABaseSeparatedQueryHandler<TQuery, TResult> : ISeparatedQueryHandler<TQuery, TResult> where TQuery : ISeparatedQuery<TResult> {
        protected PasswordManagerContext _context;

        public ABaseSeparatedQueryHandler(PasswordManagerContext context) {
            _context = context;
        }

        public abstract TResult Execute(TQuery query);
    }
}