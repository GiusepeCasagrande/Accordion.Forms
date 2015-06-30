using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Accordion.Forms
{
	/// <summary>
	/// Area devices list page.
	/// </summary>
	public class AccordionControl : ScrollView
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

		readonly List<AccordionEntry> m_entries = new List<AccordionEntry>();
		ScrollView m_scrollView;
		StackLayout m_layout;

		uint AnimationDuration { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Photon.Controls.AccordionControl"/> class.
		/// </summary>
		public AccordionControl(View header = null)
		{
			AnimationDuration = 300;
			BackgroundColor = DefaultButtonBackgroundColor;
			DefaultButtonTextColor = Color.Black;
			Padding = new Thickness(0, 0, 0, 0);

			m_layout = new StackLayout()
			{
				BackgroundColor = Color.White,
				Orientation = StackOrientation.Vertical,
				Spacing = 0
			};

			if (header != null)
				m_layout.Children.Add(header);

			m_scrollView = new ScrollView()
			{
				BackgroundColor = DefaultButtonBackgroundColor,
				Content = m_layout,
				Orientation = ScrollOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			Content = m_scrollView;
		}


		/// <summary>
		/// Add the specified cell and view.
		/// </summary>
		/// <param name="cell">cell.</param>
		/// <param name="view">View.</param>
		public void Add(View cell, View view)
		{
			if (cell == null)
				throw new ArgumentNullException("cell");

			if (view == null)
				throw new ArgumentNullException("view");

			m_entries.Add(new AccordionEntry()
				{
					Cell = cell,
					View = view,
					OriginalSize = new Size(view.WidthRequest, view.HeightRequest)
				});

			var line = new StackLayout()
			{
				Orientation = StackOrientation.Vertical
			};

			line.Children.Add(cell);
			line.Children.Add(view);

			m_layout.Children.Add(line);

			var cellIndex = m_entries.Count - 1;

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped	+= (object sender, EventArgs e) => OncellTouchUpInside(cellIndex);
			cell.GestureRecognizers.Add(tapGestureRecognizer);
		}

		/// <summary>
		/// Closes all entries.
		/// </summary>
		public void CloseAllEntries()
		{
			foreach (var entry in m_entries)
			{
				CloseAccordion(entry);
				entry.IsOpen = false;
			}
		}

		async void CloseAccordion(AccordionEntry entry)
		{
			var view = entry.View;
			var b = view.Bounds;
			b.Height = 0;

			await view.LayoutTo(b, AnimationDuration, Easing.SinInOut);

			view.IsVisible = false;
			view.IsEnabled = false;
		}

		async void OpenAccordion(AccordionEntry entry)
		{
			var view = entry.View;
			view.IsVisible = true;
			view.IsEnabled = true;

			var b = view.Bounds;
			b.Height = 0;
			view.Layout(b);

			b.Height = entry.OriginalSize.Height;

			await view.LayoutTo(b, AnimationDuration, Easing.SinInOut);
		}

		/// <summary>
		/// Raises the cell touch up inside event.
		/// </summary>
		/// <param name="cellIndex">cell index.</param>
		void OncellTouchUpInside(int cellIndex)
		{
			var touchedEntry = m_entries[cellIndex];
			bool isTouchingToClose = touchedEntry.IsOpen;

			var entriesToClose = m_entries.Where(e => e.IsOpen);
			foreach (var entry in entriesToClose)
			{
				CloseAccordion(entry);
				entry.IsOpen = false;
			}


			if (!isTouchingToClose)
			{
				OpenAccordion(touchedEntry);
				touchedEntry.IsOpen = true;
			}
		}
	}
}
