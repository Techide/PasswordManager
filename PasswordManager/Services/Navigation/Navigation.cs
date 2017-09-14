using System;

namespace PasswordManager.Services.Navigation {
    public class Navigation : IEquatable<Navigation> {

        public Navigation(Type viewModel, Type page) {
            ViewModelType = viewModel;
            PageType = page;
        }

        public Type ViewModelType { get; set; }
        public Type PageType { get; set; }

        public dynamic Argument { get; set; }

        public bool Equals(Navigation other) {
            if (other == null) {
                return false;
            }
            return this == other;
        }

        public override int GetHashCode() {
            return ViewModelType.GetHashCode();
        }
    }
}
