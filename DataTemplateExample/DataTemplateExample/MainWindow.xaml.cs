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

namespace DataTemplateExample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public List<LogoClass> LogoList
		{
			get
			{
				List<LogoClass> result = new List<LogoClass>();
				result.Add(new LogoClass()
				{
					LogoType = "car",
					LogoImage = new BitmapImage(
						new Uri("Logos/BMW.jpg", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "beverage",
					LogoImage = new BitmapImage(
						new Uri("Logos/Coke.png", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "subway",
					LogoImage = new BitmapImage(
						new Uri("Logos/London Underground.jpg", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "organization",
					LogoImage = new BitmapImage(
						new Uri("Logos/NASA.jpg", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "organization",
					LogoImage = new BitmapImage(
						new Uri("Logos/NOAA.jpg", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "beverage",
					LogoImage = new BitmapImage(
						new Uri("Logos/Red Bull.jpg", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "beverage",
					LogoImage = new BitmapImage(
						new Uri("Logos/Starbucks.jpg", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "beverage",
					LogoImage = new BitmapImage(
						new Uri("Logos/Turkish Coffee.jpg", UriKind.Relative))
				});
				result.Add(new LogoClass()
				{
					LogoType = "car",
					LogoImage = new BitmapImage(
						new Uri("Logos/VW.jpg", UriKind.Relative))
				});

				return result;
			}
		}
		public MainWindow()
		{
			InitializeComponent();

			DataContext = this;
		}
	}
	public class LogoClass
	{
		public string LogoType { get; set; }
		public BitmapImage LogoImage { get; set; }
	}
	public class LogoListDataTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			FrameworkElement element = container as FrameworkElement;
			if (element != null && item != null && item is LogoClass)
			{
				LogoClass logo_item = item as LogoClass;

				if (logo_item.LogoType == "beverage")
				{
					return element.FindResource("beverage_template") as DataTemplate;
				}
				else if (logo_item.LogoType == "car")
				{
					return element.FindResource("car_template") as DataTemplate;
				}
				else if (logo_item.LogoType == "organization")
				{
					return element.FindResource("organization_template") as DataTemplate;
				}
				else
				{
					return element.FindResource("ordinary_template") as DataTemplate;
				}
			}
			return null;
		}
	}
}
