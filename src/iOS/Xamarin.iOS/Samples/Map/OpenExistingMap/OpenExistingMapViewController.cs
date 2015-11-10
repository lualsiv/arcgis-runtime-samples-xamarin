using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.OpenExistingMap
{
    [Register("MapFromPortalViewController")]
    public class OpenExistingMapViewController : UIViewController
    {
        //String array to hold urls to publicly available web maps
        private string[] itemURLs = new string[] { "http://www.arcgis.com/home/item.html?id=2d6fa24b357d427f9c737774e7b0f977"
        , "http://www.arcgis.com/home/item.html?id=01f052c8995e4b9e889d73c3e210ebe3"
        , "http://www.arcgis.com/home/item.html?id=74a8f6645ab44c4f82d537f1aa0e375d"};

        //String array to store titles for the webmaps specified above. These titles are in the same order as the urls above
        private string[] titles = new string[] { "Housing with Mortgages", "USA Tapestry Segmentation", "Geology of United States" };

        //Variable to hold Map instance
        private Map myMap;
        
        public OpenExistingMapViewController()
        {
          
        }
        

        public override void ViewDidLoad()
        {
           
            base.ViewDidLoad();

            //Create a variable to hold the height of the button that we will be adding later on in the UI.
            var height = 45;

            //Create a new MapView control and provide its location coordinates on the frame.
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height- height)
            };

            //Create a new Map instance with url of the web map that is displayed by default
            myMap = new Map(new Uri(itemURLs[0]));

            //Assign this map to the MapView that was created above.
            myMapView.Map = myMap;

            //Now we add a button at the bottom that will present users different webmaps that they can select to open.
            UIButton mapsButton = new UIButton(UIButtonType.Custom)
            {
                Frame=new CoreGraphics.CGRect(0,myMapView.Bounds.Height,View.Bounds.Width,height),
                BackgroundColor=UIColor.White               
            };

            mapsButton.SetTitle("Maps", UIControlState.Normal);
            mapsButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);

            //Add event for button click. Clicking this button opens an action sheet with all the web maps in itemUrls above being presented as options. Users can select a webmap to open it.
            mapsButton.TouchUpInside += (s, e) => {
                //Initialize an UIAlertController with a title and style of an ActionSheet
                UIAlertController actionSheetAlert = UIAlertController.Create("Select a map to open", "", UIAlertControllerStyle.ActionSheet);
                //Add actions to ActionSheet. Selecting an option re-initializes the Map with selected web map url and assign it to MapView
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

            //Finally add the MapView to the Subview
            View.AddSubviews(myMapView, mapsButton);
        }

    }
}