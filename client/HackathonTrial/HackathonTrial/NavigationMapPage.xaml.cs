using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Xam.Plugin.MapExtend.Abstractions;
using Xam.Plugin.MapExtend.Abstractions.Models.Routes;

namespace HackathonTrial
{
	public partial class NavigationMapPage : ContentPage
	{
		CustomMap customMap;

		public NavigationMapPage ()
		{
			Button btn = new Button {
				Text = "Start"
			};

			btn.Clicked += Btn_Clicked;

			customMap = new CustomMap {
				MapType = MapType.Street,
				WidthRequest = App.ScreenWidth,
				HeightRequest = App.ScreenHeight
			};

			customMap.MoveToRegion (MapSpan.FromCenterAndRadius (new Position (37.79752, -122.40183), Xamarin.Forms.Maps.Distance.FromMiles (1.0)));

			var position = new Position (37.797534, -122.401827);

			var pin = new Pin {
				Type = PinType.Place,
				Position = position,
				Label = "Santa Cruz",
				Address = "custom detail info"
			};

			Route route = new Xam.Plugin.MapExtend.Abstractions.Models.Routes.Route ();
			Leg leg = new Leg () {
				start_address = "Shivajinagar Pune",
				end_address = "Aundh Pune"
			};

			//var map2 = new MapExtend (
			//	MapSpan.FromCenterAndRadius (
			//		position, Xamarin.Forms.Maps.Distance.FromKilometers (1))) {
			//	IsShowingUser = true,
			//	HeightRequest = 100,
			//	WidthRequest = 960,
			//	HasZoomEnabled = true,
			//	VerticalOptions = LayoutOptions.FillAndExpand
			//};

			//route.legs [0] = leg;

			var position2 = new Position (37.776831, -122.394627);

			//map2.CreateRoute (position,position2);

			customMap.Pins.Add (pin);

			StackLayout stack = new StackLayout ();
			stack.Children.Add (btn);
			stack.Children.Add (customMap);

			WebView web = new WebView () {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			web.Source = new UrlWebViewSource { Url = "http://maps.googleapis.com/maps/api/staticmap?ll=36.97,%20-122&lci=bike&z=13&t=p&size=500x500&sensor=true" };

			Content = stack;
		}

		void Btn_Clicked (object sender, EventArgs e)
		{
			var manager = LocationManager.getSharedManager ();
			manager.startNavigation ();
		}

		void ChangePosition () {
			
			//var position = locator.GetPositionAsync (timeoutMilliseconds: 500).Result;
			//refreshMap (position);
		}

		void refreshMap (Position position) { 
			customMap.RouteCoordinates.Add (position);
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			MessagingCenter.Subscribe<LocationManager,Position> (this,"LocationChanged",LocationChanged);

		}

		void LocationChanged (LocationManager arg1, Position arg2)
		{
			customMap.Pins.RemoveAt (0);
			customMap.RouteCoordinates.Add (arg2);
			var pin = new Pin {
				Type = PinType.Place,
				Position = arg2,
				Label = "Santa Cruz",
				Address = "custom detail info"
			};
			customMap.Pins.Add (pin);
		}
	}
}

