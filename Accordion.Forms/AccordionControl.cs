using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Accordion.Forms
{
	/// <summary>
	/// Area devices list page.
	/// </summary>
	public class AccordionControl : Grid
	{
		/// <summary>
		/// Gets or sets the default color of the button background.
		/// </summary>
		/// <value>The default color of the button background.</value>
		public Color DefaultButtonBackgroundColor { get; set; }

		/// <summary>
		/// Gets or sets the default color of the button text.
		/// </summary>
		/// <value>The default color of the button text.</value>
		public Color DefaultButtonTextColor { get; set; }

		/// <summary>
		/// Gets or sets the duration of the animation.
		/// </summary>
		/// <value>The duration of the animation.</value>
		public uint AnimationDuration { get; set; }

		readonly List<AccordionEntry> m_entries = new List<AccordionEntry> ();
		ScrollView m_scrollView;
		StackLayout m_cellStackLayout;
		ScrolledEventArgs m_scrolledEventArgs;
		double m_lastScrollPosition;

		/// <summary>
		/// Initializes a new instance of the <see cref="Photon.Controls.AccordionControl"/> class.
		/// </summary>
		public AccordionControl (View header = null)
		{
			AnimationDuration = 400;
			BackgroundColor = DefaultButtonBackgroundColor;
			DefaultButtonTextColor = Color.Black;
			Padding = new Thickness (0, 0, 0, 0);

			RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
			RowDefinitions.Add (new RowDefinition { Height = GridLength.Auto });
			ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });


			RowSpacing = 0;
			ColumnSpacing = 0;

			m_cellStackLayout = new StackLayout () {
				BackgroundColor = DefaultButtonBackgroundColor,
				Orientation = StackOrientation.Vertical,
				Spacing = 0
			};
					
			var shadowImage = new Image () {
				Source = "HeaderShadow.png",
				InputTransparent = true,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Aspect = Aspect.Fill
			};


			m_scrollView = new ScrollView () {
				BackgroundColor = DefaultButtonBackgroundColor,
				Content = m_cellStackLayout,
				Orientation = ScrollOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			m_scrollView.Scrolled += AccordionViewScrolled;

			Children.Add (m_scrollView, 0, 1);

			if (header != null) {
				Children.Add (header, 0, 0);
				Children.Add (shadowImage, 0, 1);
			}

		}

		/// <summary>
		/// Add the specified cell and view.
		/// </summary>
		/// <param name="cell">cell.</param>
		/// <param name="view">View.</param>
		public void Add (View cell, View view)
		{
			if (cell == null)
				throw new ArgumentNullException ("cell");

			if (view == null)
				throw new ArgumentNullException ("view");

			var entry = new AccordionEntry () {
				Cell = cell,
				View = new ScrollView () {
					Content = view,

				},
				OriginalSize = new Size (view.WidthRequest, view.HeightRequest)
			};

			m_entries.Add (entry);

			m_cellStackLayout.Children.Add (entry.Cell);
			m_cellStackLayout.Children.Add (entry.View);


			var cellIndex = m_entries.Count - 1;

			var tapGestureRecognizer = new TapGestureRecognizer ();
			tapGestureRecognizer.Tapped	+= (object sender, EventArgs e) => OnCellTouchUpInside (cellIndex);
			cell.GestureRecognizers.Add (tapGestureRecognizer);
		}

		/// <summary>
		/// Closes all entries.
		/// </summary>
		public void CloseAllEntries ()
		{
			foreach (var entry in m_entries) {
				entry.View.HeightRequest = 0;
				entry.IsOpen = false;
				entry.View.IsVisible = false;
			}
		}

		async void OpenAccordion (AccordionEntry entry)
		{
			// Get the element (cell) touched
			var element = m_cellStackLayout.Children.FirstOrDefault (x => x == entry.Cell);
			if (element == null)
				return;

			entry.View.Animate ("expand",
				x => {
					entry.View.IsVisible = true;
					entry.View.HeightRequest = entry.OriginalSize.Height * x;

					// calculate te position to be scrolled based on the X from Animate and element Y position
					var position = element.Y * x;
					m_scrollView.ScrollToAsync (0, position, false);

				}, 0, AnimationDuration, Easing.SpringOut, (d, b) => {
				entry.View.IsVisible = true;
			});
		}

		async void CloseAccordion (AccordionEntry entry)
		{
			entry.View.Animate ("colapse",
				x => {
					var change = entry.OriginalSize.Height * x;
					entry.View.HeightRequest = entry.OriginalSize.Height - change;
				}, 0, AnimationDuration, Easing.SpringIn, (d, b) => {
				entry.View.IsVisible = false;
			});
		}
			
		/// <summary>
		/// Raises the cell touch up inside event.
		/// </summary>
		/// <param name="cellIndex">cell index.</param>
		void OnCellTouchUpInside (int cellIndex)
		{
			var touchedEntry = m_entries [cellIndex];
			bool isTouchingToClose = touchedEntry.IsOpen;

			var entriesToClose = m_entries.Where (e => e.IsOpen);
			foreach (var entry in entriesToClose) {
				CloseAccordion (entry);
				entry.IsOpen = false;
			}
				
			if (!isTouchingToClose) {
				OpenAccordion (touchedEntry);
				touchedEntry.IsOpen = true;
			}
		}

		void AccordionViewScrolled (object sender, ScrolledEventArgs e)
		{
			m_scrolledEventArgs = e;
		}
	}
}
