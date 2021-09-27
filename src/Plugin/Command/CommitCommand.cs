namespace ACADPlugin.Command
{
    using System;
    using System.Windows.Input;

    using ACADPlugin.ViewModel;

    /// <summary>
    /// Команда принятия изменений чертежа.
    /// </summary>
    public class CommitCommand : ICommand
    {
        /// <summary>
        /// Поле свойства <see cref="IsChanged"/>
        /// </summary>
        private bool _isChanged;

        /// <summary>
        /// Чертеж изменен.
        /// </summary>
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                _isChanged = value;
                RaiseCanExecuteChanged();
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsChanged;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (!(parameter is ViewModelBase vm))
                return;

            vm.IsChanged = true;
        }

        private void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}