using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.Util.MVVM {

    public class MenuItemDelegateCommand : ICommand {
        private readonly Predicate<object> _canExecute;
        private Action<ItemClickEventArgs> _execute;

        public MenuItemDelegateCommand(Action<ItemClickEventArgs> executeMethod, Predicate<object> canExecuteCondition) {
            _execute = executeMethod;
            _canExecute = canExecuteCondition;
        }

        public MenuItemDelegateCommand(Action<ItemClickEventArgs> executeMethod) : this(executeMethod, null) { }

        #region ICommand

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) {
            if (!CanExecute(parameter)) {
                return;
            }
            var args = parameter as ItemClickEventArgs;
            _execute(args);
        }

        #endregion ICommand

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
