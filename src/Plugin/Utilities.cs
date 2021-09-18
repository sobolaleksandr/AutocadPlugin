namespace ACADPlugin
{
    using System.Windows;

    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <summary>
    /// Различные утилиты.
    /// </summary>
    public static class Utilities
    {
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