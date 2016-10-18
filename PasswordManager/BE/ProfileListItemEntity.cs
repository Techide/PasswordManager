﻿using PasswordManager.Util.MVVM;

namespace PasswordManager.BE {
    public class ProfileListItemEntity : ABindableBase {
        private int _id;
        public int Id { get { return _id; } set { Set(ref _id, value); } }
        public string Name { get; set; }
    }
}
