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
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.ArcGISTiledLayerUrl
{
   
    [Register("ArcGISTiledLayerUrlViewController")]
    public class ArcGISTiledLayerUrlViewController : UIViewController
    {
        public ArcGISTiledLayerUrlViewController()
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //First we create a new tiled layer and pass a Url to the service
            var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/NatGeo_World_Map/MapServer"));

            //We need to await the load call for the layer. This is required for layer to initialize all the metadata. If the layer is added without this load call, 
            //then it will not get initialized and no data will be visible on map.    
            await baseLayer.LoadAsync();

            //Create a basemap where we can add this baselayer
            var basemap = new Basemap();

            //Add the ArcGISTiledLayer that we created above to the basemap. 
            basemap.BaseLayers.Add(baseLayer);

            //Now lets create our UI.

            //Create a variable to hold the Y coordinate of the map view control. We dont need XOffset since we are going to place the mapview at x=0
            var yOffset = 70;

            //Create a new mapview control and provide its location coordinates on the frame.
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, yOffset, View.Bounds.Width, View.Bounds.Height - yOffset)
            };

            //Create a new map instance which holds basemap that was created
            Map myMap = new Map(basemap);

            //Assign this map to the mapview that was created above.
            myMapView.Map = myMap;

            //Finally add the mapview to the Subview
            View.AddSubview(myMapView);
        }
    }
}