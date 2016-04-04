// Copyright 2015 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;

namespace ArcGISRuntimeXamarin.Samples.ArcGISTiledLayerUrl
{
    [Register("ArcGISTiledLayerUrl")]
    public class ArcGISTiledLayerUrl : UIViewController
    {
        public ArcGISTiledLayerUrl()
        {
        }
        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create a new tiled layer and pass a Uri to the service
            var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/NatGeo_World_Map/MapServer"));

            // Await the load call for the layer
            await baseLayer.LoadAsync();

            // Create a basemap for the base layer
            var myBasemap = new Basemap();

            // Add the base layer to the basemap 
            myBasemap.BaseLayers.Add(baseLayer);

            // Create a variable to hold the Y coordinate of the MapView control (adds spacing at the top)
            var yOffset = 70;

            // Create a new MapView control and provide its location coordinates on the frame
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, yOffset, View.Bounds.Width, View.Bounds.Height - yOffset)
            };

            // Create a new Map instance
            Map myMap = new Map(SpatialReferences.WebMercator) { Basemap = myBasemap };

            // Assign the Map to the MapView
            myMapView.Map = myMap;

            // Add the MapView to the Subview
            View.AddSubview(myMapView);
        }
    }
}