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

                    if (DialogUtilities.ShowDialog(window) != true)
                        return;

                    CommitDrawingChanges(drawing);
                    transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        /// Принять изменения на чертеже.
        /// </summary>
        /// <param name="drawing"> Модель чертежа. </param>
        private static void CommitDrawingChanges(DrawingModel drawing)
        {
            foreach (var layer in drawing.Layers)
            {
                layer.Commit();
            }

            var layerModels = drawing.Layers.Where(layer => layer.Geometries != null).ToList();
            var geometries = layerModels
                .SelectMany(layer => layer.Geometries.Where(geometry => geometry.IsChanged)).ToList();

            foreach (var geometry in geometries)
            {
                geometry.Commit();
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
            var drawingModel = new DrawingModel(layers);
            foreach (var objectId in objectIds)
            {
                var dbObject = transaction.GetObject(objectId, OpenMode.ForWrite, true, true);
                var geometry = CreateGeometry(dbObject);
                var layer = layers.FirstOrDefault(l => l.Id == geometry?.LayerId);
                layer?.AddEntity(geometry);
            }

            return drawingModel;
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
        /// <param name="database"> База чертежа. </param>
        /// <returns> Возвращает коллекцию моделей. </returns>
        private static List<LayerModel> CreateLayerModels(Transaction transaction, Database database)
        {
            var layers = new List<LayerModel>();
            var layerIds = GetLayerIds(database);

            foreach (var layerId in layerIds)
            {
                try
                {
                    var layerTableRecord = (LayerTableRecord)transaction.GetObject(layerId, OpenMode.ForWrite);
                    var wasLocked = layerTableRecord.IsLocked;
                    layerTableRecord.IsLocked = false;
                    layers.Add(new LayerModel(layerTableRecord, wasLocked));
                }
                catch (Exception)
                {
                    // Во время отладки здесь возникали ошибки ACAD.
                }
            }

            return layers;
        }

        /// <summary>
        ///   Получить текущую таблицу слоёв.
        /// </summary>
        /// <param name="database">База чертежа.</param>
        /// <param name="transaction">Транзакция.</param>
        /// <param name="openMode">Режим открытия. По умолчанию - для чтения.</param>
        /// <returns>Возвращает таблицу слоёв.</returns>
        private static LayerTable GetCurrentLayerTable(Database database, Transaction transaction,
            OpenMode openMode = OpenMode.ForRead)
        {
            return (LayerTable)transaction.GetObject(database.LayerTableId, openMode);
        }

        /// <summary>
        ///   Получить идентификаторы всех слоёв.
        /// </summary>
        /// <param name="database">База чертежа.</param>
        /// <returns>Возвращает список идентификаторов.</returns>
        private static List<ObjectId> GetLayerIds(Database database)
        {
            using (var transaction = database.StartTransaction())
            {
                var layerTable = GetCurrentLayerTable(database, transaction);
                return layerTable.OfType<ObjectId>().ToList();
            }
        }
    }
}