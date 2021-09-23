namespace ACADPlugin.Command
{
    using System;
    using System.Windows.Input;

    using ACADPlugin.Model;
    using ACADPlugin.Utilities;
    using ACADPlugin.View;
    using ACADPlugin.ViewModel;

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
            if (!(parameter is GeometryModel geometry))
                return;

            if (!(geometry is PointModel pointModel))
                return;

            var window = new EditView
            {
                DataContext = pointModel
            };

            if (DialogUtilities.ShowDialog(window) != true)
                return;

            var viewData = geometry.EditViewData;
            ApplyChanges(viewData);
            //if (!ApplyChanges(viewData))
            //    return;

            //switch (geometry)
            //{
            //    case PointModel point:
            //    {
            //        var x = double.Parse(viewData.Field1);
            //        var y = double.Parse(viewData.Field2);
            //        var z = double.Parse(viewData.Field3);
            //        point.Position = new Point3d(x, y, z);
            //        return;
            //    }
            //    case LineModel line:
            //    {
            //        line.StartPoint = viewData.Field1;
            //        line.EndPoint = viewData.Field2;
            //        line.Height = viewData.Field3;
            //        return;
            //    }
            //    case CircleModel circle:
            //    {
            //        circle.Center = viewData.Field1;
            //        circle.Radius = viewData.Field2;
            //        circle.Height = viewData.Field3;
            //        return;
            //    }
            //    case LayerModel layer:
            //    {
            //        layer.Color = viewData.Field1;
            //        layer.Name = viewData.Field2;
            //        layer.IsOff = viewData.Field3;
            //        return;
            //    }
            //}
        }

        /// <summary>
        /// Команда открытия окна редактирования примитива
        /// </summary>
        /// <param name="viewData"> Вью-модель редактирования примитива. </param>
        /// <returns> Возращает true, если пользователь принял изменения. </returns>
        private static bool ApplyChanges(EditViewModel viewData)
        {
            var window = new EditView
            {
                DataContext = viewData
            };

            return DialogUtilities.ShowDialog(window) == true;
        }
    }
}