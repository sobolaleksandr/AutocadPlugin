namespace ACADPlugin.ViewModel
{
    using System.ComponentModel;
    using System.Globalization;

    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Вью-модель точки.
    /// </summary>
    public class PointViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _x;
        private string _y;
        private string _z;

        /// <summary>
        /// Вью-модель точки.
        /// </summary>
        public PointViewModel(Point3d point, string error)
        {
            Error = error;
            X = point.X.ToString(CultureInfo.InvariantCulture);
            Y = point.Y.ToString(CultureInfo.InvariantCulture);
            Z = point.Z.ToString(CultureInfo.InvariantCulture);
        }

        public PointViewModel(string error)
        {
            Error = error;
        }

        public static string TitleX => "X-координата точки";
        public static string TitleY => "Y-координата точки";
        public static string TitleZ => "Z-координата точки";
        public static string WindowTitle => "Точка";

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

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(X):
                        error = CreateErrorMessage(X, error, TitleX);
                        break;
                    case nameof(Y):
                        error = CreateErrorMessage(Y, error, TitleY);
                        break;
                    case nameof(Z):
                        error = CreateErrorMessage(Z, error, TitleZ);
                        break;
                }

                return error;
            }
        }

        private string CreateErrorMessage(string value, string error, string title)
        {
            if (!double.TryParse(value, out _))
            {
                error = $@"В поле {title} должно быть число!";
                IsDataValid = false;
            }

            IsDataValid = true;
            return error;
        }
    }
}