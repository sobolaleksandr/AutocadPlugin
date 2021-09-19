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
                Field1 = Color,
                Field2 = Name,
                Field3 = IsOff,
                Label1 = "Цвет",
                Label2 = "Название",
                Label3 = "Видимость",
            };
        }

        /// <summary>
        /// Цвет слоя.
        /// </summary>
        public string Color
        {
            get
            {
                var color = _layer.EntityColor.ColorIndex;
                return color.ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                if (!short.TryParse(value, out var color))
                    return;

                _layer.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, color);
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
        /// Видимость слоя.
        /// </summary>
        public string IsOff
        {
            get
            {
                var isOff = !_layer.IsOff;
                return isOff.ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                if (!bool.TryParse(value, out var isOff))
                    return;

                _layer.IsOff = !isOff;
            }
        }

        /// <summary>
        /// Наименование слоя
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