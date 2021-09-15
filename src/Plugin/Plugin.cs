namespace ACADPlugin
{
    using System.Collections.Generic;
    using System.Linq;

    using Autodesk.AutoCAD.ApplicationServices.Core;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.Runtime;

    public class Plugin : IExtensionApplication
    {
        public void Initialize()
        {
        }

        public void Terminate()
        {
        }

        [CommandMethod("AUTOCAD")]
        public void Execute()
        {
            var document = Application.DocumentManager.MdiActiveDocument;
            var database = document.Database;
            var objectIds = database.GetAllEntities();
            var layers = new List<LayerDto>();
            using (var transaction = document.StartTransaction())
            {
                var symTable = (SymbolTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead);
                foreach (var id in symTable)
                {
                    var symbol = (LayerTableRecord)transaction.GetObject(id, OpenMode.ForRead);
                    layers.Add(new LayerDto(symbol));
                }

                foreach (var objectId in objectIds)
                {
                    var dbObject = transaction.GetObject(objectId, OpenMode.ForWrite, true, true) as Entity;
                    var geometry = CreateGeometry(dbObject);
                    var layer = layers.FirstOrDefault(l => l.Id == geometry.LayerId);
                    layer?.Geometries.Add(geometry);
                }

                var test = layers.Where(l => l.Geometries.Count > 1).ToList();//.FirstOrDefault();
                foreach (var layerDto in test)
                {
                    layerDto.AssignLayerName();
                }

                var layersDtos = new LayersDto()
                {
                    Layers = test
                };

                var window = new UserControl1
                {
                    DataContext = test
                };

                if (Application.ShowModalWindow(window) == true)
                    transaction.Commit();
            }
        }

        private static Geometry CreateGeometry(DBObject dbObject)
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
    }

    public class LayersDto
    {
        public List<LayerDto> Layers { get; set; }
    }
}