using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.MapFromPortal
{
    [Register("MapFromPortalViewController")]
    public class MapFromPortalViewController : UIViewController
    {
        private string[] itemURLs = new string[] { "http://www.arcgis.com/home/item.html?id=2d6fa24b357d427f9c737774e7b0f977"
        , "http://www.arcgis.com/home/item.html?id=01f052c8995e4b9e889d73c3e210ebe3"
        , "http://www.arcgis.com/home/item.html?id=74a8f6645ab44c4f82d537f1aa0e375d"};

        private string[] titles = new string[] { "Housing with Mortgages", "USA Tapestry Segmentation", "Geology of United States" };

        private Map myMap;
        public int selectedItemIndex =0;
        public MapFromPortalViewController()
        {
          
        }
        

        public override void ViewDidLoad()
        {
           
            base.ViewDidLoad();

            //Create a variable to hold the Y coordinate of the map view control. We dont need XOffset since we are going to place the mapview at x=0
            var height = 45;

            // Perform any additional setup after loading the view
            //Create a new mapview control and provide its location coordinates on the frame.
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height- height)
            };

            //Create a new map instance with url of the web map that is displayed by default
            myMap = new Map(new Uri(itemURLs[0]));

            //Assign this map to the mapview that was created above.
            myMapView.Map = myMap;

            //add button
            UIButton mapsButton = new UIButton(UIButtonType.Custom)
            {
                Frame=new CoreGraphics.CGRect(0,myMapView.Bounds.Height,View.Bounds.Width,height),
                BackgroundColor=UIColor.White               
            };

            mapsButton.SetTitle("Maps", UIControlState.Normal);
            mapsButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);

            mapsButton.TouchUpInside += (s, e) => {
                UIAlertController actionSheetAlert = UIAlertController.Create("Select a map to open", "", UIAlertControllerStyle.ActionSheet);
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
                actionSheetAlert.AddAction(UIAlertAction.Create(titles[0], UIAlertActionStyle.Default, (action) =>
                {
                    myMap = new Map(new Uri(itemURLs[0]));
                    myMapView.Map = myMap;
                }));
                this.PresentViewController(actionSheetAlert, true, null);
            };

            //Finally add the mapview to the Subview
            View.AddSubviews(myMapView, mapsButton);

        }

    }
}