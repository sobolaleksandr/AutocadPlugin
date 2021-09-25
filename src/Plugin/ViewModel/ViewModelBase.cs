namespace ACADPlugin.ViewModel
{
    using System.ComponentModel;

    using ACADPlugin.Command;

    /// <summary>
    /// Базовый класс для моделей представления.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected string ValidateDouble(string value, string error, string title)
        {
            if (!double.TryParse(value, out _))
            {
                error = $@"В поле '{title}' должно быть число!";
            }

            return error;
        }

        protected ViewModelBase()
        {
            ApplyCommand = new ApplyCommand();
        }

        public ApplyCommand ApplyCommand { get; set; }

        /// <summary>
        /// Событие, генерируемое при изменении свойств.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод генерации события при изменении определенного свойства.
        /// </summary>
        protected void OnPropertyChanged() => ApplyCommand.RaiseCanExecuteChanged();
    }
}