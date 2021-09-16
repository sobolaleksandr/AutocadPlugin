namespace ACADPlugin
{
    using System.Windows;

    using Autodesk.AutoCAD.Colors;
    using Autodesk.AutoCAD.DatabaseServices;

    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    public static class Utilities
    {
        /// <summary>
        /// Получение цвета по индексу.
        /// </summary>
        /// <param name="colorIndex">Индекс цвета.</param>
        /// <returns>Цвет по индексу.</returns>
        public static Color GetColor(short colorIndex)
        {
            return colorIndex == 500
                ? Color.FromRgb(255, 255, 255)
                : Color.FromColorIndex(ColorMethod.ByAci, colorIndex);
        }

        /// <summary>
        /// Установка цвета для сущности AutoCAD.
        /// </summary>
        /// <param name="entity">Объект чертежа.</param>
        /// <param name="colorModel">Цвет в формате CADGIS.</param>
        public static void SetColor(this Entity entity, ColorModel colorModel)
        {
            if (colorModel != null)
                entity.Color = Color.FromRgb(colorModel.R, colorModel.G, colorModel.B);
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