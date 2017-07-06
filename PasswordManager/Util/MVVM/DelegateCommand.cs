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

    public class DelegateCommand : ICommand {
        private readonly Predicate<object> _canExecute;
        private readonly Action _execute;
        //private Func<object, object, Action> _executeMethodWithParam;

        public DelegateCommand(Action executeMethod) : this(executeMethod, null) {
        }

        public DelegateCommand(Action executeMethod, Predicate<object> canExecuteCondition) {
            _execute = executeMethod;
            _canExecute = canExecuteCondition;
        }

        //public RelayCommand(Func<object, object, Action> executeMethod) {
        //    _executeMethodWithParam = executeMethod;
        //}

        //public RelayCommand(Func<object, object, Action> executeMethod, Predicate<object> canExecuteCondition) {
        //    _executeMethodWithParam = executeMethod;
        //    _canExecute = canExecuteCondition;
        //}

        #region ICommand

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter) {
            if (!CanExecute(parameter)) {
                return;
            }
            _execute();
        }

        #endregion ICommand

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}