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
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.AccessLoadStatus
{
    [Activity(Label = "Access load status")]
    public class AccessLoadStatus : Activity
    {
        private TextView _loadStatusTextView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Title = "Access load status";
            InitializeView();        
        }

        private void InitializeView()
        {
            // Create layout and other controls into the layout
            //var layout = CreateLayout();

            var myMapView = CreateLayout();

            // Create a new map view control to display the map
        //    var myMapView = new MapView();
           
            // Create new Map with basemap
            var myMap = new Map(Basemap.CreateImagery());
           
            // Register to handle loading status changes
            myMap.LoadStatusChanged += OnMapsLoadStatusChanged;
            
            // Provide used Map to the MapView
            myMapView.Map = myMap;

            //// Add the map view to the layout
            //layout.AddView(myMapView);

            //// Show the layout in the app
            //SetContentView(layout);
        }

        private void OnMapsLoadStatusChanged(object sender, LoadStatusEventArgs e)
        {
            // Make sure that the UI changes are done in the UI thread
            RunOnUiThread(() =>
            {
                // Update the load status information
                _loadStatusTextView.Text = string.Format(
                    "Maps' load status : {0}", 
                    e.Status.ToString());
            });
        }

        private MapView CreateLayout()
        {
            // Create a new vertical layout for the app
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };

            // Create control to show the maps' loading status
            _loadStatusTextView = new TextView(this);
            layout.AddView(_loadStatusTextView);

            // Create a new map view control to display the map
            var myMapView = new MapView();

            // Add the map view to the layout
            layout.AddView(myMapView);

            // Show the layout in the app
            SetContentView(layout);

            return myMapView;
        }
    }
}