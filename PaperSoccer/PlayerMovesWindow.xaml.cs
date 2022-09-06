using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace PaperSoccer
{
	public partial class PlayerMovesWindow : Window
	{
		public PlayerMovesWindow()
		{
			InitializeComponent();

			var dt = new DataTable("Zawartość");
			
			dt.Columns.AddRange(new DataColumn[] {new("Gracz 1"), new("Gracz 2")});
			
			var dr = dt.NewRow();

			dr["Gracz 1"] = "NE, N, NW"; 
			dr["Gracz 2"] = "SE, S, S"; 
			dt.Rows.Add(dr);


//BindGridViwe

			dgPlayerMoves.ItemsSource = dt.AsDataView();
		}
	}
}