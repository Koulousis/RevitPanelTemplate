using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitPanel
{
	public class Panel : IExternalApplication
	{
		public Result OnStartup(UIControlledApplication application)
		{
			string assemblyPath = @"C:\Users\aris_\Desktop\RevitCommandTemplate\RevitCommand\bin\Debug\RevitCommand.dll";
			string className = "RevitCommand.Command";

			RibbonPanel ribbonPanel = application.CreateRibbonPanel("Addins");
			PushButtonData pushButtonData = new PushButtonData("Command", "Command", assemblyPath, className);
			PushButton pushButton = ribbonPanel.AddItem(pushButtonData) as PushButton;
			pushButton.ToolTip = "Click to run";

			return Result.Succeeded;
		}

		public Result OnShutdown(UIControlledApplication application)
		{
			return Result.Succeeded;
		}
	}
}