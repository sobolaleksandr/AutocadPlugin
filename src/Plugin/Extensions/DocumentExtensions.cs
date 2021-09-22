namespace ACADPlugin.Extensions
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Расширения для acad-документа.
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Перегрузка метода запуска транзакции.
        /// </summary>
        /// <param name="document">Acad-документ.</param>
        /// <returns>Возвращает созданную транзакцию.</returns>
        public static Transaction StartTransaction(this Document document)
        {
            return document.Database.TransactionManager.StartTransaction();
        }
    }
}