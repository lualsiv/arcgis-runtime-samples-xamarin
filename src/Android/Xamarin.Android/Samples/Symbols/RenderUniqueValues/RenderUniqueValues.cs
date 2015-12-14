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

using Android.App;
using Android.OS;
using Android.Widget;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Drawing;

namespace ArcGISRuntimeXamarin.Samples.RenderUniqueValues
{
    [Activity(Label = "RenderUniqueValues")]
    public class RenderUniqueValues : Activity
    {
        MapView MyMapView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Initialize();

            Title = "Unique value renderer";
            MyMapView.SpatialReferenceChanged += AddStatesLayer;
        }


        async void Initialize()
        {
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            var addLayerButton = new Button(this) { Text = "Add States" };
            addLayerButton.Click += AddStatesLayer;
            layout.AddView(addLayerButton);

            // create a new MapView 
            MyMapView = new MapView();

            // create a new Map with an imagery Basemap 
            var basemap = Basemap.CreateLightGrayCanvas();
            var map = new Map(basemap);

            // add the map to the map view
            MyMapView.Map = map;
    
            // add the map view to the layout then apply the layout to the app
            layout.AddView(MyMapView);
            SetContentView(layout);
        }

        private async void AddStatesLayer(object sender, EventArgs e)
        {
            // create a US states layer to symbolize with unique values
            var statesUri = new Uri("http://sampleserver6.arcgisonline.com/arcgis/rest/services/Census/MapServer/3"); // WorldTimeZones/MapServer/2");
            // create a service feature table using the service URI
            var statesFeatureTable = new ServiceFeatureTable(statesUri);
            // Add the "SUB_REGION" field to the outfields, will be used to render polygons in the layer
            statesFeatureTable.OutFields.Add("SUB_REGION");

            // Create a new feature layer using the service feature table
            var statesLayer = new FeatureLayer(statesFeatureTable);

            // Create a new unique value renderer
            var regionRenderer = new UniqueValueRenderer();
            // Add the "SUB_REGION" field to the renderer
            regionRenderer.FieldNames.Add("SUB_REGION");

            // Define a line symbol to use for the region fill symbols
            var stateOutlineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, Color.White, 0.7);
            // Define distinct fill symbols for a few regions (use the same outline symbol)
            var pacificFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, Color.Blue, stateOutlineSymbol);
            var mountainFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, Color.LawnGreen, stateOutlineSymbol);
            var westSouthCentralFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, Color.SandyBrown, stateOutlineSymbol);

            // Add values to the renderer: define the label, description, symbol, and attribute value for each
            regionRenderer.UniqueValues.Add(new UniqueValue("Pacific", "Pacific Region", pacificFillSymbol, "Pacific"));
            regionRenderer.UniqueValues.Add(new UniqueValue("Mountain", "Rocky Mountain Region", mountainFillSymbol, "Mountain"));
            regionRenderer.UniqueValues.Add(new UniqueValue("West South Central", "West South Central Region", westSouthCentralFillSymbol, "West South Central"));

            // Set the default region fill symbol (transparent with no outline) for regions not explicitly defined in the renderer
            var defaultFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Null, Color.Transparent, null);
            regionRenderer.DefaultSymbol = defaultFillSymbol;
            regionRenderer.DefaultLabel = "Other";

            // Apply the unique value renderer to the states layer
            statesLayer.Renderer = regionRenderer;

            //TEST - load the layer/table and check for exceptions
            try
            {
                await statesFeatureTable.LoadAsync();
            }            
            catch(Exception ex)
            {
                var loadStat = statesFeatureTable.LoadStatus;
                var loadEx = statesFeatureTable.LoadError;
            }

            try
            {
                await statesLayer.LoadAsync();
            }
            catch(Exception ex)
            {
                var loadStat = statesLayer.LoadStatus;
                var loadEx = statesLayer.LoadError;
            }
            // Add the new layer to the map
            MyMapView.Map.OperationalLayers.Add(statesLayer);
        }
    }
}