namespace ACADPlugin.Extensions
{
    /// <summary>
    /// Расширения для работы со строками.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Конвентирует строку в число.
        /// </summary>
        /// <param name="value"> Стрковое значение. </param>
        /// <returns> Возвращает число с двойной точностью. </returns>
        public static double ToDouble(this string value)
        {
            return double.Parse(value.Replace(",", "."));
        }
    }
}