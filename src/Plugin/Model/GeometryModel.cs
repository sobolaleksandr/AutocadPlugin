namespace ACADPlugin.Model
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    using ACADPlugin.Command;
    using ACADPlugin.Extensions;
    using ACADPlugin.Properties;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Базовый класс примитива.
    /// </summary>
    public abstract class GeometryModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Формат преобазования в строковый тип.
        /// </summary>
        protected const string FORMAT = "0.00";

        /// <summary>
        /// Сведения о регионе.
        /// </summary>
        protected readonly CultureInfo CultureInfo = new CultureInfo("en-US");

        /// <summary>
        /// Поле свойства <see cref="Information"/>.
        /// </summary>
        private string _information;

        /// <summary>
        /// Поле свойства <see cref="IsChanged"/>.
        /// </summary>
        private bool _isChanged;

        /// <summary>
        /// Поле свойства <see cref="Type"/>.
        /// </summary>
        private string _type;

        /// <summary>
        /// Базовый класс примитива.
        /// </summary>
        protected GeometryModel()
        {
            EditCommand = new EditCommand();
        }

        /// <summary>
        /// Модель чертежа.
        /// </summary>
        public DrawingModel DrawingModel { get; set; }

        /// <summary>
        /// Команда изменения примитива.
        /// </summary>
        public EditCommand EditCommand { get; set; }

        /// <summary>
        /// Информация о примитиве.
        /// </summary>
        public string Information
        {
            get => _information;
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Примитив изменен.
        /// </summary>
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Id-слоя.
        /// </summary>
        public ObjectId LayerId { get; protected set; }

        /// <summary>
        /// Тип примитива.
        /// </summary>
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Подтвердить изменения примитива.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Функция установления имени примитва.
        /// </summary>
        public abstract void SetTypeName();

        /// <summary>
        /// Функция создания точки из трех координат.
        /// </summary>
        /// <param name="x"> Координата-Х. </param>
        /// <param name="y"> Координата-Y. </param>
        /// <param name="z"> Координата-Z. </param>
        /// <returns> Возвращает точку. </returns>
        protected static Point3d CreatePoint(string x, string y, string z)
        {
            var xValue = x.ToDouble();
            var yValue = y.ToDouble();
            var zValue = z.ToDouble();
            return new Point3d(xValue, yValue, zValue);
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Функция установления информации о примитиве.
        /// </summary>
        protected virtual void SetInformation()
        {
            DrawingModel.CommitCommand.IsChanged = true;
        }
    }
}