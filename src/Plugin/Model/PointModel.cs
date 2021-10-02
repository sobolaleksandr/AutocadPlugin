namespace ACADPlugin.Model
{
    using ACADPlugin.ViewModel;

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
        public Point3d Position { get; private set; }

        /// <summary>
        /// Принять изменения.
        /// </summary>
        /// <param name="vm"> Вью-модель точки.</param>
        public void ApplyFrom(PointViewModel vm)
        {
            IsChanged = vm.IsChanged;
            Position = CreatePoint(vm.X, vm.Y, vm.Z);
            base.SetInformation();
            SetInformation();
        }

        public override void Commit()
        {
            Point.Position = Position;
        }

        public sealed override void SetTypeName()
        {
            Type = "Точка";
        }

        protected sealed override void SetInformation()
        {
            Information = $@"{Position.ToString(FORMAT, CultureInfo)}";
        }
    }
}