﻿using PasswordManager.Models.Data.EF;
using PasswordManager.Services.Cryptography;

namespace PasswordManager.Models.Data.Queries {

    public class GetProfileDetailQueryHandler : ABaseSeparatedQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> {

        public GetProfileDetailQueryHandler(PasswordManagerContext context) : base(context) {
        }

        public override GetProfileDetailResult Execute(GetProfileDetailQuery query) {
            GetProfileDetailResult result = default(GetProfileDetailResult);
            var p = _context.Profiles.Find(query.ProfileId);
            if (p != null) {
                var pwd = GetDecryptedPassword(p, query.PublicKey);
                result = new GetProfileDetailResult {
                    Profile = p.Name,
                    Account = p.Account,
                    Password = pwd,
                    Id = p.Id
                };
            }
            return result;
        }

        private string GetDecryptedPassword(Profile p, string publicKey) {
            var cp = new CryptoPassword {
                EncryptedPassword = p.Password,
                IV = p.IV,
                Salt = p.Salt,
                KeyPhrase = publicKey
            };

            return Cryptographer.Decrypt(cp);
        }
    }
}