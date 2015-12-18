//Copyright 2015 Esri.
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.DisplayDrawingStatus
{
    [Register("DisplayDrawingStatusViewController")]
    public class DisplayDrawingStatusViewController : UIViewController
    {
        UIActivityIndicatorView _activityIndicator;

        public DisplayDrawingStatusViewController()
        {
            Title = "Display drawing status";
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create a variable to hold the Y coordinate of the map view control.
            var yOffset = 60;

            // Create a new MapView control and provide its location coordinates on the frame.
            MapView myMapView = new MapView();
            myMapView.Frame = new CoreGraphics.CGRect(0, yOffset, View.Bounds.Width, View.Bounds.Height - 30);

            // Create a toolbar on the bottom of the display 
            UIToolbar toolbar = new UIToolbar();
            toolbar.Frame = new CoreGraphics.CGRect(0, myMapView.Bounds.Height, View.Bounds.Width, 30);

            // Create an activity indicator
            _activityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            _activityIndicator.Frame = new CoreGraphics.CGRect(0, 0, toolbar.Bounds.Width, toolbar.Bounds.Height);

            // Create a UIBarButtonItem to show the activity indicator
            UIBarButtonItem indicatorButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            indicatorButton.CustomView = _activityIndicator;

            // Add the indicatorButton to an array of UIBarButtonItems
            var barButtonItems = new UIBarButtonItem[] { indicatorButton };

            // Add the UIBarButtonItems to the toolbar
            toolbar.SetItems(barButtonItems, true);

            // Create a new map instance
            Map myMap = new Map(BasemapType.Topographic, 34.056, -117.196, 4);

            //Map myMap = new Map(Basemap.CreateTopographic());
            var featureTable = new ServiceFeatureTable(new Uri("http://sampleserver6.arcgisonline.com/arcgis/rest/services/DamageAssessment/FeatureServer/0"));
            var featureLayer = new FeatureLayer(featureTable);

            // TODO: Remove #2915
            await featureLayer.LoadAsync();

            // Add the feature layer to the Map
            myMap.OperationalLayers.Add(featureLayer);

            // Assign the map to the MapView that was created above.
            myMapView.Map = myMap;

            // Hook up the DrawStatusChanged event
            myMapView.DrawStatusChanged += MyMapView_DrawStatusChanged;

            // Add the MapView to the Subview
            View.AddSubviews(myMapView, toolbar);

            // Animate the activity spinner
            _activityIndicator.StartAnimating();
        }

        private void MyMapView_DrawStatusChanged(object sender, DrawStatus e)
        {
            if (e == DrawStatus.InProgress)
                _activityIndicator.Hidden = false;
            else
                _activityIndicator.Hidden = true;
        }
    }
}