namespace ACADPlugin.Model
{
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Модель окружности.
    /// </summary>
    public class CircleModel : GeometryModel
    {
        /// <summary>
        /// Модель окружности.
        /// </summary>
        /// <param name="circle"> Окружность. </param>
        public CircleModel(Circle circle)
        {
            Circle = circle;
            LayerId = circle.LayerId;
            Center = circle.Center;
            Radius = circle.Radius;
            SetInformation();
            SetTypeName();
        }

        /// <summary>
        /// Координаты центра окружности.
        /// </summary>
        public Point3d Center { get; set; }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Circle Circle { get; }

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public double Radius { get; set; }

        public override void Commit()
        {
            Circle.Radius = Radius;
            Circle.Center = Center;
        }

        public sealed override void SetInformation()
        {
            Information =
                $@"Радиус - {Radius.ToString(FORMAT, CultureInfo)}, Центр - {Center.ToString(FORMAT, CultureInfo)}";
        }

        public sealed override void SetTypeName()
        {
            Type = "Окружность";
        }
    }
}