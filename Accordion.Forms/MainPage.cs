using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace Accordion.Forms
{
	public class MainPage : ContentPage
	{
		AccordionControl m_accordionView;
		BoxView m_header;
		IList<SampleInnerView> m_innerViews;

		public MainPage ()
		{
			m_header = new BoxView () {
				BackgroundColor = Color.Red,
				HeightRequest = 250,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};


			m_innerViews = new List<SampleInnerView> ();
			for (int i = 0; i < 12; i++) {
				m_innerViews.Add (new SampleInnerView ());
			}
		}

		protected override void OnAppearing()
		{
			IsBusy = true;
			base.OnAppearing();
			NavigationPage.SetHasNavigationBar(this, false);

			m_accordionView = new AccordionControl(m_header);
			m_accordionView.BackgroundColor = Color.FromRgb(52, 52, 52);
			m_accordionView.DefaultButtonBackgroundColor = Color.FromRgb(52, 52, 52);
			m_accordionView.DefaultButtonTextColor = Color.White;


			var isGreen = true;
			foreach (SampleInnerView item in m_innerViews)
			{
				var cell = new BoxView()
				{
					BackgroundColor = isGreen? Color.Green : Color.Blue,
					HeightRequest = 100,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.FillAndExpand
				};
				cell.BindingContext = item;

				m_accordionView.Add(cell, item);
				isGreen = !isGreen;
			}

			m_accordionView.CloseAllEntries();

			Content = m_accordionView;
			IsBusy = false;
		}
	}
}


