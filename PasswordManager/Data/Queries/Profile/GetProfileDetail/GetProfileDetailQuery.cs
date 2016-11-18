using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Queries.Profile.GetProfileDetail {
    public class GetProfileDetailQuery : ISeparatedQuery<GetProfileDetailResult> {
        public int ProfileId { get; set; }
    }
}
