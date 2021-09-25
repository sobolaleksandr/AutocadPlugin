namespace ACADPlugin.Model
{
    using System;
    using System.Globalization;

    using ACADPlugin.Utilities;
    using ACADPlugin.ViewModel;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Модель отрезка.
    /// </summary>
    public class LineModel : GeometryModel
    {
        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        private readonly Line _line;

        /// <summary>
        /// Модель отрезка.
        /// </summary>
        /// <param name="line"> Отрезок. </param>
        public LineModel(Line line)
        {
            _line = line;
            LayerId = line.LayerId;
        }

        /// <summary>
        /// Конечная точка отрезка.
        /// </summary>
        public Point3d EndPoint
        {
            get => _line.EndPoint;
            set
            {
                _line.EndPoint = value;
            }
        }

        /// <summary>
        /// Начальная точка отрезка.
        /// </summary>
        public Point3d StartPoint
        {
            get => _line.StartPoint;
            set
            {
                _line.StartPoint = value;
            }
        }

        protected override string GetTypeName()
        {
            return "Отрезок";
        }

        protected override string GetInformation()
        {
            return $@"{StartPoint.ToString("0.00", new CultureInfo("en-US"))} {EndPoint.ToString("0.00", new CultureInfo("en-US"))}";
        }
    }
}