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

namespace ArcGISRuntimeXamarin.Samples.ArcGISTiledLayerLocal
{
    public partial class ArcGISTiledLayerLocal : ContentPage
    {
        public ArcGISTiledLayerLocal()
        {
            InitializeComponent();

            Title = "ArcGIS tiled layer (local TPK)";

            LoadTPKLayer();
        }

        private async void LoadTPKLayer()
        {
            try
            {
                // Create a Uri to pass to the ArcGISTiledLayer constructor
                Uri myUri;
                myUri = new Uri("campus.tpk", UriKind.Relative);

                Device.OnPlatform(Android: () => myUri = new Uri("file:///" + "sdcard/campus.tpk"));

                var layer = new ArcGISTiledLayer(myUri) { Name = "campus_map" };
                await layer.LoadAsync();

                MyMapView.Map.OperationalLayers.Add(layer);
                await MyMapView.Map.RetryLoadAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Sample Error", ex.Message, "OK");
            }
        }
    }
}
