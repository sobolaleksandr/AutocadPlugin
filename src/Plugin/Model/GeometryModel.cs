namespace ACADPlugin.Model
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using ACADPlugin.Command;
    using ACADPlugin.Properties;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Базовый класс примитива.
    /// </summary>
    public abstract class GeometryModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Поле свойства <see cref="IsChanged"/>.
        /// </summary>
        private bool _isChanged;

        /// <summary>
        /// Базовый класс примитива.
        /// </summary>
        protected GeometryModel()
        {
            EditCommand = new EditCommand();
        }

        /// <summary>
        /// Команда изменения примитива.
        /// </summary>
        public EditCommand EditCommand { get; set; }

        /// <summary>
        /// Информация о примитиве.
        /// </summary>
        public string Information => GetInformation();

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
        /// Id-слоя.
        /// </summary>
        public ObjectId LayerId { get; protected set; }

        /// <summary>
        /// Тип примитива.
        /// </summary>
        public string Type => GetTypeName();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Подтвердить изменения примитива.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Функция получения информации о примитиве.
        /// </summary>
        /// <returns> Возращает информацию о примитиве. </returns>
        protected abstract string GetInformation();

        /// <summary>
        /// Функция получения имени примитва.
        /// </summary>
        /// <returns> Возращает имя примитива. </returns>
        protected abstract string GetTypeName();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}