using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Excel = Microsoft.Office.Interop.Excel;

namespace MEPQuantity
{
    public class MEPQuantityHandler
    {
        public static ElementExport getElementInformation(Element el, double elementPercentage)
        {
            BuiltInCategory category = (BuiltInCategory)el.Category.Id.IntegerValue;

            string elementCategory = el.Category.Name;
            double quantity = 1;

            if (category == BuiltInCategory.OST_PipeCurves ||
                category == BuiltInCategory.OST_DuctCurves ||
                category == BuiltInCategory.OST_CableTray ||
                category == BuiltInCategory.OST_Conduit)
            {
                double _lengthQuantity = el.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble();
                quantity = Math.Round((_lengthQuantity * elementPercentage), 2);
            }


            return new ElementExport(elementCategory, quantity.ToString());
        }

        public static void exportToExcel(Excel.Worksheet xlWorkSheet, ElementExport elementExport, int rowIndex)
        {
            xlWorkSheet.Cells[rowIndex, 2] = elementExport.GetCategory();
            xlWorkSheet.Cells[rowIndex, 3] = elementExport.GetQuantity();
        }

    }
}
