namespace ACADPlugin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    using Autodesk.AutoCAD.ApplicationServices;
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
                using (var transaction = document.StartTransaction())
                {
                    var layers = CreateLayerModels(transaction, database);
                    foreach (var objectId in objectIds)
                    {
                        var dbObject = transaction.GetObject(objectId, OpenMode.ForWrite, true, true) as Entity;
                        var geometry = CreateGeometry(dbObject);
                        var layer = layers.FirstOrDefault(l => l.Id == geometry.LayerId);
                        layer?.Geometries.Add(geometry);
                    }

                    var test = layers.Where(l => l.Geometries.Count > 1).ToList();
                    foreach (var layerDto in test)
                    {
                        layerDto.AssignLayerName();
                    }

                    test.Add(new LayerViewModel());
                    var layersDtos = new Drawing
                    {
                        Layers = test
                    };

                    var window = new UserControl1
                    {
                        DataContext = layersDtos
                    };

                    if (Utilities.ShowDialog(window) == true)
                        transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        /// Функция создания моделей примитивов.
        /// </summary>
        /// <param name="dbObject"> Объект чертежа. </param>
        /// <returns> Возвращает модель примитива, приведенную к базовому классу <see cref="GeometryViewModel"/></returns>
        private static GeometryViewModel CreateGeometry(DBObject dbObject)
        {
            switch (dbObject)
            {
                case DBPoint point:
                    return new PointDto(point);
                case Line line:
                    return new LineDto(line);
                case Circle circle:
                    return new CircleDto(circle);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Создать вью-модель для каждого слоя.
        /// </summary>
        /// <param name="transaction"> Транзация. </param>
        /// <param name="database"> База данных. </param>
        /// <returns> Возвращает коллекцию моделей. </returns>
        private static List<LayerViewModel> CreateLayerModels(Transaction transaction, Database database)
        {
            var layers = new List<LayerViewModel>();
            var symbolTable = (SymbolTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead);
            foreach (var objectId in symbolTable)
            {
                try
                {
                    var layerTableRecord = (LayerTableRecord)transaction.GetObject(objectId, OpenMode.ForRead);
                    layers.Add(new LayerViewModel(layerTableRecord));
                }
                catch
                {
                    // Когда-то были обнаружены случаи, что нельзя было разлочить слой Но сделанная раннее проверка не являлась верной. Её пришлось убрать.
                    // Чтобы избежать возможных ошибок, добавлен try-catch с пустой обработкой.
                }
            }

            return layers;
        }
    }
}