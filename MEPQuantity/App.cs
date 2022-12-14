using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace MEPQuantity
{
    public class App : IExternalApplication
    {
        private string _assembly = typeof(App).Assembly.Location;

        public Result OnStartup(UIControlledApplication app)
        {

            CreateRibbonItems(app);
            return Result.Succeeded;

        }

        private void CreateRibbonItems(UIControlledApplication app)
        {
            const string TAB_NAME = "Lambda I&I";
            const string PANEL_NAME = "Commands";

            app.CreateRibbonTab(TAB_NAME);
            RibbonPanel panel = app.CreateRibbonPanel(TAB_NAME, PANEL_NAME);

            AddRibbonItems(panel);
        }

        private void AddRibbonItems(RibbonPanel panel)
        {

            // #### PUSH BUTTON INFORMATION ####
            const string BUTTON_NAME = "MEPQuantity";
            const string BUTTON_TEXT = "MEP Quantity";
            const string BUTTON_COMMAND = "MEPQuantity.CmdMEPQuantity";
            const string BUTTON_DESCRIPTION = "Cuantificar elementos MEP mediante un modelo generico";
            const string BUTTON_HELP_URL = "https://lambda.com.pe/";

            // #### CREATE PUSH BUTTON ####
            PushButtonData firstPushButtonData = new PushButtonData(BUTTON_NAME, BUTTON_TEXT, _assembly, BUTTON_COMMAND);

            firstPushButtonData.LongDescription = BUTTON_DESCRIPTION;
            firstPushButtonData.SetContextualHelp(new ContextualHelp(ContextualHelpType.Url, BUTTON_HELP_URL));

            PushButton pushButton = panel.AddItem(firstPushButtonData) as PushButton;

            // #### ADD PUSH BUTTON IMAGES ####
            pushButton.LargeImage = Utils.ConvertBmpToBipmapSource(Properties.Resources.MEPQuantity, 32, 32);
            pushButton.Image = Utils.ConvertBmpToBipmapSource(Properties.Resources.MEPQuantity, 16, 16);

        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
