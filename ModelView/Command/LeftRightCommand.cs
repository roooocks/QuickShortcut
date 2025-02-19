using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/* 나중에 Command<T> 방식으로 여러 커맨드를 하나로 합치기 */
namespace QuickShortcut.ModelView.Command
{
    class LeftRightCommand : ICommand
    {
        Action<object> _execute;
        Predicate<object> _canExecute;

        public LeftRightCommand(Action<object> _executeMethod) : this(_executeMethod, null)
        {
            this._execute = _executeMethod;
        }

        public LeftRightCommand(Action<object> _executeMethod, Predicate<object> _canExecuteMethod)
        {
            _execute = _executeMethod;
            _canExecute = _canExecuteMethod;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this._execute(parameter);
        }
    }
}
