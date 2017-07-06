using PasswordManager.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Queries {

    public abstract class ABaseSeparatedQueryHandler<TQuery, TResult> : ISeparatedQueryHandler<TQuery, TResult> where TQuery : ISeparatedQuery<TResult> {
        protected PasswordManagerContext _context;

        public ABaseSeparatedQueryHandler(PasswordManagerContext context) {
            _context = context;
        }

        public abstract TResult Execute(TQuery query);
    }
}