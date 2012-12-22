using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;//for saving

namespace BindingtoXML
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		private void save_data_button_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.FileName = "";
			dialog.DefaultExt = ".xml";
			dialog.Filter = "XML Documents (.xml)|*.xml";

			bool? result = dialog.ShowDialog();

			if (result == true)
			{
				TheCustomerData.Document.Save(dialog.FileName);
			}
		}
	}
}
