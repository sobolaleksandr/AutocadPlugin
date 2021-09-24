namespace ACADPlugin.View
{
    using System.Windows;
    using System.Windows.Controls;

    using MessageBox = System.Windows.Forms.MessageBox;

    public partial class UserControl2
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvItem = (TreeViewItem)sender;
            MessageBox.Show("Выбран узел: " + tvItem.Header.ToString());
        }
    }
}