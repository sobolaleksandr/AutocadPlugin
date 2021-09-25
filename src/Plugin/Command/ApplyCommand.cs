namespace ACADPlugin.Command
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    using ACADPlugin.ViewModel;

    public class ApplyCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (parameter is IDataErrorInfo vm)
                return string.IsNullOrWhiteSpace(vm.Error);

            return false;
        }

        public void Execute(object parameter)
        {
            
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;
    }
}