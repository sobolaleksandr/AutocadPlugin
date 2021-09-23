namespace UnitTestProject1
{
    using ACADPlugin.View;
    using ACADPlugin.ViewModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var point = new PointViewModel("sad") { X = "10", Y = "20", Z = "30" };
            var window = new UserControl1 { DataContext = point };
            window.ShowDialog();
        }
    }
}