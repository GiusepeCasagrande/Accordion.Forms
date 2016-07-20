using System;
using Xamarin.Forms;

namespace Accordion.Forms
{
	public class ShadowBoxCell : ContentView
	{
		public string ImageName { get; set;}
		public double BoxWidth  { get; set;}
		public double BoxHeight  { get; set;}

		public ShadowBoxCell (View content, double boxHeight, double boxWidth)
		{
			BoxWidth = boxWidth;
			BoxHeight = boxHeight;

			var frameImage = new Frame {
				Padding = 5,
				Content = content,
				BackgroundColor = Color.White,
				HasShadow = false,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				OutlineColor = Color.Black,
				HeightRequest = BoxHeight,
				WidthRequest = BoxWidth,
				MinimumHeightRequest = BoxHeight,
				MinimumWidthRequest = BoxWidth,
			};

			var frameBackground1 = new Frame {
				Padding = 5,
				BackgroundColor = Color.Gray,
				HasShadow = false,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				OutlineColor = Color.Black,
				HeightRequest = BoxHeight,
				WidthRequest = BoxWidth,
				MinimumHeightRequest = BoxHeight,
				MinimumWidthRequest = BoxWidth,
			};

			var grid = new Grid {
				RowSpacing = 0,
				Padding = new Thickness (0, 0, 0, 0),
			};

			grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (300) });
			grid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (300) });


			var relativeLayout = new RelativeLayout();

			relativeLayout.Children.Add(frameBackground1,
				Constraint.Constant(2),
				Constraint.Constant(2)
			);

			relativeLayout.Children.Add(frameImage,
				Constraint.RelativeToView(frameBackground1, (parent, sibling) =>
					{
						return sibling.X -1;
					}),
				Constraint.RelativeToView(frameBackground1, (parent, sibling) =>
					{
						return sibling.Y - 1;
					}));

			grid.Children.Add (relativeLayout, 0, 0);

			Content = grid;
		}
	}
}

