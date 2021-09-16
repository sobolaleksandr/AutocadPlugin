namespace ACADPlugin
{
    using System;
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
                Field2 = Name,
                Field3 = Transparency.ToString(CultureInfo.InvariantCulture),
                Label1 = "Цвет",
                Label2 = "Название",
                Label3 = "Видимость",
            };
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
                _layer.SetColor(color);
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
        /// Проверка на нулевой слой.
        /// </summary>
        private bool IsEditable => !Name.Equals("0", StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// Наименования слоя
        /// </summary>
        public string Name
        {
            get => _layer.Name.ToString(CultureInfo.InvariantCulture);
            set
            {
                if (IsEditable)
                    _layer.Name = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Видимость слоя.
        /// </summary>
        public string Transparency
        {
            get => _layer.Transparency.ToString();
            set
            {
                if (!byte.TryParse(value, out var transparency))
                    return;

                var alpha = (byte)(255 * (100 - transparency) / 100);
                _layer.Transparency = new Transparency(alpha);
            }
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