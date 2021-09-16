namespace ACADPlugin
{
    using Autodesk.AutoCAD.Geometry;

    /// <summary>
    /// Конвертер из строкового значения в <see cref="Point3d"/>
    /// </summary>
    public static class StringToPoint3dConverter
    {
        /// <summary>
        /// Конвертировать строковое значение в <see cref="Point3d"/>
        /// </summary>
        /// <param name="inputString"> Входная строка. </param>
        /// <returns> Возвращает <see cref="Point3d" /></returns>
        public static Point3d ToPoint3d(this string inputString)
        {
            var points = inputString.Trim('(', ')').Split(',');
            return new Point3d(double.Parse(points[0]), double.Parse(points[1]), double.Parse(points[2]));
        }
    }
}