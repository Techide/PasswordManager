using System;
using System.Windows.Input;

namespace PasswordManager.Util.MVVM {

    public class DelegateCommand<T> : ICommand {
        private readonly Predicate<object> _canExecute;
        private readonly Action<T> _execute;

        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, null) {
        }

        public DelegateCommand(Action<T> executeMethod, Predicate<object> canExecuteCondition) {
            _execute = executeMethod;
            _canExecute = canExecuteCondition;
        }

        #region Icommand

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) {
            if (!CanExecute(parameter)) {
                return;
            }
            _execute((T)parameter);
        }

        #endregion Icommand

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}