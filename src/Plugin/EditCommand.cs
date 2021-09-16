namespace ACADPlugin
{
    using System;
    using System.Windows.Input;

    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Команда изменения модели примитива.
    /// </summary>
    public class EditCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

        /// <summary>
        /// Изменение модели примитива.
        /// </summary>
        /// <param name="parameter"> Вызывающий примитив. </param>
        public void Execute(object parameter)
        {
            if (!(parameter is GeometryViewModel geometry))
                return;

            var viewData = geometry.EditViewData;
            if (!ApplyChanges(viewData))
                return;

            switch (geometry)
            {
                case PointViewModel point:
                {
                    var x = double.Parse(viewData.Field1);
                    var y = double.Parse(viewData.Field2);
                    var z = double.Parse(viewData.Field3);
                    point.Position = new Point3d(x, y, z);
                    return;
                }
                case LineViewModel line:
                {
                    line.StartPoint = viewData.Field1;
                    line.EndPoint = viewData.Field2;
                    line.Height = viewData.Field3;
                    return;
                }
                case CircleViewModel circle:
                {
                    circle.Center = viewData.Field1;
                    circle.Radius = viewData.Field2;
                    circle.Height = viewData.Field3;
                    return;
                }
                case LayerViewModel layer:
                {
                    layer.ColorModel = viewData.Field1;
                    layer.Name = viewData.Field2;
                    layer.Transparency = viewData.Field3;
                    return;
                }
            }
        }

        /// <summary>
        /// Команда открытия окна редактирования примитива
        /// </summary>
        /// <param name="viewData"> Вью-модель редактирования примитива. </param>
        /// <returns> Возращает true, если пользователь принял изменения. </returns>
        private static bool ApplyChanges(EditViewModel viewData)
        {
            var window = new EditWindow
            {
                DataContext = viewData
            };

            return Utilities.ShowDialog(window) == true;
        }
    }
}