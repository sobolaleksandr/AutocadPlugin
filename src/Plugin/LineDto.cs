namespace ACADPlugin
{
    using System.Globalization;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    public class LineDto : GeometryViewModel
    {
        private readonly Line _line;

        public LineDto(Line line)
        {
            _line = line;
            LayerId = line.LayerId;
            EditViewData = new EditViewModel
            {
                Field1 = EndPoint.ToString(CultureInfo.InvariantCulture),
                Field2 = StartPoint.ToString(CultureInfo.InvariantCulture),
                Field3 = Height.ToString(CultureInfo.InvariantCulture),
                Label1 = "Начальная точка",
                Label2 = "Конечная точка",
                Label3 = "Высота",
            };
        }

        public Point3d EndPoint
        {
            get => _line.EndPoint;
            set => _line.EndPoint = value;
        }

        public Point3d StartPoint
        {
            get => _line.StartPoint;
            set => _line.StartPoint = value;
        }

        protected override string GetTypeName()
        {
            return "Отрезок";
        }
    }
}