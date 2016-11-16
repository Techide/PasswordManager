using PasswordManager.Data.EF;
using PasswordManager.Util;
using PasswordManager.Util.Crypto;
using System.Linq;

namespace PasswordManager.Data.Queries.Profile.GetProfileDetail {

    public class GetProfileDetailQueryHandler : IQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> {

        public GetProfileDetailResult Execute(GetProfileDetailQuery query) {
            GetProfileDetailResult result = null;
            using (var db = new PasswordManagerContext()) {
                var p = db.Profiles.SingleOrDefault(x => x.Id == query.ProfileId);
                if (p == null) {
                    return null;
                }
                else {
                    var pwd = GetDecryptedPassword(p);
                    result = new GetProfileDetailResult {
                        Account = p.Account,
                        Password = pwd,
                        Id = p.Id
                    };
                }
            }
            return result;
        }

        private string GetDecryptedPassword(EF.Entities.Profile p) {
            var cp = new CryptoPassword {
                EncryptedPassword = p.Password,
                IV = p.IV,
                Salt = p.Salt,
                PublicKey = SettingsProvider.Password
            };

            return Cryptographer.Decrypt(cp);
        }

    }
}
