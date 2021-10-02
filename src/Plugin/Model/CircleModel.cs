namespace ACADPlugin.Model
{
    using ACADPlugin.Extensions;
    using ACADPlugin.ViewModel;

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
        public Point3d Center { get; private set; }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Circle Circle { get; }

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public double Radius { get; private set; }

        /// <summary>
        /// Принять изменения.
        /// </summary>
        /// <param name="vm"> Вью-модель окружности.</param>
        public void ApplyFrom(CircleViewModel vm)
        {
            IsChanged = vm.IsChanged;
            Center = CreatePoint(vm.X, vm.Y, vm.Z);
            Radius = vm.Radius.ToDouble();
            base.SetInformation();
            SetInformation();
        }

        public override void Commit()
        {
            Circle.Radius = Radius;
            Circle.Center = Center;
        }

        public sealed override void SetTypeName()
        {
            Type = "Окружность";
        }

        protected sealed override void SetInformation()
        {
            Information =
                $@"Радиус - {Radius.ToString(FORMAT, CultureInfo)}, Центр - {Center.ToString(FORMAT, CultureInfo)}";
        }
    }
}