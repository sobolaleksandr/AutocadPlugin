namespace ACADPlugin.Model
{
    using System.Collections.Generic;
    using System.Windows.Media;

    using ACADPlugin.ViewModel;

    using Autodesk.AutoCAD.DatabaseServices;

    using Color = Autodesk.AutoCAD.Colors.Color;

    /// <summary>
    /// Модель слоя.
    /// </summary>
    public sealed class LayerModel : GeometryModel
    {
        /// <summary>
        /// Поле свойства <see cref="Brush"/>
        /// </summary>
        private Brush _brush;

        /// <summary>
        /// Модель слоя.
        /// </summary>
        /// <param name="layer"> Слой. </param>
        /// <param name="wasLocked"> Был ли слой заблокирован до операции. </param>
        public LayerModel(LayerTableRecord layer, bool wasLocked)
        {
            WasLocked = wasLocked;
            Layer = layer;
            Color = layer.Color;
            Name = layer.Name;
            Visibility = !layer.IsOff;
            SetTypeName();
        }

        /// <summary>
        /// Отображение цвета.
        /// </summary>
        public Brush Brush
        {
            get => _brush;
            set
            {
                _brush = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Цвет слоя.
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Примитивы в слое.
        /// </summary>
        public List<GeometryModel> Geometries { get; private set; }

        /// <summary>
        /// Идентификатор слоя.
        /// </summary>
        public ObjectId Id => Layer.Id;

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public LayerTableRecord Layer { get; }

        /// <summary>
        /// Наименование слоя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Видимость слоя.
        /// </summary>
        public bool Visibility { get; private set; }

        /// <summary>
        /// Был ли слой заблокирован до начала операции.
        /// </summary>
        private bool WasLocked { get; }

        /// <summary>
        /// Добавить примитив в список объектов слоя.
        /// </summary>
        /// <param name="geometry"> Примитив. </param>
        public void AddEntity(GeometryModel geometry)
        {
            geometry.DrawingModel = DrawingModel;
            if (Geometries != null)
                Geometries.Add(geometry);
            else
                Geometries = new List<GeometryModel> { geometry };
        }

        /// <summary>
        /// Принять изменения.
        /// </summary>
        /// <param name="vm"> Вью-модель отрезка.</param>
        public void ApplyFrom(LayerViewModel vm)
        {
            IsChanged = vm.IsChanged;
            Visibility = vm.Visibility;
            Name = vm.Name;
            Color = vm.LayerColor;
            base.SetInformation();
            SetTypeName();
        }

        public override void Commit()
        {
            Layer.IsLocked = WasLocked;
            Layer.Name = Name;
            Layer.Color = Color;
            Layer.IsOff = !Visibility;
        }

        public override void SetTypeName()
        {
            var visibility = "виден";
            if (!Visibility)
                visibility = "скрыт";

            Type = $"Слой {Name}, {visibility}";

            var cadColor = Color.ColorValue;
            var color = System.Windows.Media.Color.FromRgb(cadColor.R, cadColor.G, cadColor.B);
            Brush = new SolidColorBrush(color);
        }

        protected override void SetInformation()
        {
        }
    }
}