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
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            switch (parameter)
            {
                case PointDto point:
                {
                    var viewData = point.EditViewData;
                    if (!ApplyChanges(viewData))
                        return;

                    var x = double.Parse(viewData.Field1);
                    var y = double.Parse(viewData.Field2);
                    var z = double.Parse(viewData.Field3);
                    point.Position = new Point3d(x, y, z);
                    return;
                }
                case LineDto line:
                {
                    var viewData = line.EditViewData;
                    if (!ApplyChanges(viewData))
                        return;

                    line.StartPoint = viewData.Field1.ToPoint3d();
                    line.EndPoint = viewData.Field2.ToPoint3d();
                    line.Height = double.Parse(viewData.Field3);
                    return;
                }
                case CircleDto circle:
                {
                    var viewData = circle.EditViewData;
                    if (!ApplyChanges(viewData))
                        return;

                    circle.Center = viewData.Field1.ToPoint3d();
                    circle.Radius = double.Parse(viewData.Field2);
                    circle.Height = double.Parse(viewData.Field3);
                    return;
                }
                case LayerViewModel layer:
                {
                    var viewData = layer.EditViewData;
                    if (!ApplyChanges(viewData))
                        return;

                    layer.ColorModel = viewData.Field1;
                    layer.Name = viewData.Field2;
                    layer.IsLocked = bool.Parse(viewData.Field3);
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
            var window = new UserControl2
            {
                DataContext = viewData
            };

            return Utilities.ShowDialog(window) == true;
        }
    }
}