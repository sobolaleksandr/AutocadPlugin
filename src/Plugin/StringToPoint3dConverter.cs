namespace ACADPlugin
{
    using Autodesk.AutoCAD.Geometry;

    public static class StringToPoint3dConverter
    {
        public static Point3d ToPoint3d(this string inputString)
        {
            var value = inputString.Trim('(', ')').Split(',');
            return new Point3d(double.Parse(value[0]), double.Parse(value[1]), double.Parse(value[2]));
        }
    }
}