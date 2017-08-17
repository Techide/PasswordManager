using System;
using System.Windows.Input;

namespace PasswordManager.Util.MVVM {

    public class DelegateCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        private readonly Predicate<object> _canExecute;
        private readonly Action _execute;

        public DelegateCommand(Action executeMethod, Predicate<object> canExecuteCondition) {
            _execute = executeMethod;
            _canExecute = canExecuteCondition;
        }

        public DelegateCommand(Action executeMethod) : this(executeMethod, null) { }

        #region ICommand

        public bool CanExecute(object parameter) {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) {
            if (!CanExecute(parameter)) {
                return;
            }
            _execute();
        }

        #endregion

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
