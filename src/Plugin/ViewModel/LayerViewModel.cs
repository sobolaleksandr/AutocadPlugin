namespace ACADPlugin.ViewModel
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    using Xceed.Wpf.Toolkit;

    /// <summary>
    /// Вью-модель слоя.
    /// </summary>
    public class LayerViewModel : ViewModelBase, IDataErrorInfo
    {
        private bool _visibility;
        private string _name;
        private Color _layerColor;
        public static string NameTitle => "Наименование слоя";
        public static string VisibilityTitle => "Видимость слоя";
        public static string ColorTitle => "Цвет слоя";

        public System.Windows.Media.Color LayerColor
        {
            get => _layerColor;
            set => _layerColor = value;
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

        public string Error { get; }
    }
}