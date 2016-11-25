using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Queries.Profiles.GetProfilesByName {
    public class GetProfilesByNameQuery : ISeparatedQuery<GetProfilesByNameResult> {
        public string QueryText { get; set; }
    }
}
