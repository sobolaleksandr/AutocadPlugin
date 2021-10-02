namespace ACADPlugin.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using ACADPlugin.Command;
    using ACADPlugin.Model;

    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Вью-модель слоя.
    /// </summary>
    public class LayerViewModel : ViewModelBase, IDataErrorInfo
    {
        private const string UN_ALLOWED_CHARACTERS = @"<>/\“:;?*|=‘";

        /// <summary>
        /// Поле свойства <see cref="LayerColor"/>.
        /// </summary>
        private Color _layerColor;

        /// <summary>
        /// Поле свойства <see cref="Name"/>.
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле свойства <see cref="Visibility"/>.
        /// </summary>
        private bool _visibility;

        /// <summary>
        /// Вью-модель слоя.
        /// </summary>
        /// <param name="layer"> Модель слоя. </param>
        public LayerViewModel(LayerModel layer)
        {
            Layer = layer.Layer;
            Visibility = layer.Visibility;
            LayerColor = layer.Color;
            Name = layer.Name;
            OpenPaletteCommand = new OpenPaletteCommand();
        }

        /// <summary>
        /// Наименование атрибута <see cref="LayerColor"/>.
        /// </summary>
        public static string ColorTitle => "Цвет слоя";

        /// <summary>
        /// Проверка на нулевой слой.
        /// </summary>
        public bool IsEditable => !Name.Equals("0", StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// Ссылка на объект чертежа.
        /// </summary>
        public LayerTableRecord Layer { get; }

        /// <summary>
        /// Цвет слоя.
        /// </summary>
        public Color LayerColor
        {
            get => _layerColor;
            set
            {
                _layerColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование слоя.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование атрибута <see cref="Name"/>.
        /// </summary>
        public string NameTitle => IsEditable ? "Наименование слоя" : "Нельзя редактировать этот слой!";

        /// <summary>
        /// Команда открытия цветовой палитры.
        /// </summary>
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

        /// <summary>
        /// Наименование атрибута <see cref="Visibility"/>.
        /// </summary>
        public static string VisibilityTitle => "Видимость слоя";

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "Слой";

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
                            error = $@"Поле '{NameTitle}' не должно быть пустым!";
                        else if (Name.Length >= 255)
                            error = $@"В поле '{NameTitle}' превышено максимальное число символов!";
                        else if (UN_ALLOWED_CHARACTERS.Any(character => Name.Contains(character)))
                            error = $@"В поле '{NameTitle}' находятся недопустимые символы!";

                        break;
                }

                return error;
            }
        }
    }
}