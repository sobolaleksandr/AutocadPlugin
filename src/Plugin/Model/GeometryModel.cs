namespace ACADPlugin.Model
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using ACADPlugin.Annotations;
    using ACADPlugin.Command;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Базовый класс примитива.
    /// </summary>
    public abstract class GeometryModel : INotifyPropertyChanged
    {
        private bool _isChanged;

        /// <summary>
        /// Базовый класс примитива.
        /// </summary>
        protected GeometryModel()
        {
            EditCommand = new EditCommand();
        }

        public abstract void Commit();

        /// <summary>
        /// Команда изменения примитива.
        /// </summary>
        public EditCommand EditCommand { get; set; }

        public string Information => GetInformation();

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