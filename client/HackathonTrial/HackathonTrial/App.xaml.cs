using Xamarin.Forms;

namespace HackathonTrial
{
	public partial class App : Application
	{
		public static double ScreenHeight;
		public static double ScreenWidth;

		public App ()
		{
			InitializeComponent ();

			var tabs = new TabbedPage ();

			tabs.Children.Add (new MapPage () { Title = "Map"});
			tabs.Children.Add (new CameraPage () { Title = "Camera"});
			tabs.Children.Add (new MapRoutePage () { Title="Route"});
			tabs.Children.Add (new NavigationMapPage () { Title="Nav"});

			MainPage = new NavigationMapPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
