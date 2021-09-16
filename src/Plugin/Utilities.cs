namespace ACADPlugin
{
    using System.Windows;

    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;

    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <summary>
    /// Различные утилиты.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Установка цвета для сущности AutoCAD.
        /// </summary>
        /// <param name="layer">Объект чертежа.</param>
        /// <param name="color">Цвет в формате CADGIS.</param>
        public static void SetColor(this LayerTableRecord layer, ColorModel color)
        {
            if (color != null)
                layer.Color = Color.FromRgb(color.R, color.G, color.B);
        }

        /// <summary>
        /// Открыть диалоговое окно под управлением приложения AutoCAD, если у окна не указан иной хозяин.
        /// </summary>
        /// <param name="window">Окно.</param>
        /// <returns>True - если диалоговое окно успешно закрыто, false - если отменено.</returns>
        public static bool? ShowDialog(Window window)
        {
            return window.Owner == null
                ? Application.ShowModalWindow(window)
                : window.ShowDialog();
        }
    }
}