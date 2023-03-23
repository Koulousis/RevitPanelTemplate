using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RevitPanel
{
	public class PushBtnAttr
	{
		public string Title { get; }
		public string AssemblyPath { get; }
		public string ClassName { get; }
		public string ToolTip { get; set; }
		public string LongDescription { get; set; }
		public BitmapImage Image { get; set; }
		public BitmapImage LargeImage { get; set; }


		public PushBtnAttr(string title, string assemblyPath, string className , string toolTip = "", string longDescription = "", string imagePath = "", string largeImagePath = "")
		{
			this.Title = title;
			this.AssemblyPath = assemblyPath;
			this.ClassName = className;
			this.ToolTip = toolTip;
			this.LongDescription = longDescription;
			this.Image = File.Exists(imagePath) ? new BitmapImage(new Uri(imagePath)) : null;
			this.LargeImage = File.Exists(largeImagePath) ? new BitmapImage(new Uri(largeImagePath)) : null;
		}
	}
}
