namespace ACADPlugin.Model
{
    using System.Collections.Generic;

    using ACADPlugin.Command;
    using ACADPlugin.ViewModel;

    public class DrawingViewModel
    {
        public static string WindowTitle => "Test";
        public List<LayerViewModel> LayerViewModels { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Команда изменения примитива.
        /// </summary>
        public EditCommand EditCommand { get; set; } = new EditCommand();
    }

    public class SuperDto
    {
        public List<DrawingViewModel> Super { get; set; }
    }
}