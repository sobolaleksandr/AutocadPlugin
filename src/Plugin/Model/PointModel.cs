namespace ACADPlugin.Model
{
    using System.Globalization;

    using ACADPlugin.ViewModel;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Модель точки.
    /// </summary>
    public class PointModel : GeometryModel
    {
        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        private readonly DBPoint _point;

        /// <summary>
        /// Модель точки.
        /// </summary>
        /// <param name="point"> Точка. </param>
        public PointModel(DBPoint point)
        {
            _point = point;
            Position = point.Position;
            LayerId = point.LayerId;
            EditViewData = new EditViewModel
            {
                Field1 = X,
                Field2 = Y,
                Field3 = Height,
                Label1 = "X",
                Label2 = "Y",
                Label3 = "Высота",
            };
            _height = Position.Z;
        }

        /// <summary>
        /// Позиция точки.
        /// </summary>
        public Point3d Position
        {
            get => _point.Position;
            set
            {
                _point.Position = value;
                _height = value.Z;
            }
        }

        /// <summary>
        /// X-координата точки.
        /// </summary>
        private string X => Position.X.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Y-координата точки.
        /// </summary>
        private string Y => Position.Y.ToString(CultureInfo.InvariantCulture);

        protected override string GetTypeName()
        {
            return "Точка";
        }
    }
}