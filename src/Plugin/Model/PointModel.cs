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
        public DBPoint Point { get; }

        /// <summary>
        /// Модель точки.
        /// </summary>
        /// <param name="point"> Точка. </param>
        public PointModel(DBPoint point)
        {
            Point = point;
            LayerId = point.LayerId;
            Position = point.Position;
        }

        /// <summary>
        /// Позиция точки.
        /// </summary>
        public Point3d Position { get; set; }
        //{
        //    get => _point.Position;
        //    set => _point.Position = value;
        //}

        public override void Commit()
        {
            Point.Position = Position;
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