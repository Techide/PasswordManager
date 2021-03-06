﻿using PasswordManager.Util.MVVM;

namespace PasswordManager.Models.Data.Queries {

    public class GetProfileDetailResult {
        private int _id;

        private string _account;

        private string _password;

        private string _profile;

        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        public string Profile {
            get {
                return _profile;
            }

            set {
                _profile = value;
            }
        }

        public string Account {
            get { return _account; }
            set { _account = value; }
        }

        public string Password {
            get { return _password; }
            set { _password = value; }
        }
    }
}