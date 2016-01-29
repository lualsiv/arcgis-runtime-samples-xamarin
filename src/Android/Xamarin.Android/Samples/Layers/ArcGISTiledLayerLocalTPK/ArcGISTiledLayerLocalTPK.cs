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
using System;
using System.IO;

namespace ArcGISRuntimeXamarin.Samples.ArcGISTiledLayerLocalTPK
{
    [Activity(Label = "ArcGISTiledLayerLocalTPK")]
    public class ArcGISTiledLayerLocalTPK : Activity
    {
        MapView MyMapView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Initialize();

            Title = "ArcGIS tiled layer (local tpk)";
        }

        async void Initialize()
        {
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };

            var myMap = new Map();
            MyMapView = new MapView() { Map = myMap };

            layout.AddView(MyMapView);

            SetContentView(layout);

            LoadTPKLayer();
        }
        private async void LoadTPKLayer()
        {
            try
            {
                var pathToLayer = "/sdcard/campus.tpk";
              
                // Create a Uri to pass to the ArcGISTiledLayer constructor
                Uri myUri = new Uri("file://" + pathToLayer);

                // Check that the file exists
                if (!File.Exists(pathToLayer))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Sample Error");
                    alert.SetMessage("'" + pathToLayer + "'" + " not found");
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
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Sample Error");
                alert.SetMessage(ex.Message);
                alert.Show();
            }
        }
    }
}