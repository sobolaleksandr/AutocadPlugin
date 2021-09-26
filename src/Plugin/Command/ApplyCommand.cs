namespace ACADPlugin.Command
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public class ApplyCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (parameter is IDataErrorInfo vm)
                return string.IsNullOrWhiteSpace(vm.Error);

            return false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}