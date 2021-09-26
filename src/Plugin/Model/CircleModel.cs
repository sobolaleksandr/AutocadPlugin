namespace ACADPlugin.Model
{
    using System.Globalization;

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

        protected override string GetInformation()
        {
            return
                $@"{Radius.ToString("0.00", new CultureInfo("en-US"))} {Center.ToString("0.00", new CultureInfo("en-US"))}";
        }

        protected override string GetTypeName()
        {
            return "Окружность";
        }
    }
}