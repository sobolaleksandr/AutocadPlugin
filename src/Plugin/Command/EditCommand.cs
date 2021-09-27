﻿namespace ACADPlugin.Command
{
    using System;
    using System.Windows.Input;

    using ACADPlugin.Model;
    using ACADPlugin.Utilities;
    using ACADPlugin.View;
    using ACADPlugin.ViewModel;

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
                    {
                        point.IsChanged = vm.IsChanged;
                        var x = double.Parse(vm.X);
                        var y = double.Parse(vm.Y);
                        var z = double.Parse(vm.Z);
                        point.Position = new Point3d(x, y, z);
                        point.SetInformation();
                        point.DrawingModel.CommitCommand.IsChanged = true;
                    }

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
                    {
                        circle.IsChanged = vm.IsChanged;
                        var x = double.Parse(vm.X);
                        var y = double.Parse(vm.Y);
                        var z = double.Parse(vm.Z);
                        circle.Center = new Point3d(x, y, z);
                        circle.Radius = double.Parse(vm.Radius);
                        circle.SetInformation();
                        circle.DrawingModel.CommitCommand.IsChanged = true;
                    }

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
                    {
                        layer.IsChanged = vm.IsChanged;
                        layer.Visibility = vm.Visibility;
                        layer.Name = vm.Name;
                        layer.Color = vm.LayerColor;
                        layer.SetTypeName();
                        layer.DrawingModel.CommitCommand.IsChanged = true;
                    }

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
                    {
                        line.IsChanged = vm.IsChanged;
                        var startX = double.Parse(vm.StartX);
                        var startY = double.Parse(vm.StartY);
                        var endX = double.Parse(vm.EndX);
                        var endY = double.Parse(vm.EndY);
                        var height = double.Parse(vm.Height);
                        line.StartPoint = new Point3d(startX, startY, height);
                        line.EndPoint = new Point3d(endX, endY, height);
                        line.SetInformation();
                        line.DrawingModel.CommitCommand.IsChanged = true;
                    }

                    break;
                }
            }
        }
    }
}