using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace Accordion.Forms
{
    public class MainPage : ContentPage
    {
        AccordionControl m_accordionView;
        Grid m_header;
        IList<SampleInnerView> m_innerViews;

        public MainPage()
        {
            CreateHeader();

            m_innerViews = new List<SampleInnerView>();
            for (int i = 0; i < 12; i++)
            {
                m_innerViews.Add(new SampleInnerView());
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

            foreach (SampleInnerView item in m_innerViews)
            {
                Grid cell = CreateCell();
                m_accordionView.Add(cell, item);
            }

            m_accordionView.CloseAllEntries();

            Content = m_accordionView;
            IsBusy = false;
        }

        void CreateHeader()
        {
            var imageSource = ImageSource.FromFile("HeaderImage.jpg");
            var image = new Image()
            {
                Aspect = Aspect.AspectFill,
                BackgroundColor = Color.Red,
                HeightRequest = 300,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Source = imageSource
            };

            var label = new Label()
            {
                Text = "A Beautiful Butterfly",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 60
            };

            m_header = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Padding = new Thickness(0, 0, 0, 0),
                RowSpacing = 0,
                ColumnSpacing = 0
            };

            m_header.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            m_header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            m_header.Children.Add(image);
            m_header.Children.Add(label);

        }

        static Grid CreateCell()
        {
            var blackBox = new BoxView()
            {
                BackgroundColor = Color.Black,
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var greenBox = new BoxView()
            {
                BackgroundColor = Color.FromHex("607624"),
                HeightRequest = 98,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            var label = new Label()
            {
                Text = "You can touch here.",
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            var cell = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Padding = new Thickness(0, 0, 0, 0),
                RowSpacing = 0,
                ColumnSpacing = 0,
            };

            cell.Children.Add(blackBox);
            cell.Children.Add(greenBox);
            cell.Children.Add(label);
            return cell;
        }
    }
}