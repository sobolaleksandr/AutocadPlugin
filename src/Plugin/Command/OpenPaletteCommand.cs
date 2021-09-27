namespace ACADPlugin.Command
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;

    using ACADPlugin.ViewModel;

    using Autodesk.AutoCAD.Colors;

    using ColorDialog = Autodesk.AutoCAD.Windows.ColorDialog;

    /// <summary>
    /// Команда открытия цветовой палитры.
    /// </summary>
    public class OpenPaletteCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

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