namespace ACADPlugin
{
    using System.Globalization;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    public class PointDto : GeometryViewModel
    {
        private readonly DBPoint _point;

        public PointDto(DBPoint point)
        {
            _point = point;
            Position = point.Position;
            LayerId = point.LayerId;
            EditViewData = new EditViewModel
            {
                Field1 = X.ToString(CultureInfo.InvariantCulture),
                Field2 = Y.ToString(CultureInfo.InvariantCulture),
                Field3 = Height.ToString(CultureInfo.InvariantCulture),
                Label1 = "X",
                Label2 = "Y",
                Label3 = "Высота",
            };
            Height = Position.Z;
        }

        public Point3d Position
        {
            get => _point.Position;
            set
            {
                _point.Position = value;
                Height = value.Z;
            }
        }

        public double X => Position.X;

        public double Y => Position.Y;

        protected override string GetTypeName()
        {
            return "Точка";
        }
    }
}