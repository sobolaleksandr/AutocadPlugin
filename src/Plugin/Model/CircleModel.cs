namespace ACADPlugin.Model
{
    using System.Globalization;

    using ACADPlugin.Utilities;
    using ACADPlugin.ViewModel;

    using Autodesk.AutoCAD.DatabaseServices;

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
            EditViewData = new EditViewModel
            {
                Field1 = Center,
                Field2 = Radius,
                Field3 = Height,
                Label1 = "Центр",
                Label2 = "Радиус",
                Label3 = "Высота",
            };
            _height = circle.Center.Z;
        }

        /// <summary>
        /// Координаты центра окружности.
        /// </summary>
        public string Center
        {
            get => _circle.Center.ToString(CultureInfo.InvariantCulture);
            set
            {
                var center = value.ToPoint3d();
                _circle.Center = center;
                _height = center.Z;
            }
        }

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public string Radius
        {
            get => _circle.Radius.ToString(CultureInfo.InvariantCulture);
            set
            {
                if (!double.TryParse(value, out var radius))
                    return;

                _circle.Radius = radius;
            }
        }

        protected override string GetTypeName()
        {
            return "Окружность";
        }
    }
}