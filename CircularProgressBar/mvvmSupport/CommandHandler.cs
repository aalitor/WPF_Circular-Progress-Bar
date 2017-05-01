using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CircularProgressBarApp.mvvmSupport
{
    public class CommandHandler : ICommand
    {
        private Action execute;

        private Func<bool> canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public CommandHandler(Action execute)
           : this(execute, DefaultCanExecute)
        {
        }

        public CommandHandler(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.execute();
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private static bool DefaultCanExecute()
        {
            return true;
        }
    }
}
