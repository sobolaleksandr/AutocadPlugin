namespace ACADPlugin.Model
{
    using System;
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
            _circle = circle;
            LayerId = circle.LayerId;
        }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Circle _circle { get; set; }

        /// <summary>
        /// Координаты центра окружности.
        /// </summary>
        public Point3d Center
        {
            get => _circle.Center;
            set => _circle.Center = value;
        }

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public double Radius
        {
            get => _circle.Radius;
            set => _circle.Radius = value;
        }

        public override void Commit()
        {
            throw new NotImplementedException();
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