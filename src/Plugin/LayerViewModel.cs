namespace ACADPlugin
{
    using System.Collections.Generic;
    using System.Globalization;

    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Модель слоя.
    /// </summary>
    public class LayerViewModel : GeometryViewModel
    {
        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        private readonly LayerTableRecord _layer;

        /// <summary>
        /// Модель слоя.
        /// </summary>
        /// <param name="layer"> Слой. </param>
        public LayerViewModel(LayerTableRecord layer)
        {
            _layer = layer;
            Geometries = new List<GeometryViewModel> { this };
            LayerName = layer.Name;
            EditViewData = new EditViewModel
            {
                Field1 = ColorModel,
                Field2 = Name.ToString(CultureInfo.InvariantCulture),
                Field3 = IsLocked.ToString(CultureInfo.InvariantCulture),
                Label1 = "Цвет",
                Label2 = "Название",
                Label3 = "Видимость",
            };
        }

        public LayerViewModel()
        {
        }

        /// <summary>
        /// Цвет слоя.
        /// </summary>
        public string ColorModel
        {
            get
            {
                var color = _layer.Color;
                var colorModel = new ColorModel(color.Red, color.Green, color.Blue);
                return colorModel.ToString();
            }
            set
            {
                var color = ACADPlugin.ColorModel.TryParse(value);
                _layer.Color = Color.FromRgb(color.R, color.G, color.B);
            }
        }

        /// <summary>
        /// Примитивы в слое.
        /// </summary>
        public List<GeometryViewModel> Geometries { get; }

        /// <summary>
        /// Идентификатор слоя.
        /// </summary>
        public ObjectId Id => _layer.Id;

        /// <summary>
        /// Видимость слоя.
        /// </summary>
        public bool IsLocked
        {
            get => _layer.IsLocked;
            set => _layer.IsLocked = value;
        }

        /// <summary>
        /// Наименования слоя
        /// </summary>
        public string Name
        {
            get => _layer.Name;
            set => _layer.Name = value;
        }

        /// <summary>
        /// Функция задания всем объектам имени слоя.
        /// </summary>
        public void AssignLayerName()
        {
            foreach (var geometry in Geometries)
            {
                geometry.LayerName = LayerName;
            }
        }

        
        protected override string GetTypeName()
        {
            return "Слой";
        }
    }
}