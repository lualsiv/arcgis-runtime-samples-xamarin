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
using Esri.ArcGISRuntime.Layers;
using System;

namespace ArcGISRuntimeXamarin.Samples.MapRotation
{
    [Activity(Label = "MapRotation")]
    public class MapRotation : Activity
    {
        MapView MyMapView;
        int SliderValue = 0; // map rotation angle (degrees)

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Initialize();

            Title = "Map rotation";
        }


        void Initialize()
        {
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };

            // create a button to rotate the map
            var rotateMapButton = new Button(this);
            rotateMapButton.Click += RotateMap;

            // create a slider (SeekBar) control for selecting an angle between 0-180
            var angleSlider = new SeekBar(this);       
            // when the slider value ("Progress") changes, store the value   
            angleSlider.ProgressChanged += (object s, SeekBar.ProgressChangedEventArgs e) => 
            {
                SliderValue = e.Progress;
                rotateMapButton.Text = "Rotate " + SliderValue.ToString() + " degrees";
            };
            // set a maximum slider value of 180 and a current value of 90
            angleSlider.Max = 180;
            angleSlider.Progress = 90;

            // create a new map with a World Imagery basemap
            var basemap = Basemap.CreateImagery();
            var map = new Map(basemap);

            // create a new map view control to show the map
            MyMapView = new MapView();
            MyMapView.Map = map;

            // add controls and the map view to the layout
            layout.AddView(angleSlider);
            layout.AddView(rotateMapButton);
            layout.AddView(MyMapView);

            // apply the layout to the app
            SetContentView(layout);
        }

        private void RotateMap(object sender, EventArgs e)
        {
            // rotate the current map view viewpoint using the current slider value
            MyMapView.SetViewpointRotationAsync(SliderValue);
        }

    }
}