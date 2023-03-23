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
	public class AddinAttr
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public string AssemblyPath { get; set; }
		public string ClassName { get; set; }
		public string ToolTip { get; set; }
		public string LongDescription { get; set; }
		public BitmapImage Image { get; set; }
		public BitmapImage LargeImage { get; set; }

		/// <remarks>
		/// Image property: typically 16x16 pixels.
		/// Large Image property: typically 32x32 pixels.
		/// </remarks>
		public AddinAttr()
		{
			
		}
	}
}
