﻿using PasswordManager.Data.EF;
using PasswordManager.Util;
using PasswordManager.Util.Crypto;
using System;
using System.Diagnostics;
using System.Linq;

namespace PasswordManager.Data.Queries.Profiles.GetProfileDetail {

    public class GetProfileDetailQueryHandler : ISeparatedQueryHandler<GetProfileDetailQuery, GetProfileDetailResult> {

        public GetProfileDetailResult Execute(GetProfileDetailQuery query) {
            GetProfileDetailResult result = null;
            try {
                using (var db = new PasswordManagerContext()) {
                    var p = db.Profiles.SingleOrDefault(x => x.Id == query.ProfileId);
                    if (p != null) {
                        var pwd = GetDecryptedPassword(p);
                        result = new GetProfileDetailResult {
                            Account = p.Account,
                            Password = pwd,
                            Id = p.Id
                        };
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                var t = new StackTrace(ex, true);
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