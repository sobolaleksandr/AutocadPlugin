namespace ACADPlugin
{
    using Autodesk.AutoCAD.DatabaseServices;

    public abstract class Geometry
    {
        protected Geometry()
        {
            Height = 2d;
            EditCommand = new EditCommand();
            EditViewData = new EditViewModel
            {
                Field1 = "qwe",
                Field2 = "asd",
                Field3 = "zxc",
                Label1 = "rty",
                Label2 = "fgh",
                Label3 = "vbn",
            };
        }

        public EditCommand EditCommand { get; set; }

        public EditViewModel EditViewData { get; set; }

        public double Height { get; set; }

        public ObjectId LayerId { get; protected set; }

        public string LayerName { get; set; }

        public string Type => GetTypeName();

        protected abstract string GetTypeName();
    }
}