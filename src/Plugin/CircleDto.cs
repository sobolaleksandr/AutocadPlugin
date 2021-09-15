namespace ACADPlugin
{
    using System.Globalization;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    public class CircleDto : Geometry
    {
        private readonly Circle _circle;

        public CircleDto(Circle circle)
        {
            _circle = circle;
            LayerId = circle.LayerId;
            EditViewData = new EditViewModel
            {
                Field1 = Center.ToString(CultureInfo.InvariantCulture),
                Field2 = Radius.ToString(CultureInfo.InvariantCulture),
                Field3 = Height.ToString(CultureInfo.InvariantCulture),
                Label1 = "Центр",
                Label2 = "Радиус",
                Label3 = "Высота",
            };
        }

        public Point3d Center
        {
            get => _circle.Center;
            set => _circle.Center = value;
        }

        public double Radius
        {
            get => _circle.Radius;
            set => _circle.Radius = value;
        }

        protected override string GetTypeName()
        {
            return "Окружность";
        }
    }
}