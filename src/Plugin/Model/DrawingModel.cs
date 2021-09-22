namespace ACADPlugin.Model
{
    using System.Collections.Generic;

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
            foreach (var layerDto in layers)
            {
                layerDto.AssignLayerName();
            }

            Layers = layers;
        }

        /// <summary>
        /// Слои, содержащие объекты.
        /// </summary>
        public List<LayerModel> Layers { get; }
    }
}