using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
namespace BindingtoProperty
{
	class OtherCustomer:INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;//required by interface

		protected void OnPropertyChanged(PropertyChangedEventArgs e)//required by interface
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}
		private string _FirstName;
		public string FirstName
		{
			get { return _FirstName; }
			set
			{
				_FirstName = value;
				OnPropertyChanged(new PropertyChangedEventArgs("FirstName"));
			}
		}
		private string _LastName;
		public string LastName
		{
			get { return _LastName; }
			set
			{
				_LastName = value;
				OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
			}
		}
		private string _City;
		public string City
		{
			get { return _City; }
			set
			{
				_City = value;
				OnPropertyChanged(new PropertyChangedEventArgs("City"));
			}
		}
		private string _State;
		public string State
		{
			get { return _State; }
			set
			{
				_State = value;
				OnPropertyChanged(new PropertyChangedEventArgs("State"));
			}
		}
		private string _ZIP;
		public string ZIP
		{
			get { return _ZIP; }
			set
			{
				_ZIP = value;
				OnPropertyChanged(new PropertyChangedEventArgs("ZIP"));
			}
		}
	}
}
