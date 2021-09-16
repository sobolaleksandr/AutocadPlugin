namespace ACADPlugin
{
    using System.Collections.Generic;
    using System.Linq;

    using Autodesk.AutoCAD.DatabaseServices;

    /// <summary>
    /// Расширения для работы с базой данных чертежа.
    /// </summary>
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Получить идентификаторы всех объектов чертежа.
        /// </summary>
        /// <param name="database">База чертежа.</param>
        /// <returns>Возвращает список идентификаторов чертежа.</returns>
        public static List<ObjectId> GetAllEntities(this Database database)
        {
            if (database.TransactionManager.TopTransaction != null)
                return database.GetAllEntities(database.TransactionManager.TopTransaction);

            using (var transaction = database.StartTransaction())
            {
                return database.GetAllEntities(transaction);
            }
        }

        /// <summary>
        /// Получить идентификаторы всех объектов чертежа.
        /// </summary>
        /// <param name="database">База чертежа.</param>
        /// <param name="transaction">Транзакция.</param>
        /// <returns>Возвращает список идентификаторов чертежа.</returns>
        private static List<ObjectId> GetAllEntities(this Database database, Transaction transaction)
        {
            var btr = database.GetModelSpaceBtr(transaction, OpenMode.ForRead);
            return btr.Cast<ObjectId>().ToList();
        }

        /// <summary>
        /// Получить запись таблицы блоков пространства чертежа, в котором хранится вся геометрия.
        /// </summary>
        /// <param name="database">База чертежа.</param>
        /// <param name="transaction">Транзакция чертежа.</param>
        /// <param name="openMode">Режим доступа.</param>
        /// <returns>Возвращает запись таблицы блоков пространства чертежа.</returns>
        private static BlockTableRecord GetModelSpaceBtr(this Database database, Transaction transaction,
            OpenMode openMode)
        {
            var bt = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
            return (BlockTableRecord)transaction.GetObject(bt[BlockTableRecord.ModelSpace], openMode);
        }

        /// <summary>
        /// Запустить транзакцию.
        /// </summary>
        /// <param name="database">База чертежа.</param>
        /// <returns>Возвращает созданную транзакцию.</returns>
        private static Transaction StartTransaction(this Database database)
        {
            return database.TransactionManager.StartTransaction();
        }
    }
}