using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E_LearningApp.ViewModels.Commands
{
    class RelayCommandsV2 : ICommand
    {
        private Action<object> commandTask;

        public RelayCommandsV2(Action<object> workToDo)
        {
            commandTask = workToDo;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            commandTask(parameter);
        }
    }
}
