using PasswordManager.Data.EF;
using PasswordManager.Data.Queries;
using PasswordManager.Services.Cryptography;
using System.Linq;

namespace PasswordManager.Models.Data.Queries.MasterPassword.AuthenticateMasterPassword {

    public class AuthenticateMasterPasswordQueryHandler : ABaseSeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> {

        public AuthenticateMasterPasswordQueryHandler(PasswordManagerContext context) : base(context) {
        }

        public override AuthenticateMasterPasswordResult Execute(AuthenticateMasterPasswordQuery query) {
            var result = new AuthenticateMasterPasswordResult();
            if (query == null) {
                return result;
            }
            var passwordSetting = _context.Settings.Single(x => x.Name == query.MasterPasswordKey);
            result.Authenticated = SecurePasswordHasher.Verify(query.Password, passwordSetting.Value);
            return result;
        }
    }
}