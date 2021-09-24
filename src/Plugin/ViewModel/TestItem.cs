namespace ACADPlugin.ViewModel
{
    using System.Collections.ObjectModel;

    public class TestItem
    {
        public TestItem()
        {
            Items = new ObservableCollection<TestItem>();
        }

        public ObservableCollection<TestItem> Items { get; set; }

        public string Title { get; set; }
    }
}