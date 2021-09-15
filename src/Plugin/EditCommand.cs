namespace ACADPlugin
{
    using System;
    using System.Windows.Input;

    using Autodesk.AutoCAD.ApplicationServices.Core;
    using Autodesk.AutoCAD.Geometry;

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

                    line.StartPoint = ToPoint3d(viewData.Field1);
                    line.EndPoint = ToPoint3d(viewData.Field2);
                    line.Height = double.Parse(viewData.Field3);
                    return;
                }
                case CircleDto circle:
                {
                    var viewData = circle.EditViewData;
                    if (!ApplyChanges(viewData))
                        return;

                    circle.Center = ToPoint3d(viewData.Field1);
                    circle.Radius = double.Parse(viewData.Field2);
                    circle.Height = double.Parse(viewData.Field3);
                    return;
                }
                case LayerDto layer:
                {
                    var viewData = layer.EditViewData;
                    if (!ApplyChanges(viewData))
                        return;

                    //var color = byte.Parse(viewData.Field1);
                    //layer.Color = Color.FromRgb(color, color, color);
                    layer.Name = viewData.Field2;
                    layer.IsLocked = bool.Parse(viewData.Field3);
                    return;
                }
            }
        }

        private Point3d ToPoint3d(string inputString)
        {
            var a = inputString.Trim('(', ')').Split(',');
            return new Point3d(double.Parse(a[0]), double.Parse(a[1]), double.Parse(a[2]));
        }

        private static bool ApplyChanges(EditViewModel viewData)
        {
            var window = new UserControl2
            {
                DataContext = viewData
            };

            return Application.ShowModalWindow(window) == true;
        }
    }
}