namespace ACADPlugin.Model
{
    using System.Collections.Generic;

    using ACADPlugin.Command;

    /// <summary>
    /// Модель чертежа.
    /// </summary>
    public class DrawingModel
    {
        /// <summary>
        /// Модель чертежа.
        /// </summary>
        public DrawingModel(List<LayerModel> layers)
        {
            Layers = layers;
            EditCommand = new EditCommand();
        }

        /// <summary>
        /// Команда изменения примитива.
        /// </summary>
        public EditCommand EditCommand { get; set; }

        /// <summary>
        /// Заголовок дерева.
        /// </summary>
        public static string Header => "Слои";

        /// <summary>
        /// Слои, содержащие объекты.
        /// </summary>
        public List<LayerModel> Layers { get; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "Autocad Plugin";
    }
}