using System;
using Xamarin.Forms;

namespace Accordion.Forms
{
	public class SampleInnerView : StackLayout
	{
		Slider m_slider;
		Label m_labelDeviceName;

		/// <summary>
        /// Initializes a new instance of the <see cref="T:Accordion.Forms.SampleInnerView"/> class.
        /// </summary>
		public SampleInnerView()
		{
			SetupLabel();
			SetupSlider();

			HeightRequest = 150;
			Orientation = StackOrientation.Vertical;
			HorizontalOptions = LayoutOptions.Fill;
            BackgroundColor = Color.FromHex("443427");
            Children.Add(m_slider);
			Children.Add(m_labelDeviceName);	
		}

		/// <summary>
		/// Setups the label.
		/// </summary>
		void SetupLabel()
		{
			m_labelDeviceName = new Label
			{
				Text = "A label insinde the accordion.",
                TextColor = Color.White,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
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
				Maximum = 100,
                HeightRequest = 50
			};
		}
	}
}

