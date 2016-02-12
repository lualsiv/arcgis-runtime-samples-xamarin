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
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime;
using System.IO;
using Esri.ArcGISRuntime.Mapping;

namespace ArcGISRuntimeXamarin.Samples.ArcGISTiledLayerLocal
{
    [Register("ArcGISTiledLayerLocalViewController")]
    public class ArcGISTiledLayerLocalViewController : UIViewController
    {
        MapView MyMapView;
        public ArcGISTiledLayerLocalViewController()
        {
            Title = "ArcGIS tiled layer (local tpk)";
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create a variable to hold the Y coordinate of the MapView control (adds spacing at the top)
            var yOffset = 70;

            // Create a new MapView control and provide its location coordinates on the frame
            MyMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, yOffset, View.Bounds.Width, View.Bounds.Height - yOffset)
            };

            // Create a new Map instance
            Map myMap = new Map(SpatialReferences.WebMercator);

            // Assign the Map to the MapView
            MyMapView.Map = myMap;

            // Add the MapView to the Subview
            View.AddSubview(MyMapView);

            LoadTPKLayer();
        }

        private async void LoadTPKLayer()
        {
            try
            {
                var pathToLayer = "campus.tpk";

                // Create a Uri to pass to the ArcGISTiledLayer constructor
                Uri myUri = new Uri(pathToLayer, UriKind.Relative);

                // Check that the file exists
                if (!File.Exists(pathToLayer))
                {
                    UIAlertView alert = new UIAlertView("Sample Error", "'" + pathToLayer + "'" + " not found", null, "OK", null);
                    alert.Show();
                    return;
                }
                else
                {
                    var layer = new ArcGISTiledLayer(myUri) { Name = "campus_map" };
                    await layer.LoadAsync();

                    MyMapView.Map.OperationalLayers.Add(layer);
                    await MyMapView.Map.RetryLoadAsync();
                }
            }
            catch (Exception ex)
            {
                UIAlertView alert = new UIAlertView("Sample Error", ex.Message + " not found", null, "OK", null);
                alert.Show();

                Console.WriteLine(ex.Message);
            }
        }
    }
}