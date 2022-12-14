#region Namespaces
using System.Collections.Generic;
using System.Linq;

// #### PresentationCore && System.Drawing && WindowsBase ####
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

// #### Revit ####
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace MEPQuantity
{
    public class Utils
    {
        // #### GENERALS ####
        public static void ShowMessage(string message, MessageType type = MessageType.Information)
        {
            string title;
            string description;
            TaskDialogIcon icon;

            if (type == MessageType.Information)
            {
                title = "Información";
                description = "Mensaje de información";
                icon = TaskDialogIcon.TaskDialogIconInformation;
            }
            else if (type == MessageType.Warning)
            {
                title = "Advertencia";
                description = "Cuidado!";
                icon = TaskDialogIcon.TaskDialogIconWarning;
            }
            else if (type == MessageType.Error)
            {
                title = "Error";
                description = "Tenemos un Error!";
                icon = TaskDialogIcon.TaskDialogIconError;
            }
            else
            {
                title = "Title";
                description = "Nuevo mensaje";
                icon = TaskDialogIcon.TaskDialogIconNone;
            }
            TaskDialog taskDialog = new TaskDialog(title);
            taskDialog.TitleAutoPrefix = false;
            taskDialog.MainIcon = icon;
            taskDialog.MainInstruction = description;
            taskDialog.MainContent = message;
            taskDialog.FooterText = "Lambda Ingeniería & Innovación";

            taskDialog.Show();
        }

        public static (bool isDone, List<Element> selectedElement) SelectElements(UIDocument uiDoc)
        {
            List<Element> elements;
            try
            {
                IList<Reference> references = uiDoc.Selection.PickObjects(ObjectType.Element, "Seleccione un elemento");

                elements = (from reference in references select uiDoc.Document.GetElement(reference)).ToList();
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return (false, null);
            }

            if (elements.Count == 0)
            {
                ShowMessage("Usted debe seleccionar minimo un elemento", MessageType.Warning);
                return (false, null);
            }

            uiDoc.RefreshActiveView();

            return (true, elements);

        }

        // #### EXTERNAL APPLICATION HELPERS ####
        public static BitmapSource ConvertBmpToBipmapSource(Icon icon, int width, int height)
        {
            Icon resizeIcon = new Icon(icon, new System.Drawing.Size(width, height));

            return Imaging.CreateBitmapSourceFromHIcon(resizeIcon.Handle,
                                                       Int32Rect.Empty,
                                                       BitmapSizeOptions.FromEmptyOptions());

        }

        // #### GEOMTRY HANLDER ####
        public static Solid GetSolidElement(Document doc, Element el)
        {

            Options option = doc.Application.Create.NewGeometryOptions();
            option.ComputeReferences = true;
            option.IncludeNonVisibleObjects = true;
            option.View = doc.ActiveView;
            Solid solid = null;

            GeometryElement geoEle = el.get_Geometry(option) as GeometryElement;
            foreach (GeometryObject gObj in geoEle)
            {
                Solid geoSolid = gObj as Solid;
                if (geoSolid != null && geoSolid.Volume != 0)
                {
                    solid = geoSolid;
                }
                else if (gObj is GeometryInstance)
                {
                    GeometryInstance geoInst = gObj as GeometryInstance;
                    GeometryElement geoElem = geoInst.SymbolGeometry;
                    foreach (GeometryObject gObj2 in geoElem)
                    {
                        Solid geoSolid2 = gObj2 as Solid;
                        if (geoSolid2 != null && geoSolid2.Volume != 0)
                        {
                            solid = geoSolid2;
                        }
                    }
                }
            }
            return solid;
        }

    }

    public enum MessageType
    {
        Information = 1,
        Warning = 2,
        Error = 3,
    }
}
