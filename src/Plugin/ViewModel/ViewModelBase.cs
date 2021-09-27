namespace ACADPlugin.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using ACADPlugin.Command;
    using ACADPlugin.Properties;

    /// <summary>
    /// Базовый класс для моделей представления.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Поле свойства <see cref="IsChanged"/>.
        /// </summary>
        private bool _isChanged;

        /// <summary>
        /// Базовый класс для моделей представления.
        /// </summary>
        protected ViewModelBase()
        {
            ApplyCommand = new ApplyCommand();
            RestoreCommand = new RestoreCommand();
        }

        /// <summary>
        /// Команда принятия изменений примитива.
        /// </summary>
        public ApplyCommand ApplyCommand { get; set; }

        /// <summary>
        /// Примитив изменен.
        /// </summary>
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда восстановления примитива.
        /// </summary>
        public RestoreCommand RestoreCommand { get; set; }

        /// <summary>
        /// Событие, генерируемое при изменении свойств.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод генерации события при изменении определенного свойства.
        /// </summary>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ApplyCommand.RaiseCanExecuteChanged();
        }

        protected string ValidateDouble(string value, string error, string title)
        {
            if (!double.TryParse(value, out _))
                error = $@"В поле '{title}' должно быть число!";

            return error;
        }
    }
}