using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaperSoccer.Extensions
{
	public static class ComboBoxExtentions
	{
		public static void FillWithColors(this ComboBox destination)
		{
			var humanFriendlyColors = typeof(Brushes).GetProperties()
				.Where(x => x.PropertyType.FullName == "System.Windows.Media.SolidColorBrush")
				.Select(x => x.Name)
				.Where(name => 
					!name.Contains("White")
					&& !name.Contains("Light")
					&& !name.Contains("Black")
					&& !name.Contains("Dark"))
				.ToList();

			foreach (var name in humanFriendlyColors) 
				destination.Items.Add(name);
		}
	}
}