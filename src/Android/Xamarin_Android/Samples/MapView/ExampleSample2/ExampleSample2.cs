
using Android.App;
using Android.OS;
using Android.Widget;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using System;

namespace ArcGISRuntimeXamarin.Samples.ExampleSample2
{
    [Activity(Label = "ExampleSample2")]
    public class ExampleSample2 : Activity
    {
        MapView MyMapView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Initialize();

            Title = "Example Sample 2";
            
        }


        async void Initialize()
        {
            var layout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            var zoomInButton = new Button(this) { Text = "Zoom In" };
            zoomInButton.Click += ZoomInButton_Click; // TODO - Implement below
            layout.AddView(zoomInButton);

            var zoomOutButton = new Button(this) { Text = "Zoom Out" };
            zoomOutButton.Click += zoomOutButton_Click; // TODO - Implement below
            layout.AddView(zoomOutButton);

            var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer"));
            var labelsLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer"));
            var basemap = new Basemap();
            basemap.BaseLayers.Add(baseLayer);
            basemap.ReferenceLayers.Add(labelsLayer);

            var transportationLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/ArcGIS/rest/services/Reference/World_Transportation/MapServer"))
            {
                IsVisible = false
            };

            var m = new Map(SpatialReferences.WebMercator) { Basemap = basemap };
            m.OperationalLayers.Add(transportationLayer);

            await m.LoadAsync();

            MyMapView = new MapView();
            MyMapView.Map = m;
            layout.AddView(MyMapView);

            // This is your key bit for getting the layout you just created to show in the app
            SetContentView(layout);

        }

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Not Implemented");
            builder.SetMessage("Zoom In not implemented");
            builder.Show();
            
            
            throw new NotImplementedException();
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();

        }
    }
}