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

using Esri.ArcGISRuntime.Mapping;
using System;
using Xamarin.Forms;

namespace ArcGISRuntimeXamarin.Samples.ArcGISVectorTiledLayerUrl
{
    public partial class ArcGISVectorTiledLayerUrl : ContentPage
	{
        public ArcGISVectorTiledLayerUrl()
		{
            InitializeComponent ();

            Title = "ArcGIS vector tiled layer (URL)";
            // Create the UI, setup the control references and execute initialization 
            Initialize();
        }

        private void Initialize()
        {
            // Create new Map
            var myMap = new Map();

            // Create uri to the vector tile service
            var serviceUrl = new Uri(
              "http://www.arcgis.com/sharing/rest/content/items/f96366254a564adda1dc468b447ed956/resources/styles/root.json");

            // Create new vector tile layer from the first url defined
            var vectorBasemap = new ArcGISVectorTiledLayer(serviceUrl);

            // Add created layer to the basemaps collection
            myMap.Basemap.BaseLayers.Add(vectorBasemap);

            // Assign the map to the MapView
            MyMapView.Map = myMap;
        }
    }
}
