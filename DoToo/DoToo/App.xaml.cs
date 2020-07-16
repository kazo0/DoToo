using System;
using DoToo.ViewModels;
using DoToo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoToo
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainView(Resolver.Resolve<MainViewModel>()));
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
