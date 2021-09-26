namespace ACADPlugin.Model
{
    using System;
    using System.Collections.Generic;

    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Модель слоя.
    /// </summary>
    public class LayerModel : GeometryModel
    {
        /// <summary>
        /// Модель слоя.
        /// </summary>
        /// <param name="layer"> Слой. </param>
        public LayerModel(LayerTableRecord layer)
        {
            Layer = layer;
            Color = layer.Color;
            Name = layer.Name;
            Visibility = !layer.IsOff;
        }

        /// <summary>
        /// Цвет слоя.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Примитивы в слое.
        /// </summary>
        public List<GeometryModel> Geometries { get; private set; }

        /// <summary>
        /// Идентификатор слоя.
        /// </summary>
        public ObjectId Id => Layer.Id;

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public LayerTableRecord Layer { get; }

        /// <summary>
        /// Наименование слоя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Видимость слоя.
        /// </summary>
        public bool Visibility { get; set; }

        /// <summary>
        /// Добавить примитив в список объектов слоя.
        /// </summary>
        /// <param name="geometry"> Примитив. </param>
        public void AddEntity(GeometryModel geometry)
        {
            if (Geometries != null)
                Geometries.Add(geometry);
            else
                Geometries = new List<GeometryModel> { geometry };
        }

        public override void Commit()
        {
            Layer.Name = Name;
            Layer.Color = Color;
            Layer.IsOff = !Visibility;
        }

        protected override string GetInformation()
        {
            throw new NotSupportedException("Неподдерживаемая функция!");
        }

        protected override string GetTypeName()
        {
            var visibility = "виден";
            if (Layer.IsOff)
                visibility = "скрыт";

            return $"Слой {Name}, {visibility}";
        }
    }
}