#region Header
/* MEP Quantity
 * Author:  Ing. Renzo Rios Rugel (RiversCode)
 * Updated: 14/12/2022
 * Youtube: Riverscode
 * Blog:    https://lambda.com.pe/blog/cuantificar-por-modelo-generico    
 */
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

// Revit API Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using MEPQuantity;
#endregion

namespace MEPQuantity
{
    [Transaction(TransactionMode.ReadOnly)]
    public class CmdMEPQuantity : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData,
                                            ref string message,
                                            ElementSet elements)
        {
            // Access the document
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            // User select the generic model
            ElementId genericModelElementId = uiDoc.Selection.PickObject(ObjectType.Element, "Select element").ElementId;
            Element genericModelElement = doc.GetElement(genericModelElementId);

            BoundingBoxXYZ bb = genericModelElement.get_BoundingBox(doc.ActiveView); // Get the BoundingBox from generic model
            Outline outline = new Outline(bb.Min, bb.Max);// Generate Outline from Bounding Box

            BoundingBoxIntersectsFilter bbfilter = new BoundingBoxIntersectsFilter(outline); // Create filter rule from BoundingBox

            // Create list with generic model element
            ICollection<ElementId> idsExclude = new List<ElementId>();
            idsExclude.Add(genericModelElement.Id);

            // Filter elements with boundingBox filter rule and exclude the generic model element
            FilteredElementCollector elementInCurrentViewCollector = new FilteredElementCollector(doc, doc.ActiveView.Id); // All element in current active view
            List<Element> intersectedElements = elementInCurrentViewCollector.Excluding(idsExclude).WherePasses(bbfilter).ToList(); // analysis elements

            // if there is no intersecting element
            if (intersectedElements.Count == 0)
            {
                Utils.ShowMessage("Not element interseted", MessageType.Warning);
                return Result.Cancelled;
            }

            // Create solid from generic model
            Solid enviromentSolid = Utils.GetSolidElement(doc, genericModelElement);

            // Excel export
            Excel.Application xlApp = new Excel.Application(); // Create Aplication Excel Object

            xlApp.Visible = false; // Hide Excel Aplication
            xlApp.DisplayAlerts = false; // Hide Excel Alert

            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(Type.Missing); // Create a Workbook
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1); // Select the first Worksheets on workbook

            // Write information on worksheet
            int rowIndex = 4; // row start
            xlWorkSheet.Cells[rowIndex - 1, 2] = "Category";
            xlWorkSheet.Cells[rowIndex - 1, 3] = "Quantity";

            foreach (Element intersectedElement in intersectedElements)
            {
                Solid intersectedSolid = Utils.GetSolidElement(doc, intersectedElement); // Create solid from current intersectedElement

                if (intersectedSolid == null) continue; // if the current intersectedElement don't have solid

                Solid intersectSolid = BooleanOperationsUtils.ExecuteBooleanOperation(intersectedSolid, enviromentSolid, BooleanOperationsType.Intersect); // create solid from intersect from generic model solid and intersected element

                if (intersectSolid == null) continue; // if the current intersectedElement don't have solid

                double solidPercentage = intersectSolid.Volume / intersectedSolid.Volume; // Percentage of intersect
                MEPQuantityHandler.exportToExcel(xlWorkSheet, MEPQuantityHandler.getElementInformation(intersectedElement, solidPercentage), rowIndex);
                rowIndex++;
            }

            xlApp.Visible = true;

            return Result.Succeeded;
        }

    }

}