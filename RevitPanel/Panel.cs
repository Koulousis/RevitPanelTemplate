using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;

namespace RevitPanel
{
	public class Panel : IExternalApplication
	{
		public Result OnStartup(UIControlledApplication application)
		{
			string assemblyPath = @"C:\Users\aris_\Desktop\RevitCommandTemplate\RevitCommand\bin\Debug\RevitCommand.dll";
			string className = "RevitCommand.Command";

			RibbonPanel ribbonPanel = application.CreateRibbonPanel("Addins");
			CreatePushButton(ribbonPanel);
			CreateSplitButton(ribbonPanel);
			CreatePulldownButton(ribbonPanel);
			CreateComboBox(ribbonPanel);
			CreateTextBox(ribbonPanel);

			return Result.Succeeded;
		}

		public Result OnShutdown(UIControlledApplication application)
		{
			return Result.Succeeded;
		}

		/// <summary>
		/// A PushButton is a simple button control that triggers an external command when clicked.
		/// </summary>
		void CreatePushButton(RibbonPanel ribbonPanel)
		{
			string assemblyPath = @"C:\Users\aris_\Desktop\RevitCommandTemplate\RevitCommand\bin\Debug\RevitCommand.dll";
			string className = "RevitCommand.Command";
			PushButtonData buttonData = new PushButtonData("MyButton", "My Button", assemblyPath, className);
			PushButton button = ribbonPanel.AddItem(buttonData) as PushButton;
		}

		/// <summary>
		/// A SplitButton is a button control that consists of two parts: a primary button and a drop-down menu.
		/// The primary button triggers an external command, while the drop-down menu provides additional commands.
		/// </summary>
		void CreateSplitButton(RibbonPanel ribbonPanel)
		{
			SplitButtonData splitButtonData = new SplitButtonData("MySplitButton", "My Split Button");
			SplitButton splitButton = ribbonPanel.AddItem(splitButtonData) as SplitButton;

			// Add additional commands to the drop-down menu
			PushButtonData buttonData1 = new PushButtonData("Command1", "Command 1", Assembly.GetExecutingAssembly().Location, "MyNamespace.Command1");
			PushButtonData buttonData2 = new PushButtonData("Command2", "Command 2", Assembly.GetExecutingAssembly().Location, "MyNamespace.Command2");
			splitButton.AddPushButton(buttonData1);
			splitButton.AddPushButton(buttonData2);
		}

		/// <summary>
		/// A PulldownButton is a button control that displays a drop-down menu with additional commands when clicked.
		/// </summary>
		void CreatePulldownButton(RibbonPanel ribbonPanel)
		{
			PulldownButtonData pulldownButtonData = new PulldownButtonData("MyPulldownButton", "My Pulldown Button");
			PulldownButton pulldownButton = ribbonPanel.AddItem(pulldownButtonData) as PulldownButton;

			// Add additional commands to the drop-down menu
			PushButtonData buttonData1 = new PushButtonData("Command1", "Command 1", Assembly.GetExecutingAssembly().Location, "MyNamespace.Command1");
			PushButtonData buttonData2 = new PushButtonData("Command2", "Command 2", Assembly.GetExecutingAssembly().Location, "MyNamespace.Command2");
			pulldownButton.AddPushButton(buttonData1);
			pulldownButton.AddPushButton(buttonData2);
		}

		/// <summary>
		/// A ComboBox is a drop-down control that allows users to select an item from a list.
		/// </summary>
		void CreateComboBox(RibbonPanel ribbonPanel)
		{
			ComboBoxData comboBoxData = new ComboBoxData("MyComboBox");
			Autodesk.Revit.UI.ComboBox comboBox = ribbonPanel.AddItem(comboBoxData) as Autodesk.Revit.UI.ComboBox;

			// Add items to the ComboBox
			ComboBoxMemberData item1 = new ComboBoxMemberData("Item1", "Item 1");
			ComboBoxMemberData item2 = new ComboBoxMemberData("Item2", "Item 2");
			comboBox.AddItem(item1);
			comboBox.AddItem(item2);

			// Set the ComboBox event handler
			comboBox.CurrentChanged += ComboBox_CurrentChanged;

			// Event handler method
			void ComboBox_CurrentChanged(object sender, ComboBoxCurrentChangedEventArgs e)
			{
				// Handle the ComboBox item selection
			}
		}

		/// <summary>
		/// A TextBox is a control that allows users to enter text.
		/// </summary>
		void CreateTextBox(RibbonPanel ribbonPanel)
		{
			TextBoxData textBoxData = new TextBoxData("MyTextBox");
			Autodesk.Revit.UI.TextBox textBox = ribbonPanel.AddItem(textBoxData) as TextBox;
		}
	}
}