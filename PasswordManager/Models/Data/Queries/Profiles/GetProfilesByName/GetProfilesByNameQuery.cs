using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models.Data.Queries {
    public class GetProfilesByNameQuery : ISeparatedQuery<GetProfilesByNameResult> {
        public string QueryText { get; set; }
    }
}
