namespace ACADPlugin.ViewModel
{
    using System.ComponentModel;
    using System.Globalization;

    using ACADPlugin.Model;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Вью-модель окружности.
    /// </summary>
    public class CircleViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="Radius"/>.
        /// </summary>
        private string _radius;

        /// <summary>
        /// Поле свойства <see cref="X"/>.
        /// </summary>
        private string _x;

        /// <summary>
        /// Поле свойства <see cref="Y"/>.
        /// </summary>
        private string _y;

        /// <summary>
        /// Поле свойства <see cref="Z"/>.
        /// </summary>
        private string _z;

        /// <summary>
        /// Вью-модель окружности.
        /// </summary>
        /// <param name="circle"> Модель окружности. </param>
        public CircleViewModel(CircleModel circle)
        {
            Circle = circle.Circle;
            Radius = circle.Radius.ToString("0.00", new CultureInfo("en-US"));
            X = circle.Center.X.ToString("0.00", new CultureInfo("en-US"));
            Y = circle.Center.Y.ToString("0.00", new CultureInfo("en-US"));
            Z = circle.Center.Z.ToString("0.00", new CultureInfo("en-US"));
        }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Circle Circle { get; }

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public string Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="Radius"/>.
        /// </summary>
        public static string TitleRadius => "Радиус окружности";

        /// <summary>
        /// Наименование атрибута <see cref="X"/>.
        /// </summary>
        public static string TitleX => "X-координата центра окружности";

        /// <summary>
        /// Наименование атрибута <see cref="Y"/>.
        /// </summary>
        public static string TitleY => "Y-координата центра окружности";

        /// <summary>
        /// Наименование атрибута <see cref="Z"/>.
        /// </summary>
        public static string TitleZ => "Z-координата центра окружности";

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "Окружность";

        /// <summary>
        /// X-координата окружности.
        /// </summary>
        public string X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Y-координата окружности.
        /// </summary>
        public string Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Z-координата окружности.
        /// </summary>
        public string Z
        {
            get => _z;
            set
            {
                _z = value;
                OnPropertyChanged();
            }
        }

        public string Error => this[nameof(X)] + this[nameof(Y)] + this[nameof(Z)] + this[nameof(Radius)];

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(X):
                        error = ValidateDouble(X, error, TitleX);
                        break;
                    case nameof(Y):
                        error = ValidateDouble(Y, error, TitleY);
                        break;
                    case nameof(Z):
                        error = ValidateDouble(Z, error, TitleZ);
                        break;
                    case nameof(Radius):
                        error = ValidateDouble(Radius, error, TitleRadius);
                        if (string.IsNullOrWhiteSpace(error))
                        {
                            var radius = double.Parse(Radius);
                            if (radius < 0)
                                error = $@"В поле '{TitleRadius}' число должно быть больше нуля!";
                        }

                        break;
                }

                return error;
            }
        }
    }
}