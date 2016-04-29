using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Geometry;
using CoreLocation;
using Esri.ArcGISRuntime.Location;

namespace ArcGISRuntimeXamarin.Samples.DisplayDeviceLocation
{    
    [Register("DisplayDeviceLocation")]
    public class DisplayDeviceLocation : UIViewController
    {
        // Create and hold reference to the used MapView
        private MapView _myMapView = new MapView();

        public DisplayDeviceLocation()
        {
            this.Title = "Display Device Location";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Create the UI, setup the control references and execute initialization 
            CreateLayout();
            Initialize();
        }

        private void Initialize()
        {
            // Create new Map with basemap
            Map myMap = new Map(Basemap.CreateImagery());

            // Create a mappoint the map should zoom to
            MapPoint mapPoint = new MapPoint(-13630484, 4545415, SpatialReferences.WebMercator);

            // Set the initial viewpoint for map
            myMap.InitialViewpoint = new Viewpoint(mapPoint, 90000);

            // Provide used Map to the MapView
            _myMapView.Map = myMap;
        }

        private void OnStopButtonClicked(object sender, EventArgs e)
        {
            _myMapView.LocationDisplay.Stop();
        }

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            try
            {
                    UIAlertController actionSheetAlert = UIAlertController.Create(
             "Select device location option", "", UIAlertControllerStyle.ActionSheet);

                    // Add actions to ActionSheet. Selecting an option displays different option for auto pan modes.
                    actionSheetAlert.AddAction(UIAlertAction.Create("On", UIAlertActionStyle.Default, (action) =>
                    {
                        // Starts location display with auto pan mode set to Off
                        _myMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Off;
                        _myMapView.LocationDisplay.Start();

                    }));
                    actionSheetAlert.AddAction(UIAlertAction.Create("Re-center", UIAlertActionStyle.Default, (action) =>
                    {
                        // Starts location display with auto pan mode set to Default
                        _myMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Default;
                        _myMapView.LocationDisplay.Start();
                    }));
                    actionSheetAlert.AddAction(UIAlertAction.Create("Navigation", UIAlertActionStyle.Default, (action) =>
                    {
                        // Starts location display with auto pan mode set to Navigation
                        _myMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Navigation;
                        _myMapView.LocationDisplay.Start();
                    }));
                    actionSheetAlert.AddAction(UIAlertAction.Create("Compass", UIAlertActionStyle.Default, (action) =>
                    {
                        // Starts location display with auto pan mode set to Compass Navigation
                        _myMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
                         _myMapView.LocationDisplay.Start();
                    }));
                //present action sheet
                PresentViewController(actionSheetAlert, true, null);                                    
            }
            catch (Exception ex)
            {
                UIAlertController alert = UIAlertController.Create("Error", ex.Message, UIAlertControllerStyle.Alert);
                PresentViewController(alert, true, null);
            }
        }

        private void CreateLayout()
        {
            // Setup the visual frame for the MapView
            _myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height)
            };

            // Create a button to start the location
            var startButton = new UIBarButtonItem() { Title = "Start", Style = UIBarButtonItemStyle.Plain };
            startButton.Clicked += OnStartButtonClicked;

            // Create a button to apply new renderer
            var stopButton = new UIBarButtonItem() { Title = "Stop", Style = UIBarButtonItemStyle.Plain };
            stopButton.Clicked += OnStopButtonClicked; ;

            // Add the buttons to the toolbar
            SetToolbarItems(new UIBarButtonItem[] {startButton,
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null),
                stopButton}, false);

            // Show the toolbar
            NavigationController.ToolbarHidden = false;

            // Add MapView to the page
            View.AddSubviews(_myMapView);
        }
    }
}