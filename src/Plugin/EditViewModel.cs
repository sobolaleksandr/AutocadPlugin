namespace ACADPlugin
{
    public class EditViewModel : ViewModelBase
    {
        private string _field1;
        private string _field2;
        private string _field3;

        public string Field1
        {
            get => _field1;
            set
            {
                _field1 = value;
                OnPropertyChanged();
            }
        }

        public string Field2
        {
            get => _field2;
            set
            {
                _field2 = value;
                OnPropertyChanged();
            }
        }

        public string Field3
        {
            get => _field3;
            set
            {
                _field3 = value;
                OnPropertyChanged();
            }
        }

        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
    }
}