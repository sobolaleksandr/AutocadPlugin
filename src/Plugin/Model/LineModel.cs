namespace ACADPlugin.Model
{
    using System.Globalization;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Модель отрезка.
    /// </summary>
    public class LineModel : GeometryModel
    {
        /// <summary>
        /// Модель отрезка.
        /// </summary>
        /// <param name="line"> Отрезок. </param>
        public LineModel(Line line)
        {
            Line = line;
            LayerId = line.LayerId;
            EndPoint = line.EndPoint;
            StartPoint = line.StartPoint;
        }

        /// <summary>
        /// Конечная точка отрезка.
        /// </summary>
        public Point3d EndPoint { get; set; }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Line Line { get; }

        /// <summary>
        /// Начальная точка отрезка.
        /// </summary>
        public Point3d StartPoint { get; set; }

        public override void Commit()
        {
            Line.StartPoint = StartPoint;
            Line.EndPoint = EndPoint;
        }

        protected override string GetInformation()
        {
            return
                $@"{StartPoint.ToString("0.00", new CultureInfo("en-US"))} {EndPoint.ToString("0.00", new CultureInfo("en-US"))}";
        }

        protected override string GetTypeName()
        {
            return "Отрезок";
        }
    }
}