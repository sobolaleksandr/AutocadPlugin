namespace ACADPlugin
{
    using System.Globalization;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Базовый класс примитива.
    /// </summary>
    public abstract class GeometryViewModel
    {
        /// <summary>
        /// Высота примитива.
        /// </summary>
        protected double _height;

        /// <summary>
        /// Базовый класс примитива.
        /// </summary>
        protected GeometryViewModel()
        {
            EditCommand = new EditCommand();
        }

        /// <summary>
        /// Команда изменения примитива.
        /// </summary>
        public EditCommand EditCommand { get; set; }

        /// <summary>
        /// Вью модель изменения примитива.
        /// </summary>
        public EditViewModel EditViewData { get; protected set; }

        /// <summary>
        /// Высота примитива.
        /// </summary>
        public string Height
        {
            get => _height.ToString(CultureInfo.InvariantCulture);
            set
            {
                if (!double.TryParse(value, out var height))
                    return;

                _height = height;
            }
        }

        /// <summary>
        /// Id-слоя.
        /// </summary>
        public ObjectId LayerId { get; protected set; }

        /// <summary>
        /// Имя слоя.
        /// </summary>
        public string LayerName { get; set; }

        /// <summary>
        /// Тип примитива.
        /// </summary>
        public string Type => GetTypeName();

        /// <summary>
        /// Функция получения имени примитва.
        /// </summary>
        /// <returns> Возращает имя примитива. </returns>
        protected abstract string GetTypeName();
    }
}