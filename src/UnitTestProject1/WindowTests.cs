namespace UnitTestProject1
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using ACADPlugin.Model;
    using ACADPlugin.View;
    using ACADPlugin.ViewModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WindowTests
    {
        [TestMethod]
        public void EditWindowTest()
        {
            var vm = new LayerViewModel()
            {
                Name = "test",
                Visibility = true
            };

            var widnow = new EditLayerView
            {
                DataContext = vm
            };
            widnow.ShowDialog();
        }
    }

    
}