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

using System.Globalization;

namespace ValueConverter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public string TelephoneNumber { get; set; }
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = this;
			TelephoneNumber = "3043843115";
		}
	}
	public class FormatTelephoneNumber : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string telephone_number = (string)value;
			return "(" + telephone_number.Substring(0, 3) + ") " + telephone_number.Substring(3, 3) + "-" +
				telephone_number.Substring(6);
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string formatted_telephone_number = (string)value;
			return formatted_telephone_number.Substring(1, 3) +
					formatted_telephone_number.Substring(6, 3) +
					formatted_telephone_number.Substring(10);
		}
	}
	public class BooleanToVisibilty : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool is_checked = (bool)value;
			return is_checked ? Visibility.Visible : Visibility.Hidden;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
