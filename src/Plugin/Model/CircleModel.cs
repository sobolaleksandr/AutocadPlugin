namespace ACADPlugin.Model
{
    using System.Globalization;

    using ACADPlugin.Utilities;
    using ACADPlugin.ViewModel;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Модель окружности.
    /// </summary>
    public class CircleModel : GeometryModel
    {
        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        private readonly Circle _circle;

        /// <summary>
        /// Модель окружности.
        /// </summary>
        /// <param name="circle"> Окружность. </param>
        public CircleModel(Circle circle)
        {
            _circle = circle;
            LayerId = circle.LayerId;
        }

        /// <summary>
        /// Координаты центра окружности.
        /// </summary>
        public Point3d Center
        {
            get => _circle.Center;
            set
            {
                _circle.Center = value;
            }
        }

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public double Radius
        {
            get => _circle.Radius;
            set
            {
                _circle.Radius = value;
            }
        }

        protected override string GetTypeName()
        {
            return "Окружность";
        }

        protected override string GetInformation()
        {
            return $@"{Radius.ToString("0.00", new CultureInfo("en-US"))} {Center.ToString("0.00", new CultureInfo("en-US"))}";
        }
    }
}