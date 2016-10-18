using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordManager.Util.MVVM {
    public abstract class ABindableBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(ref T item, T value, [CallerMemberName] string propertyName = null) {
            if (Equals(item, value)) {
                return false;
            }

            item = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
