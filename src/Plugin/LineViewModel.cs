namespace ACADPlugin
{
    using System;
    using System.Globalization;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Вью-модель отрезка.
    /// </summary>
    public class LineViewModel : GeometryViewModel
    {
        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        private readonly Line _line;

        /// <summary>
        /// Вью-модель отрезка.
        /// </summary>
        public LineViewModel(Line line)
        {
            _line = line;
            LayerId = line.LayerId;
            EditViewData = new EditViewModel
            {
                Field1 = EndPoint,
                Field2 = StartPoint,
                Field3 = Height,
                Label1 = "Начальная точка",
                Label2 = "Конечная точка",
                Label3 = "Высота",
            };
            _height = Math.Min(line.StartPoint.Z, line.EndPoint.Z);
        }

        /// <summary>
        /// Конечная точка отрезка.
        /// </summary>
        public string EndPoint
        {
            get => _line.EndPoint.ToString(CultureInfo.InvariantCulture);
            set
            {
                var endPoint = value.ToPoint3d();
                _line.EndPoint = endPoint;
                _height = Math.Min(endPoint.Z, _line.StartPoint.Z);
            }
        }

        /// <summary>
        /// Начальная точка отрезка.
        /// </summary>
        public string StartPoint
        {
            get => _line.StartPoint.ToString(CultureInfo.InvariantCulture);
            set
            {
                var startPoint = value.ToPoint3d();
                _line.StartPoint = startPoint;
                _height = Math.Min(startPoint.Z, _line.EndPoint.Z);
            }
        }

        protected override string GetTypeName()
        {
            return "Отрезок";
        }
    }
}