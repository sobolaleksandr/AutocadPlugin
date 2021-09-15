namespace ACADPlugin
{
    using System.Collections.Generic;
    using System.Globalization;

    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;

    public class LayerDto : Geometry
    {
        private readonly LayerTableRecord _layer;

        public LayerDto(LayerTableRecord layer)
        {
            _layer = layer;
            Geometries = new List<Geometry> { this };
            LayerName = layer.Name;
            EditViewData = new EditViewModel
            {
                Field1 = Color.ToString(CultureInfo.InvariantCulture),
                Field2 = Name.ToString(CultureInfo.InvariantCulture),
                Field3 = IsLocked.ToString(CultureInfo.InvariantCulture),
                Label1 = "Цвет",
                Label2 = "Название",
                Label3 = "Видимость",
            };
        }

        public Color Color
        {
            get => _layer.Color;
            set => _layer.Color = value;
        }

        public List<Geometry> Geometries { get; }

        public ObjectId Id => _layer.Id;

        public bool IsLocked
        {
            get => _layer.IsLocked;
            set => _layer.IsLocked = value;
        }

        public string Name
        {
            get => _layer.Name;
            set => _layer.Name = value;
        }

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