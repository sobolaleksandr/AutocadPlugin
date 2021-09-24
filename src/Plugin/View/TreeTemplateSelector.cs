namespace ACADPlugin.View
{
    using System.Windows;
    using System.Windows.Controls;

    using ACADPlugin.Model;

    using Autodesk.AutoCAD.BoundaryRepresentation;

    /// <summary>
    /// Класс выбора шаблона для вью-моделей дерева элементов.
    /// </summary>
    public class TreeTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Шаблон дерева с выбором узлов по чекбоксам.
        /// </summary>
        public DataTemplate CheckableTreeTemplate { get; set; }

        /// <summary>
        /// Шаблон дерева с выбором узла.
        /// </summary>
        public DataTemplate SelectableTreeTemplate { get; set; }

        /// <summary>
        /// Получить шаблон элемента управления деревом элементов в зависимости от рабочей вью-модели.
        /// </summary>
        /// <param name="item"> Объект данных, для которого можно выбрать шаблон. </param>
        /// <param name="container"> Объект с привязкой к данным. </param>
        /// <returns>Соответствующий шаблон или null.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case null:
                    return null;

                case LayerModel _:
                    return CheckableTreeTemplate;

                case PointModel _:
                    return SelectableTreeTemplate;

                default:
                    throw new Exception();
            }
        }
    }
}