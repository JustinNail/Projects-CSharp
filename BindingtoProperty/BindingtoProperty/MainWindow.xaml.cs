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

using System.ComponentModel;
namespace BindingtoProperty
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Customer[] CustomerList = new Customer[]{
		new Customer() {FirstName="A_first",LastName="A_last",
							City = "A_city",State = "A_state",ZIP ="A_ZIP"},
		new Customer() {FirstName="B_first",LastName="B_last",
							City = "B_city",State = "B_state",ZIP ="B_ZIP"},
		new Customer() {FirstName="C_first",LastName="C_last",
							City = "C_city",State = "C_state",ZIP ="C_ZIP"} };

		OtherCustomer TheOtherCustomer = new OtherCustomer();
		public MainWindow()
		{
			InitializeComponent();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			foreach (Customer c in CustomerList)
			{
				customer_combinationbox.Items.Add(c.LastName + ", " + c.FirstName);
			}
			bottom_stackpanel.DataContext = TheOtherCustomer;
		}
		private void customer_combinationbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ((ComboBox)sender).SelectedIndex;
			top_stackpanel.DataContext = CustomerList[index];

			TheOtherCustomer.FirstName = CustomerList[index].FirstName;
			TheOtherCustomer.LastName = CustomerList[index].LastName;
			TheOtherCustomer.City = CustomerList[index].City;
			TheOtherCustomer.State = CustomerList[index].State;
			TheOtherCustomer.ZIP = CustomerList[index].ZIP;
		}
	}
}

