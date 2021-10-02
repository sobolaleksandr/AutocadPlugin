namespace ACADPlugin.Model
{
    using ACADPlugin.ViewModel;

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
            SetTypeName();
            SetInformation();
        }

        /// <summary>
        /// Конечная точка отрезка.
        /// </summary>
        public Point3d EndPoint { get; private set; }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Line Line { get; }

        /// <summary>
        /// Начальная точка отрезка.
        /// </summary>
        public Point3d StartPoint { get; private set; }

        /// <summary>
        /// Принять изменения.
        /// </summary>
        /// <param name="vm"> Вью-модель отрезка.</param>
        public void ApplyFrom(LineViewModel vm)
        {
            IsChanged = vm.IsChanged;
            StartPoint = CreatePoint(vm.StartX, vm.StartY, vm.Height);
            EndPoint = CreatePoint(vm.EndX, vm.EndY, vm.Height);
            base.SetInformation();
            SetInformation();
        }

        public override void Commit()
        {
            Line.StartPoint = StartPoint;
            Line.EndPoint = EndPoint;
        }

        public sealed override void SetTypeName()
        {
            Type = "Отрезок";
        }

        protected sealed override void SetInformation()
        {
            Information =
                $@"Начало - {StartPoint.ToString(FORMAT, CultureInfo)}, Конец - {EndPoint.ToString(FORMAT, CultureInfo)}";
        }
    }
}