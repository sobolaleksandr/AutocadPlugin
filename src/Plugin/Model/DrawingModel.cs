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
            foreach (var layer in layers)
            {
                layer.DrawingModel = this;
            }

            Layers = layers;
            CommitCommand = new CommitCommand();
        }

        /// <summary>
        /// Команда принятия изменений чертежа.
        /// </summary>
        public CommitCommand CommitCommand { get; set; }

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