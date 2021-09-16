namespace ACADPlugin
{
    using System.Collections.Generic;

    /// <summary>
    /// Модель чертежа.
    /// </summary>
    public class Drawing
    {
        /// <summary>
        /// Слои, содержащие объекты.
        /// </summary>
        public List<LayerViewModel> Layers { get; set; }
    }
}