namespace ACADPlugin.ViewModel
{
    using System;
    using System.ComponentModel;

    using ACADPlugin.Model;

    using Autodesk.AutoCAD.Colors;

    /// <summary>
    /// Вью-модель слоя.
    /// </summary>
    public class LayerViewModel : ViewModelBase, IDataErrorInfo
    {
        private Color _layerColor;
        private string _name;
        private bool _visibility;

        public LayerViewModel(LayerModel layer)
        {
            Visibility = layer.IsOff;
            LayerColor = layer.Color;
            Name = layer.Name;
            OpenPaletteCommand = new OpenPaletteCommand();
        }

        public static string ColorTitle => "Цвет слоя";

        /// <summary>
        /// Проверка на нулевой слой.
        /// </summary>
        public bool IsEditable => !Name.Equals("0", StringComparison.CurrentCultureIgnoreCase);

        public Color LayerColor
        {
            get => _layerColor;
            set
            {
                _layerColor = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string NameTitle => IsEditable ? "Наименование слоя" : "Нельзя редактировать этот слой!";

        public OpenPaletteCommand OpenPaletteCommand { get; set; }

        /// <summary>
        /// Видимость слоя.
        /// </summary>
        public bool Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public static string VisibilityTitle => "Видимость слоя";

        public string Error => this[nameof(Name)];

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                            error = $@"Поле '{NameTitle}' не должно быть пустым";
                        break;
                }

                return error;
            }
        }
    }
}