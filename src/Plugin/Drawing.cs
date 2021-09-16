namespace ACADPlugin
{
    using System.Collections.Generic;

    /// <summary>
    /// Модель чертежа.
    /// </summary>
    public class Drawing
    {
        /// <summary>
        /// Модель чертежа.
        /// </summary>
        public Drawing(List<LayerViewModel> layers)
        {
            foreach (var layerDto in layers)
            {
                layerDto.AssignLayerName();
            }

            Layers = layers;
        }

        /// <summary>
        /// Слои, содержащие объекты.
        /// </summary>
        public List<LayerViewModel> Layers { get; }
    }
}