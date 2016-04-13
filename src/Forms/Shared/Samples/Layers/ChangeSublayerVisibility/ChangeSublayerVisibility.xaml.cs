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

namespace ArcGISRuntimeXamarin.Samples.ChangeSublayerVisibility
{
    public partial class ChangeSublayerVisibility : ContentPage
    {
        private ArcGISMapImageLayer _imageLayer;

        public ChangeSublayerVisibility()
        {
            InitializeComponent();

            Title = "Change sublayer visibility";
            // Create the UI, setup the control references and execute initialization 
            Initialize();
        }

        private void Initialize()
        {
            // Create new Map
            Map myMap = new Map();

            // Create uri to the map image layer
            var serviceUri = new Uri(
               "http://sampleserver6.arcgisonline.com/arcgis/rest/services/SampleWorldCities/MapServer");

            // Create new image layer from the url
            _imageLayer = new ArcGISMapImageLayer(serviceUri);

            // Add created layer to the basemaps collection
            myMap.Basemap.BaseLayers.Add(_imageLayer);

            // Assign the map to the MapView
            MyMapView.Map = myMap;
        }

        private async void OnSublayersClicked(object sender, EventArgs e)
        {
            // Make sure that layer and it's sublayers are loaded
            // If layer is already loaded, this returns directly
            await _imageLayer.LoadAsync();

            // Create layout for sublayers page
            // Create root layout
            var layout = new StackLayout();

            // Create list for the layers
            var sublayersListView = new ListView();

            // Create Item template for each items in the list
            sublayersListView.ItemTemplate = new DataTemplate(() =>
            {
                // Using switch cells that provides on/off functionality
                SwitchCell cell = new SwitchCell();
                // Set data binding between the SwitchCell and ArcGISSublayers
                cell.SetBinding(SwitchCell.TextProperty, new Binding("Name"));
                cell.SetBinding(SwitchCell.OnProperty, new Binding("IsVisible"));
                return cell;
            });

            // Set items source to get the content from sublayers
            sublayersListView.ItemsSource = _imageLayer.Sublayers;

            // Add list to the root layout
            layout.Children.Add(sublayersListView);

            // Create internal page for the navigation page
            var sublayersPage = new ContentPage() { Content = layout, Title = "Sublayers" };
                        
            // Navigate to the sublayers page
            await Navigation.PushAsync(sublayersPage);
        }
    }
}
