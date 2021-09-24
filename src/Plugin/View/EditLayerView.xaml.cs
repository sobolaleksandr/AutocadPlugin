using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACADPlugin.View
{
    /// <summary>
    /// Interaction logic for EditLayerView.xaml
    /// </summary>
    public partial class EditLayerView
    {
        public EditLayerView()
        {
            InitializeComponent();
        }
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> routedPropertyChangedEventArgs)
        {
            TextBox.Text = "#" + ClrPckerBackground.SelectedColor?.R.ToString() + ClrPckerBackground.SelectedColor?.G.ToString() + ClrPckerBackground.SelectedColor?.B.ToString();
        }
    }
}
