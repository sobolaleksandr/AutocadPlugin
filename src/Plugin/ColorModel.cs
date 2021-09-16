namespace ACADPlugin
{
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Цвет сущности CADGIS.
    /// </summary>
    public class ColorModel
    {
        /// <summary>
        /// Регулярное выражение для проверки правильности цвета при парсинге из строки.
        /// </summary>
        private static readonly Regex ColorValidator;

        /// <summary>
        /// Инициализирует статические поля класса <see cref="ColorModel"/>.
        /// </summary>
        static ColorModel()
        {
            ColorValidator = new Regex("^(#[0-9A-Fa-f]{8})$");
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ColorModel"/>.
        /// </summary>
        /// <param name="r">
        /// Красный канал цвета.
        /// </param>
        /// <param name="g">
        /// Зеленый канал цвета.
        /// </param>
        /// <param name="b">
        /// Синий канал цвета.
        /// </param>
        /// <param name="a">
        /// Канал прозрачности.
        /// </param>
        public ColorModel(byte r = 0, byte g = 0, byte b = 0, byte a = 0)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Канал прозрачности.
        /// </summary>
        private byte A { get; }

        /// <summary>
        /// Синий канал цвета.
        /// </summary>
        public byte B { get; }

        /// <summary>
        /// Зеленый канал цвета.
        /// </summary>
        public byte G { get; }

        /// <summary>
        /// Красный канал цвета.
        /// </summary>
        public byte R { get; }

        public override string ToString()
        {
            return $"{R:X2}{G:X2}{B:X2}{A:X2}";
        }

        /// <summary>
        /// Попытка парсинга строки с цветом.
        /// </summary>
        /// <param name="colorString">
        /// Строка с цветом.
        /// </param>
        /// <returns>
        /// Объект цвета <see cref="ColorModel"/>.
        /// </returns>
        public static ColorModel TryParse(string colorString)
        {
            if (ColorValidator.IsMatch(colorString))
                return null;

            try
            {
                const NumberStyles style = NumberStyles.HexNumber;
                var result = new ColorModel(
                    byte.Parse(colorString.Substring(0, 2), style),
                    byte.Parse(colorString.Substring(2, 2), style),
                    byte.Parse(colorString.Substring(4, 2), style),
                    byte.Parse(colorString.Substring(6, 2), style));

                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}