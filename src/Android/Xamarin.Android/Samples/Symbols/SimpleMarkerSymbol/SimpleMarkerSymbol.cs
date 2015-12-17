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
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;

namespace ArcGISRuntimeXamarin.Samples.SimpleMarkerSymbol
{
    [Activity(Label = "SimpleMarkerSymbol")]
    public class SimpleMarkerSymbol : Activity
    {
        MapView MyMapView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // call a function to initialize the map and display a red point graphic
            Initialize();

            Title = "Simple marker symbol";
        }

        void Initialize()
        {
            // Create a new vertical layout for the app
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };

            // Create a new map view control to display a map
            MyMapView = new MapView();

            // Create a new map with an imagery base map
            var myBasemap = Basemap.CreateImagery();
            var myMap = new Map(myBasemap);

            // Create a map point to use for graphic geometry
            var graphicPoint = new MapPoint(-226773, 6550477, SpatialReferences.WebMercator);

            // Set an initial viewpoint that centers the map on the graphic
            var graphicViewpoint = new Viewpoint(graphicPoint, 7500);
            myMap.InitialViewpoint = graphicViewpoint;

            // Add the map to the map view
            MyMapView.Map = myMap;

            // Create a new graphics overlay and add it to the map view
            var graphicsOverlay = new GraphicsOverlay();
            MyMapView.GraphicsOverlays.Add(graphicsOverlay);

            // Create a new simple marker symbol: red circle
            var colorRed = System.Drawing.Color.Red;
            var markerSymbol = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol(Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbolStyle.Circle, colorRed, 12); 
            
            // Create a new point graphic and add it to the overlay
            var myGraphic = new Graphic(graphicPoint, markerSymbol);
            graphicsOverlay.Graphics.Add(myGraphic);
           
            // Add the map view to the layout
            layout.AddView(MyMapView);

            // Show the layout in the app
            SetContentView(layout);
        }
    }
}