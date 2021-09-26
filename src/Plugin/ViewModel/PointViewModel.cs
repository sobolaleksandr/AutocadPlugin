namespace ACADPlugin.ViewModel
{
    using System.ComponentModel;
    using System.Globalization;

    using ACADPlugin.Model;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Вью-модель точки.
    /// </summary>
    public class PointViewModel : ViewModelBase, IDataErrorInfo
    {
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
        /// Вью-модель точки.
        /// </summary>
        public PointViewModel(PointModel point)
        {
            Point = point.Point;
            var position = point.Position;
            X = position.X.ToString("0.00", new CultureInfo("en-US"));
            Y = position.Y.ToString("0.00", new CultureInfo("en-US"));
            Z = position.Z.ToString("0.00", new CultureInfo("en-US"));
        }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public DBPoint Point { get; }

        /// <summary>
        /// Наименование атрибута <see cref="X"/>.
        /// </summary>
        public static string TitleX => "X-координата точки";

        /// <summary>
        /// Наименование атрибута <see cref="Y"/>.
        /// </summary>
        public static string TitleY => "Y-координата точки";

        /// <summary>
        /// Наименование атрибута <see cref="Z"/>.
        /// </summary>
        public static string TitleZ => "Z-координата точки";

        /// <summary>
        /// Наименование окна.
        /// </summary>
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

        public string Error => this[nameof(X)] + this[nameof(Y)] + this[nameof(Z)];

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
    }
}