namespace ACADPlugin.Model
{
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Модель точки.
    /// </summary>
    public class PointModel : GeometryModel
    {
        /// <summary>
        /// Модель точки.
        /// </summary>
        /// <param name="point"> Точка. </param>
        public PointModel(DBPoint point)
        {
            Point = point;
            LayerId = point.LayerId;
            Position = point.Position;
            SetInformation();
            SetTypeName();
        }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public DBPoint Point { get; }

        /// <summary>
        /// Позиция точки.
        /// </summary>
        public Point3d Position { get; set; }

        public override void Commit()
        {
            Point.Position = Position;
        }

        public sealed override void SetInformation()
        {
            Information = $@"{Position.ToString(FORMAT, CultureInfo)}";
        }

        public sealed override void SetTypeName()
        {
            Type = "Точка";
        }
    }
}