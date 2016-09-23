using System;
using System.ServiceModel.Channels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
namespace HackathonTrial
{
	public class LocationManager
	{
		public static LocationManager locationManager;

		double x = 37.797534;
		double y = -122.401827;
		double dx = 00.000200;
		double dy = -00.000200;


		LocationManager ()
		{
		}

		public static LocationManager getSharedManager () {
			if (locationManager == null) {
				locationManager = new LocationManager ();
			}
			return locationManager;
		}

		public async void startNavigation () { 
			
			for (int i = 0; i < 10; i++) {
				x += dx;
				y += dy;
				var position = new Position (x, y);
				MessagingCenter.Send (this,"LocationChanged",position);
				//customMap.RouteCoordinates.Add (new Position (x, y));
				await System.Threading.Tasks.Task.Delay (1000);
			}
		}
		 
	}
}

