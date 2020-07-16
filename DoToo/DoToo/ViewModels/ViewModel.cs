using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DoToo.Annotations;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public INavigation Navigation;

		protected void RaisePropertyChanged(params string[] propertyNames)
		{
			foreach (var propertyName in propertyNames)
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
