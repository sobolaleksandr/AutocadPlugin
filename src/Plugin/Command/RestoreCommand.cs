namespace ACADPlugin.Command
{
    using System;
    using System.Globalization;
    using System.Windows.Input;

    using ACADPlugin.ViewModel;

    /// <summary>
    /// Команда восстанововления примитива.
    /// </summary>
    public class RestoreCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true; // Оставил такую реализацию
        }

        public event EventHandler CanExecuteChanged; // Не использовал

        /// <summary>
        /// Восстановление примитива.
        /// </summary>
        /// <param name="parameter"> Вызывающий примитив. </param>
        public void Execute(object parameter)
        {
            if (!(parameter is ViewModelBase vm))
                return;

            vm.IsChanged = false;
            switch (vm)
            {
                case CircleViewModel circleVM:
                    circleVM.X = circleVM.Circle.Center.X.ToString("0.00", new CultureInfo("en-US"));
                    circleVM.Y = circleVM.Circle.Center.Y.ToString("0.00", new CultureInfo("en-US"));
                    circleVM.Z = circleVM.Circle.Center.Z.ToString("0.00", new CultureInfo("en-US"));
                    circleVM.Radius = circleVM.Circle.Radius.ToString("0.00", new CultureInfo("en-US"));
                    break;
                case LayerViewModel layerVM:
                    layerVM.Name = layerVM.Layer.Name;
                    layerVM.LayerColor = layerVM.Layer.Color;
                    layerVM.Visibility = !layerVM.Layer.IsOff;
                    break;
                case PointViewModel pointVM:
                    pointVM.X = pointVM.Point.Position.X.ToString("0.00", new CultureInfo("en-US"));
                    pointVM.Y = pointVM.Point.Position.Y.ToString("0.00", new CultureInfo("en-US"));
                    pointVM.Z = pointVM.Point.Position.Z.ToString("0.00", new CultureInfo("en-US"));
                    break;
                case LineViewModel lineVM:
                    var startPoint = lineVM.Line.StartPoint;
                    lineVM.StartX = startPoint.X.ToString("0.00", new CultureInfo("en-US"));
                    lineVM.StartY = startPoint.Y.ToString("0.00", new CultureInfo("en-US"));
                    var endPoint = lineVM.Line.EndPoint;
                    lineVM.EndX = endPoint.X.ToString("0.00", new CultureInfo("en-US"));
                    lineVM.EndY = endPoint.Y.ToString("0.00", new CultureInfo("en-US"));
                    lineVM.Height = Math.Min(startPoint.Z, endPoint.Z).ToString("0.00", new CultureInfo("en-US"));
                    break;
            }
        }
    }
}