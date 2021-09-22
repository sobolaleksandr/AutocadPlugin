namespace ACADPlugin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    using ACADPlugin.Extensions;
    using ACADPlugin.Model;
    using ACADPlugin.Utilities;
    using ACADPlugin.View;

    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Runtime;

    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <summary>
    /// Плагин для AutoCad
    /// </summary>
    public class Plugin : IExtensionApplication
    {
        /// <summary>
        /// Функция инициализации. Оставил пустую реализацию
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Функция завершения работы. Оставил пустую реализацию
        /// </summary>
        public void Terminate()
        {
        }

        /// <summary>
        /// Функция редактирования слоёв и примитивов
        /// </summary>
        [CommandMethod("AUTOCAD")]
        public void Execute()
        {
            try
            {
                var document = Application.DocumentManager.MdiActiveDocument;
                var database = document.Database;
                var objectIds = database.GetAllEntities();

                using (document.LockDocument())
                using (var transaction = document.StartTransaction())
                {
                    var layers = CreateLayerModels(transaction, database);
                    var drawing = CreateDrawing(objectIds, transaction, layers);
                    var window = new LayersView
                    {
                        DataContext = drawing
                    };

                    if (DialogUtilities.ShowDialog(window) == true)
                        transaction.Commit();

                    //Блокируем все слои.
                    var layerIds = GetLayerIds(transaction, database);
                    foreach (var layerId in layerIds)
                    {
                        LockLayer(transaction, true, layerId);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        /// Функция создания объекта чертежа. 
        /// </summary>
        /// <param name="objectIds"> Id-объектов на чертеже. </param>
        /// <param name="transaction"> Транзакция. </param>
        /// <param name="layers"> Список слоев, на которые необходимо добавить объекты. </param>
        /// <returns> Возращает чертеж со слоями с соответствующии объектами. </returns>
        private static DrawingModel CreateDrawing(IEnumerable<ObjectId> objectIds, Transaction transaction,
            List<LayerModel> layers)
        {
            foreach (var objectId in objectIds)
            {
                var dbObject = transaction.GetObject(objectId, OpenMode.ForWrite, true, true);
                var geometry = CreateGeometry(dbObject);
                var layer = layers.FirstOrDefault(l => l.Id == geometry?.LayerId);
                layer?.Geometries.Add(geometry);
            }

            // Больше одного т.к. мы добавляем первым объектом сам слой в коллекцию объектов
            //var nonEmptyLayers = layers.Where(l => l.Geometries.Count > 1).ToList();

            return new DrawingModel(layers);
        }

        /// <summary>
        /// Функция создания моделей примитивов.
        /// </summary>
        /// <param name="dbObject"> Объект чертежа. </param>
        /// <returns> Возвращает модель примитива, приведенную к базовому классу <see cref="GeometryModel"/></returns>
        private static GeometryModel CreateGeometry(DBObject dbObject)
        {
            switch (dbObject)
            {
                case DBPoint point:
                    return new PointModel(point);
                case Line line:
                    return new LineModel(line);
                case Circle circle:
                    return new CircleModel(circle);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Создать модели для каждого слоя.
        /// </summary>
        /// <param name="transaction"> Транзация. </param>
        /// <param name="database"> База данных. </param>
        /// <returns> Возвращает коллекцию моделей. </returns>
        private static List<LayerModel> CreateLayerModels(Transaction transaction, Database database)
        {
            var layers = new List<LayerModel>();
            var layerIds = GetLayerIds(transaction, database);
            foreach (var layerId in layerIds)
            {
                try
                {
                    var layerTableRecord = LockLayer(transaction, false, layerId);
                    layers.Add(new LayerModel(layerTableRecord));
                }
                catch (Exception)
                {
                    // Когда-то были обнаружены случаи, что нельзя было разлочить слой Но сделанная раннее проверка не являлась верной. Её пришлось убрать.
                    // Чтобы избежать возможных ошибок, добавлен try-catch с пустой обработкой.
                }
            }

            return layers;
        }

        /// <summary>
        /// Блокировать/разблокировать слой.
        /// </summary>
        /// <param name="transaction"> Транзакция. </param>
        /// <param name="isLocked"> Заблокировать/разблокировать слой. </param>
        /// <param name="layerId"> Id-слоя. </param>
        /// <returns> Возвращает заблокированный/разблокированный слой. </returns>
        private static LayerTableRecord LockLayer(Transaction transaction, bool isLocked, ObjectId layerId)
        {
            var layerTableRecord = (LayerTableRecord)transaction.GetObject(layerId, OpenMode.ForWrite);
            layerTableRecord.IsLocked = isLocked;
            return layerTableRecord;
        }

        /// <summary>
        /// Получить Id-слоев с чертежа.
        /// </summary>
        /// <param name="transaction"> Транзакция. </param>
        /// <param name="database"> База данных чертежа. </param>
        /// <returns> Возвращает список Id-слоев. </returns>
        private static List<ObjectId> GetLayerIds(Transaction transaction, Database database)
        {
            var layerTable = (LayerTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead);
            return layerTable.OfType<ObjectId>().ToList();
        }
    }
}