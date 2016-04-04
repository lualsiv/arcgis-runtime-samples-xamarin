using System;
using System.Drawing;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;

namespace ArcGISRuntimeXamarin.Samples.SimpleMarkerSymbol
{
    [Register("SimpleMarkerSymbol")]
    public class SimpleMarkerSymbol : UIViewController
    {
        public SimpleMarkerSymbol()
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create a new tiled layer and pass a Uri to the service
            var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer"));

            // Load the base layer 
            await baseLayer.LoadAsync();

            // Create a basemap and add the base layer
            var myBasemap = new Basemap();
            myBasemap.BaseLayers.Add(baseLayer);

            // Create a new MapView control and provide its location coordinates on the frame
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height)
            };

            // Create a new map instance and add the basemap
            Map myMap = new Map(myBasemap);


            // Assign this Map to the MapView
            myMapView.Map = myMap;

            // Create a new instance of GraphicsOverlay where we can create graphics with simple marker symbol 
            GraphicsOverlay graphicsOverlay = new GraphicsOverlay()
            {
                RenderingMode = GraphicsRenderingMode.Static
            };

            // Create a MapPoint where the Graphic will be located
            var mapPoint = new MapPoint(-117.195646, 34.056397, SpatialReferences.Wgs84);

            // TODO: #2914
            // Center the map view extent on the point graphic
            // myMap.InitialViewpoint = new Viewpoint(mapPoint, 24000);

            // Create a new red circle marker symbol
            var circleSymbol = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol()
            {
                Style = SimpleMarkerSymbolStyle.Circle,
                Size = 14,
                Color = Color.Red
            };

            // Create a new graphic using the map point and the symbol
            var pointGraphic = new Graphic(mapPoint, circleSymbol);

            // Add the graphic to the graphicsOverlay
            graphicsOverlay.Graphics.Add(pointGraphic);

            // Add the graphicsOverlay to MapView's GraphicsOverlays collection
            myMapView.GraphicsOverlays.Add(graphicsOverlay);

            // Center the map view extent on the point graphic          
            myMapView.SetViewpoint(new Viewpoint(mapPoint, 24000));

            // Add the MapView to the Subview
            View.AddSubview(myMapView);


        }
    }
}