using PasswordManager.Models.Data.EF;
using PasswordManager.Services.Cryptography;
using System.Linq;

namespace PasswordManager.Models.Data.Queries {

    public class AuthenticateMasterPasswordQueryHandler : ABaseSeparatedQueryHandler<AuthenticateMasterPasswordQuery, bool> {

        public AuthenticateMasterPasswordQueryHandler(PasswordManagerContext context) : base(context) {
        }

        public override bool Execute(AuthenticateMasterPasswordQuery query) {
            if (query == null) {
                return false;
            }
            var passwordSetting = _context.Settings.Single(x => x.Name == query.MasterPasswordKey);
            return SecurePasswordHasher.Verify(query.Password, passwordSetting.Value);
        }
    }
}