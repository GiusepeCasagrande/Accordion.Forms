using System;
using Xamarin.Forms;

namespace Accordion.Forms
{
	public class SampleInnerView : StackLayout
	{
		Slider m_slider;
		Label m_labelDeviceName;

		/// <summary>
		/// Initializes a new instance of the <see cref="Photon.Views.AreaDetail.Details.LampDetailView"/> class.
		/// </summary>
		public SampleInnerView()
		{
			SetupLabel();
			SetupSlider();

			HeightRequest = 100;
			Orientation = StackOrientation.Vertical;
			HorizontalOptions = LayoutOptions.Fill;
			BackgroundColor = Color.Gray;
			Children.Add(m_labelDeviceName);
			Children.Add(m_slider);
		}

		/// <summary>
		/// Setups the label.
		/// </summary>
		void SetupLabel()
		{
			m_labelDeviceName = new Label
			{
				Text = "Label 1",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				VerticalOptions = LayoutOptions.Center
			};
		}

		/// <summary>
		/// Setups the slider.
		/// </summary>
		void SetupSlider()
		{
			m_slider = new Slider()
			{
				Minimum = 0,
				Maximum = 100
			};
		}
	}
}

