using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
			#region Add-ins
			//Add-in data #1
			AddinAttr addinAttr = new AddinAttr()
			{
				Name = "button",
				Title = "button",
				AssemblyPath = @"C:\Users\AKoulousis\OneDrive - Petersime NV\Bureaublad\Master\RevitCommandTemplate\RevitCommand\bin\Debug\RevitCommand.dll",
				ClassName = "RevitCommand.Command",
				ToolTip = "Description",
				LongDescription = "Long description",
				Image = File.Exists(GetImagePath("Bitmap 16x16.png")) ? new BitmapImage(new Uri(GetImagePath("Bitmap 16x16.png"))) : null,
				LargeImage = File.Exists(GetLargeImagePath("Bitmap 32x32.png")) ? new BitmapImage(new Uri(GetLargeImagePath("Bitmap 32x32.png"))) : null
			};
			PushButtonData addinData = CreateButtonData(addinAttr);

			//Add-in data #2

			//Add-in data #3

			//Add-in data #n
			#endregion

			#region UI Elements
			//Ribbon panel
			RibbonPanel ribbonPanel = application.CreateRibbonPanel("Addins");

			//Push buttons
			CreateRibbonPushButton(ribbonPanel, addinData);

			//Split buttons
			SplitButtonAttr splitButtonAttr = new SplitButtonAttr()
			{
				Name = "splitButton",
				Title = "button",
				ToolTip = "Description",
				LongDescription = "Long description",
				Image = File.Exists(GetImagePath("Bitmap 16x16.png")) ? new BitmapImage(new Uri(GetImagePath("Bitmap 16x16.png"))) : null,
				LargeImage = File.Exists(GetLargeImagePath("Bitmap 32x32.png")) ? new BitmapImage(new Uri(GetLargeImagePath("Bitmap 32x32.png"))) : null
			};
			SplitButtonData splitButtonData = CreateSplitButtonData(splitButtonAttr);
			CreateRibbonSplitButtons(ribbonPanel, splitButtonData, new List<PushButtonData>() { addinData });

			//Pulldown buttons
			PulldownButtonAttr pulldownButtonAttr = new PulldownButtonAttr()
			{
				Name = "pulldownButton",
				Title = "button",
				ToolTip = "Description",
				LongDescription = "Long description",
				Image = File.Exists(GetImagePath("Bitmap 16x16.png")) ? new BitmapImage(new Uri(GetImagePath("Bitmap 16x16.png"))) : null,
				LargeImage = File.Exists(GetLargeImagePath("Bitmap 32x32.png")) ? new BitmapImage(new Uri(GetLargeImagePath("Bitmap 32x32.png"))) : null
			};
			PulldownButtonData pulldownButtonData = CreatePulldownButtonData(pulldownButtonAttr);
			CreateRibbonPulldownButtons(ribbonPanel, pulldownButtonData, new List<PushButtonData>() { addinData });
			
			CreateComboBox(ribbonPanel);

			CreateTextBox(ribbonPanel);
			#endregion
			
			return Result.Succeeded;
		}

		public Result OnShutdown(UIControlledApplication application)
		{
			return Result.Succeeded;
		}

		#region Buttons data
		PushButtonData CreateButtonData(AddinAttr buttonAttr)
		{
			PushButtonData pushButtonData = new PushButtonData(buttonAttr.Name, buttonAttr.Title, buttonAttr.AssemblyPath, buttonAttr.ClassName)
			{
				ToolTip = buttonAttr.ToolTip,
				LongDescription = buttonAttr.LongDescription,
				Image = buttonAttr.Image,
				LargeImage = buttonAttr.LargeImage
			};

			return pushButtonData;
		}

		SplitButtonData CreateSplitButtonData(SplitButtonAttr splitButtonAttr)
		{
			SplitButtonData splitButtonData = new SplitButtonData(splitButtonAttr.Name, splitButtonAttr.Title)
			{
				ToolTip = splitButtonAttr.ToolTip,
				LongDescription = splitButtonAttr.LongDescription,
				Image = splitButtonAttr.Image,
				LargeImage = splitButtonAttr.LargeImage
			};

			return splitButtonData;
		}

		PulldownButtonData CreatePulldownButtonData(PulldownButtonAttr pulldownButtonAttr)
		{
			PulldownButtonData pulldownButtonData = new PulldownButtonData(pulldownButtonAttr.Name, pulldownButtonAttr.Title)
			{
				ToolTip = pulldownButtonAttr.ToolTip,
				LongDescription = pulldownButtonAttr.LongDescription,
				Image = pulldownButtonAttr.Image,
				LargeImage = pulldownButtonAttr.LargeImage
			};

			return pulldownButtonData;
		}
		#endregion

		#region Ribbon buttons
		/// <summary>
		/// A PushButton is a simple button control that triggers an external command when clicked.
		/// </summary>
		private void CreateRibbonPushButton(RibbonPanel ribbonPanel, PushButtonData pushButtonData)
		{
			PushButton pushButton = ribbonPanel.AddItem(pushButtonData) as PushButton;
		}

		/// <summary>
		/// A SplitButton is a button control that consists of two parts: a primary button and a drop-down menu.
		/// The primary button triggers an external command, while the drop-down menu provides additional commands.
		/// </summary>
		private void CreateRibbonSplitButtons(RibbonPanel ribbonPanel, SplitButtonData splitButtonData, List<PushButtonData> pushButtonDatas)
		{
			SplitButton splitButton = ribbonPanel.AddItem(splitButtonData) as SplitButton;
			foreach (PushButtonData pushButtonData in pushButtonDatas)
			{
				splitButton.AddPushButton(pushButtonData);
			}
		}

		/// <summary>
		/// A PulldownButton is a button control that displays a drop-down menu with additional commands when clicked.
		/// </summary>
		private void CreateRibbonPulldownButtons(RibbonPanel ribbonPanel, PulldownButtonData pulldownButtonData, List<PushButtonData> pushButtonDatas)
		{
			PulldownButton pullDownButton = ribbonPanel.AddItem(pulldownButtonData) as PulldownButton;
			foreach (PushButtonData pushButtonData in pushButtonDatas)
			{
				pullDownButton.AddPushButton(pushButtonData);
			}
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
		#endregion

		#region Other methods
		private string GetImagePath(string imageName)
		{
			string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string imagePath = Path.Combine(assemblyDirectory, "Images", imageName);

			return imagePath;
		}

		private string GetLargeImagePath(string largeImageName)
		{
			string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string largeImagePath = Path.Combine(assemblyDirectory, "LargeImages", largeImageName);

			return largeImagePath;
		}
		#endregion
	}
}