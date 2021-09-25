namespace ACADPlugin.ViewModel
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;

    using Autodesk.AutoCAD.Colors;

    using ColorDialog = Autodesk.AutoCAD.Windows.ColorDialog;

    public class OpenPaletteCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (!(parameter is LayerViewModel vm))
                return;

            var dialog = new ColorDialog { Color = vm.LayerColor };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            if (dialog.Color.IsByAci)
                vm.LayerColor = Color.FromColorIndex(ColorMethod.ByAci, dialog.Color.ColorIndex);
        }
    }
}