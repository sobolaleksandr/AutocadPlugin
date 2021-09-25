namespace ACADPlugin.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Модель слоя.
    /// </summary>
    public class LayerModel : GeometryModel
    {
        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        private readonly LayerTableRecord _layer;

        /// <summary>
        /// Модель слоя.
        /// </summary>
        /// <param name="layer"> Слой. </param>
        public LayerModel(LayerTableRecord layer)
        {
            _layer = layer;
        }

        /// <summary>
        /// Цвет слоя.
        /// </summary>
        public Color Color
        {
            get => _layer.Color;
            set => _layer.Color = value;
        }

        /// <summary>
        /// Примитивы в слое.
        /// </summary>
        public List<GeometryModel> Geometries { get; private set; }

        /// <summary>
        /// Идентификатор слоя.
        /// </summary>
        public ObjectId Id => _layer.Id;

        /// <summary>
        /// Видимость слоя.
        /// </summary>
        public bool IsOff
        {
            get => !_layer.IsOff;
            set => _layer.IsOff = !value;
        }

        /// <summary>
        /// Наименование слоя
        /// </summary>
        public string Name
        {
            get => _layer.Name;
            set => _layer.Name = value;
        }

        public void AddEntity(GeometryModel geometry)
        {
            if (Geometries != null)
                Geometries.Add(geometry);
            else
                Geometries = new List<GeometryModel> { geometry };
        }

        protected override string GetInformation()
        {
            throw new NotImplementedException();
        }

        protected override string GetTypeName()
        {
            var str = "виден";
            if (_layer.IsOff)
                str = "отключен";

            return $"Слой {Name}, {_layer.Color} {str}";
        }
    }
}