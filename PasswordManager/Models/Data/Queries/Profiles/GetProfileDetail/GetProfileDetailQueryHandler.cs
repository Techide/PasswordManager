using PasswordManager.Data.EF;
using PasswordManager.Data.EF.Entities;
using PasswordManager.Services.Cryptography;
using PasswordManager.Services.Settings;
using System;
using System.Diagnostics;
using System.Linq;

namespace PasswordManager.Data.Queries.Profiles.GetProfileDetail {

    public class GetProfileDetailQueryHandler : ISeparatedQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> {

        public GetProfileDetailResult Execute(GetProfileDetailQuery query) {
            GetProfileDetailResult result = null;
            Profile p = null;
            using (var db = new PasswordManagerContext()) {
                p = db.Profiles.SingleOrDefault(x => x.Id == query.ProfileId);
            }
            if (p != null) {
                var pwd = GetDecryptedPassword(p);
                result = new GetProfileDetailResult {
                    Profile = p.Name,
                    Account = p.Account,
                    Password = pwd,
                    Id = p.Id
                };
            }
            return result;
        }

        private string GetDecryptedPassword(Profile p) {
            var publicKey = SettingsService.Password;
            var cp = new CryptoPassword {
                EncryptedPassword = p.Password,
                IV = p.IV,
                Salt = p.Salt,
                PublicKey = publicKey
            };

            return Cryptographer.Decrypt(cp);
        }
    }
}