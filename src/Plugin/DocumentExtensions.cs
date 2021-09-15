namespace ACADPlugin
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;

    public static class DocumentExtensions
    {
        public static Transaction StartTransaction(this Document document)
        {
            return document.Database.TransactionManager.StartTransaction();
        }
    }
}