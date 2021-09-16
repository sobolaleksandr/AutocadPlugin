namespace ACADPlugin
{
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Базовый класс примитива.
    /// </summary>
    public abstract class GeometryViewModel
    {
        /// <summary>
        /// Базовый класс примитива.
        /// </summary>
        protected GeometryViewModel()
        {
            Height = 2d;
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
        public double Height { get; set; }

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