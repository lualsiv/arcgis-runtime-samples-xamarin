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

            //Create a new tiled layer and pass a Uri to the service
            var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/NatGeo_World_Map/MapServer"));

            //We need to await the load call for the layer.    
          //  await baseLayer.LoadAsync();

            //Create a basemap where we can add this baselayer
            var myBasemap = new Basemap();

            //Add the ArcGISTiledLayer that we created above to the basemap. 
            myBasemap.BaseLayers.Add(baseLayer);

            //Now lets create our UI.

            //Create a variable to hold the Y coordinate of the map view control.
            var yOffset = 70;

            //Create a new mapview control and provide its location coordinates on the frame.
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, yOffset, View.Bounds.Width, View.Bounds.Height - yOffset)
            };

            //Create a new map instance which holds basemap that was created
            Map myMap = new Map(myBasemap);

            //Assign this map to the mapview that was created above.
            myMapView.Map = myMap;

            //Finally add the mapview to the Subview
            View.AddSubview(myMapView);
        }
    }
}