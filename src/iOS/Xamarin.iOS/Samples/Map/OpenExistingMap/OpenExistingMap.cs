using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.OpenExistingMap
{
    [Register("OpenExistingMap")]
    public class OpenExistingMap : UIViewController
    {
        // String array to hold urls to publicly available web maps
        private string[] itemURLs = new string[] { "http://www.arcgis.com/home/item.html?id=2d6fa24b357d427f9c737774e7b0f977"
        , "http://www.arcgis.com/home/item.html?id=01f052c8995e4b9e889d73c3e210ebe3"
        , "http://www.arcgis.com/home/item.html?id=74a8f6645ab44c4f82d537f1aa0e375d"};

        // String array to store titles for the webmaps specified above. These titles are in the same order as the urls above
        private string[] titles = new string[] { "Housing with Mortgages", "USA Tapestry Segmentation", "Geology of United States" };

        // Variable to hold Map instance
        private Map myMap;
        
        public OpenExistingMap()
        {
          
        }     

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            try
            {
                // Create a variable to hold the height of the button
                var height = 45;

                // Create a new MapView control and provide its location coordinates on the frame
                MapView myMapView = new MapView()
                {
                    Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height - height)
                };

                // Create a new Map instance with url of the webmap that is displayed by default
                myMap = new Map(new Uri(itemURLs[0]));

                // Assign the map to the MapView
                myMapView.Map = myMap;

                // Add a button at the bottom to show webmap choices
                UIButton mapsButton = new UIButton(UIButtonType.Custom)
                {
                    Frame = new CoreGraphics.CGRect(0, myMapView.Bounds.Height, View.Bounds.Width, height),
                    BackgroundColor = UIColor.White
                };

                mapsButton.SetTitle("Maps", UIControlState.Normal);
                mapsButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);

                // Open an action sheet with the available webmaps. Users can select a webmap to open it.
                mapsButton.TouchUpInside += (s, e) => {
                    // Initialize an UIAlertController with a title and style of an ActionSheet
                    UIAlertController actionSheetAlert = UIAlertController.Create("Select a map to open", "", UIAlertControllerStyle.ActionSheet);
                    // Add actions to ActionSheet. Selecting an option re-initializes the Map with selected webmap url and assigns it to MapView.
                    actionSheetAlert.AddAction(UIAlertAction.Create(titles[0], UIAlertActionStyle.Default, (action) =>
                    {
                        myMap = new Map(new Uri(itemURLs[0]));
                        myMapView.Map = myMap;

                    }));
                    actionSheetAlert.AddAction(UIAlertAction.Create(titles[1], UIAlertActionStyle.Default, (action) =>
                    {
                        myMap = new Map(new Uri(itemURLs[1]));
                        myMapView.Map = myMap;

                    }));
                    actionSheetAlert.AddAction(UIAlertAction.Create(titles[2], UIAlertActionStyle.Default, (action) =>
                    {
                        myMap = new Map(new Uri(itemURLs[2]));
                        myMapView.Map = myMap;

                    }));
                    this.PresentViewController(actionSheetAlert, true, null);
                };

                // Add the MapView to the Subview
                View.AddSubviews(myMapView, mapsButton);
            }
            catch (Exception ex)
            {
                UIAlertController errorAlert = UIAlertController.Create("error", ex.Message, UIAlertControllerStyle.Alert);
            }         
        }
    }
}