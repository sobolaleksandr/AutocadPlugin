namespace ACADPlugin.ViewModel
{
    using System;
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
        public PointViewModel(Point3d point)
        {
            X = point.X.ToString(CultureInfo.InvariantCulture);
            Y = point.Y.ToString(CultureInfo.InvariantCulture);
            Z = point.Z.ToString(CultureInfo.InvariantCulture);
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

        public string Error => throw new NotSupportedException("Неподдерживаемая функция");

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
                }

                return error;
            }
        }

        private string ValidateDouble(string value, string error, string title)
        {
            if (!double.TryParse(value, out _))
            {
                error = $@"В поле '{title}' должно быть число!";
            }

            return error;
        }
    }
}