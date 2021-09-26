namespace ACADPlugin.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    using ACADPlugin.Model;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Вью-модель отрезка.
    /// </summary>
    public class LineViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="EndX"/>.
        /// </summary>
        private string _endX;

        /// <summary>
        /// Поле свойства <see cref="EndY"/>.
        /// </summary>
        private string _endY;

        /// <summary>
        /// Поле свойства <see cref="Height"/>.
        /// </summary>
        private string _height;

        /// <summary>
        /// Поле свойства <see cref="StartX"/>.
        /// </summary>
        private string _startX;

        /// <summary>
        /// Поле свойства <see cref="StartY"/>.
        /// </summary>
        private string _startY;

        /// <summary>
        /// Вью-модель отрезка.
        /// </summary>
        /// <param name="line"> Модель отрезка. </param>
        public LineViewModel(LineModel line)
        {
            Line = line.Line;
            var startPoint = line.StartPoint;
            StartX = startPoint.X.ToString("0.00", new CultureInfo("en-US"));
            StartY = startPoint.Y.ToString("0.00", new CultureInfo("en-US"));
            var endPoint = line.EndPoint;
            EndX = endPoint.X.ToString("0.00", new CultureInfo("en-US"));
            EndY = endPoint.Y.ToString("0.00", new CultureInfo("en-US"));
            Height = Math.Min(startPoint.Z, endPoint.Z).ToString("0.00", new CultureInfo("en-US"));
        }

        /// <summary>
        /// Х-координата конечной точки отрезка.
        /// </summary>
        public string EndX
        {
            get => _endX;
            set
            {
                _endX = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Y-координата конечной точки отрезка.
        /// </summary>
        public string EndY
        {
            get => _endY;
            set
            {
                _endY = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Высота отрезка.
        /// </summary>
        public string Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public Line Line { get; }

        /// <summary>
        /// X-координата начальной точки отрезка.
        /// </summary>
        public string StartX
        {
            get => _startX;
            set
            {
                _startX = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Y-координата начальной точки отрезка.
        /// </summary>
        public string StartY
        {
            get => _startY;
            set
            {
                _startY = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="EndX"/>
        /// </summary>
        public static string TitleEndX => "X-координата конца отрезка";

        /// <summary>
        /// Наименование атрибута <see cref="EndY"/>
        /// </summary>
        public static string TitleEndY => "Y-координата конца отрезка";

        /// <summary>
        /// Наименование атрибута <see cref="Height"/>
        /// </summary>
        public static string TitleHeight => "Высота отрезка";

        /// <summary>
        /// Наименование атрибута <see cref="StartX"/>
        /// </summary>
        public static string TitleStartX => "X-координата начала отрезка";

        /// <summary>
        /// Наименование атрибута <see cref="StartY"/>
        /// </summary>
        public static string TitleStartY => "Y-координата начала отрезка";

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "Отрезок";

        public string Error => this[nameof(StartX)] + this[nameof(StartY)] + this[nameof(Height)] + this[nameof(EndX)] +
                               this[nameof(EndY)];

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(StartX):
                        error = ValidateDouble(StartX, error, TitleStartX);
                        break;
                    case nameof(StartY):
                        error = ValidateDouble(StartY, error, TitleStartY);
                        break;
                    case nameof(Height):
                        error = ValidateDouble(Height, error, TitleHeight);
                        break;
                    case nameof(EndX):
                        error = ValidateDouble(EndX, error, TitleEndX);
                        break;
                    case nameof(EndY):
                        error = ValidateDouble(EndY, error, TitleEndY);
                        break;
                }

                return error;
            }
        }
    }
}