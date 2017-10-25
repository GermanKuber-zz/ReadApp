using System;
using System.Windows.Input;

namespace Common.ForViews
{
    public class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _action;

        public CommandHandler(Action<object> action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action(parameter);
        }
    }
}