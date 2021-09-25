namespace ACADPlugin.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    using ACADPlugin.Model;

    /// <summary>
    /// Вью-модель отрезка.
    /// </summary>
    public class LineViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _endX;
        private string _endY;
        private string _height;
        private string _startX;
        private string _startY;

        public LineViewModel(LineModel line)
        {
            var startPoint = line.StartPoint;
            StartX = startPoint.X.ToString("0.00", new CultureInfo("en-US"));
            StartY = startPoint.Y.ToString("0.00", new CultureInfo("en-US"));
            var endPoint = line.EndPoint;
            EndX = endPoint.X.ToString("0.00", new CultureInfo("en-US"));
            EndY = endPoint.Y.ToString("0.00", new CultureInfo("en-US"));
            Height = Math.Min(startPoint.Z,endPoint.Z).ToString("0.00", new CultureInfo("en-US"));
        }

        public string EndX
        {
            get => _endX;
            set
            {
                _endX = value;
                OnPropertyChanged();
            }
        }

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
        /// X-координата точки.
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
        /// Y-координата точки.
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

        public static string TitleEndX => "X-координата конца отрезка";
        public static string TitleEndY => "Y-координата конца отрезка";
        public static string TitleHeight => "Высота отрезка";
        public static string TitleStartX => "X-координата начала отрезка";
        public static string TitleStartY => "Y-координата начала отрезка";
        public static string WindowTitle => "Отрезок";

        public string this[string columnName] {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(StartX) :
                        error = ValidateDouble(StartX, error, TitleStartX);
                        break;
                    case nameof(StartY) :
                        error = ValidateDouble(StartY, error, TitleStartY);
                        break;
                    case nameof(Height) :
                        error = ValidateDouble(Height, error, TitleHeight);
                        break;
                }

                return error;
            }
        }

public string Error { get; }
    }
}