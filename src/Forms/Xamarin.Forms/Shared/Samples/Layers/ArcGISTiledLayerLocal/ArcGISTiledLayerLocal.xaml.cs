using Esri.ArcGISRuntime.Xamarin.Forms.Controls;
using Esri.ArcGISRuntime.Layers;
using System;
using System.IO;

using Xamarin.Forms;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.ArcGISTiledLayerLocal
{
    public partial class ArcGISTiledLayerLocal : ContentPage
    {
        // MapView MyMapView;
        // Map MyMap;

        public ArcGISTiledLayerLocal()
        {
            InitializeComponent();

            Title = "ArcGIS tiled layer (local TPK)";

            // MyMap = new Map();
            // MyMapView = new MapView();
            //  MyMapView.Map = new Map();

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
