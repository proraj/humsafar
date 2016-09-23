﻿using System;
using CoreLocation;
using HackathonTrial;
using MapKit;
using ObjCRuntime;
using UIKit;
using WorkingWithMaps.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (CustomMap), typeof (CustomMapRenderer))]
namespace WorkingWithMaps.iOS
{
	public class CustomMapRenderer : MapRenderer
	{
		MKPolylineRenderer polylineRenderer;

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) {
				var nativeMap = Control as MKMapView;
				nativeMap.OverlayRenderer = null;
			}

			if (e.NewElement != null) {

				var formsMap = (CustomMap)e.NewElement;
				var nativeMap = Control as MKMapView;

				nativeMap.OverlayRenderer = GetOverlayRenderer;

				CLLocationCoordinate2D [] coords = new CLLocationCoordinate2D [formsMap.RouteCoordinates.Count];

				int index = 0;
				foreach (var position in formsMap.RouteCoordinates) {
					coords [index] = new CLLocationCoordinate2D (position.Latitude, position.Longitude);
					index++;
				}

				var routeOverlay = MKPolyline.FromCoordinates (coords);
				nativeMap.AddOverlay (routeOverlay);
			}
		}

		MKOverlayRenderer GetOverlayRenderer (MKMapView mapView, IMKOverlay overlayWrapper)
		{
			IMKOverlay overlay = Runtime.GetNSObject (overlayWrapper.Handle) as IMKOverlay;
			if (polylineRenderer == null) {
				polylineRenderer = new MKPolylineRenderer (overlay as MKPolyline);
				polylineRenderer.FillColor = UIColor.Blue;
				polylineRenderer.StrokeColor = UIColor.Red;
				polylineRenderer.LineWidth = 3;
				polylineRenderer.Alpha = 0.4f;
			}
			return polylineRenderer;
		}
	}
}

