namespace ACADPlugin
{
    /// <summary>
    /// Вью модель редактирования примитива.
    /// </summary>
    public class EditViewModel : ViewModelBase
    {
        /// <summary>
        /// Поле редактирования первого атрибута примитива.
        /// </summary>
        private string _field1;

        /// <summary>
        /// Поле редактирования второго атрибута примитива.
        /// </summary>
        private string _field2;

        /// <summary>
        /// Поле редактирования третьго атрибута примитива.
        /// </summary>
        private string _field3;

        /// <summary>
        /// Поле редактирования первого атрибута примитива.
        /// </summary>
        public string Field1
        {
            get => _field1;
            set
            {
                _field1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Поле редактирования второго атрибута примитива.
        /// </summary>
        public string Field2
        {
            get => _field2;
            set
            {
                _field2 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Поле редактирования третьго атрибута примитива.
        /// </summary>
        public string Field3
        {
            get => _field3;
            set
            {
                _field3 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование первого атрибута примитива.
        /// </summary>
        public string Label1 { get; set; }

        /// <summary>
        /// Наименование второго атрибута примитива.
        /// </summary>
        public string Label2 { get; set; }

        /// <summary>
        /// Наименование третьего атрибута примитива.
        /// </summary>
        public string Label3 { get; set; }
    }
}