namespace ACADPlugin.ViewModel
{
    using System.ComponentModel;
    using System.Globalization;

    using ACADPlugin.Model;

    using Autodesk.AutoCAD.DatabaseServices;

    public class CircleViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _radius;
        private string _x;
        private string _y;
        private string _z;

        public CircleViewModel(CircleModel circle)
        {
            Circle = circle._circle;
            Radius = circle.Radius.ToString("0.00", new CultureInfo("en-US"));
            X = circle.Center.X.ToString("0.00", new CultureInfo("en-US"));
            Y = circle.Center.Y.ToString("0.00", new CultureInfo("en-US"));
            Z = circle.Center.Z.ToString("0.00", new CultureInfo("en-US"));
        }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Circle Circle { get; set; }

        public string Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                OnPropertyChanged();
            }
        }

        public static string TitleRadius => "Радиус окружности";
        public static string TitleX => "X-координата центра окружности";
        public static string TitleY => "Y-координата центра окружности";
        public static string TitleZ => "Z-координата центра окружности";
        public static string WindowTitle => "Окружность";

        /// <summary>
        /// X-координата точки.
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
        /// Y-координата точки.
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
        /// Z-координата точки.
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