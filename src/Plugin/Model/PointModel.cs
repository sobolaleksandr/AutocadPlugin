namespace ACADPlugin.Model
{
    using System.Globalization;

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
            LayerId = point.LayerId;
        }

        /// <summary>
        /// Позиция точки.
        /// </summary>
        public Point3d Position
        {
            get => _point.Position;
            set => _point.Position = value;
        }

        protected override string GetInformation()
        {
            return $@"{Position.ToString("0.00", new CultureInfo("en-US"))}";
        }

        protected override string GetTypeName()
        {
            return "Точка";
        }
    }
}