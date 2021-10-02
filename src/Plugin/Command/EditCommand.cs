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

            switch (geometry)
            {
                case PointModel point:
                {
                    var vm = new PointViewModel(point);
                    var window = new EditPointView
                    {
                        DataContext = vm
                    };

                    if (DialogUtilities.ShowDialog(window) == true)
                        point.ApplyFrom(vm);

                    break;
                }
                case CircleModel circle:
                {
                    var vm = new CircleViewModel(circle);
                    var window = new EditCircleView
                    {
                        DataContext = vm
                    };

                    if (DialogUtilities.ShowDialog(window) == true)
                        circle.ApplyFrom(vm);

                    break;
                }
                case LayerModel layer:
                {
                    var vm = new LayerViewModel(layer);
                    var window = new EditLayerView
                    {
                        DataContext = vm
                    };

                    if (DialogUtilities.ShowDialog(window) == true)
                        layer.ApplyFrom(vm);

                    break;
                }
                case LineModel line:
                {
                    var vm = new LineViewModel(line);
                    var window = new EditLineView
                    {
                        DataContext = vm
                    };

                    if (DialogUtilities.ShowDialog(window) == true)
                        line.ApplyFrom(vm);

                    break;
                }
            }
        }
    }
}