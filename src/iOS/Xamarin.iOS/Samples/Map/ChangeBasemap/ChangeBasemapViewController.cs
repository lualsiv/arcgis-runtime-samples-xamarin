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
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;

namespace ArcGISRuntimeXamarin.Samples.ChangeBasemap
{
    [Register("ChangeBasemapViewController")]
    public class ChangeBasemapViewController : UIViewController
    {
        public ChangeBasemapViewController()
        {
            Title = "Change basemap";
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create a variable to hold the height of the segmented control.
            var yOffset = 60;

            var height = 40;
            // Create a new MapView control and provide its location coordinates on the frame.
            MapView myMapView = new MapView();
            myMapView.Frame = new CoreGraphics.CGRect(0, yOffset, View.Bounds.Width, View.Bounds.Height - height);

            // Create a new Map instance with the basemap               
            Map myMap = new Map(SpatialReferences.WebMercator);
            myMap.Basemap = Basemap.CreateTopographic();

            // Assign the Map to the MapView
            myMapView.Map = myMap;

            // Create a toolbar on the bottom of the display 
            UIToolbar toolbar = new UIToolbar();
            toolbar.Frame = new CoreGraphics.CGRect(0, myMapView.Bounds.Height, View.Bounds.Width, height);

            // Create a segmented control to display buttons
            UISegmentedControl segmentControl = new UISegmentedControl();
            segmentControl.Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height);
            segmentControl.InsertSegment("Topographic", 0, false);
            segmentControl.InsertSegment("Streets", 1, false);
            segmentControl.InsertSegment("Imagery", 2, false);
            segmentControl.InsertSegment("Ocean", 3, false);

            segmentControl.ValueChanged += (sender, e) =>
            {
                var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;

                switch (selectedSegmentId)
                {
                    case 0:
                        // Set the basemap to Topographic
                        myMapView.Map.Basemap = Basemap.CreateTopographic();
                        break;
                    case 1:
                        // Set the basemap to Streets
                        myMapView.Map.Basemap = Basemap.CreateStreets();
                        break;
                    case 2:
                        // Set the basemap to Imagery
                        myMapView.Map.Basemap = Basemap.CreateImagery();
                        break;
                    case 3:
                        // Set the basemap to Oceans
                        myMapView.Map.Basemap = Basemap.CreateOceans();
                        break;
                }
            };

            // Create a UIBarButtonItem to show the SegmentControl
            UIBarButtonItem barButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            barButtonItem.CustomView = segmentControl;

            // Add the bar button item to an array of UIBarButtonItems
            var barButtonItems = new UIBarButtonItem[] { barButtonItem };
            
            // Add the UIBarButtonItems array to the toolbar
            toolbar.SetItems(barButtonItems, true);

            View.AddSubviews(myMapView, toolbar);
        }
    }
}