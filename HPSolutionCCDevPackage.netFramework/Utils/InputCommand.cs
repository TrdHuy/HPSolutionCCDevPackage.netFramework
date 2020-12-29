using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HPSolutionCCDevPackage.netFramework.Utils
{
    internal class InputCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public InputAction action;

        public InputCommand(InputAction action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return action != null;
        }

        public void Execute(object parameter)
        {
            action?.Invoke(parameter);
        }
    }
    internal delegate void InputAction(object paramaters);
}
