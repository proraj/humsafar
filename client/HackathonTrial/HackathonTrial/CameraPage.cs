using System;
using Plugin.Media;
using Xamarin.Forms;

namespace HackathonTrial
{
	public class CameraPage : ContentPage
	{
		Image image;

		public CameraPage ()
		{
			Button TakeImageButton = new Button {
				Text = "Capture Image"
			};

			Button ChooseImageButton = new Button {
				Text = "Choose Image"
			};

			image = new Image {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			TakeImageButton.Clicked += TakeImageButton_Clicked;
			ChooseImageButton.Clicked += ChooseImageButton_Clicked;

			StackLayout stack = new StackLayout () {
				Children = {
					TakeImageButton,
					ChooseImageButton,
					image
				}
			};

			Content = stack;
		}

		async void TakeImageButton_Clicked (object sender, EventArgs e)
		{
			if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported) {
				// Supply media options for saving our photo after it's taken.
				var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions {
					Directory = "Receipts",
					Name = $"{DateTime.UtcNow}.jpg"
				};

				// Take a photo of the business receipt.
				var file = await CrossMedia.Current.TakePhotoAsync (mediaOptions);

				if (file == null){
					return;
				}

				image.Source = ImageSource.FromStream (() => {
					var stream = file.GetStream ();
					file.Dispose ();
					return stream;
				});
			}
		}

		async void ChooseImageButton_Clicked (object sender, EventArgs e)
		{
			if (CrossMedia.Current.IsPickPhotoSupported) {
				var photo = await CrossMedia.Current.PickPhotoAsync ();

				if (photo == null)
					return;

				image.Source = ImageSource.FromStream (() => {
					var stream = photo.GetStream ();
					photo.Dispose ();
					return stream;
				});
			}
		}
	}
}


