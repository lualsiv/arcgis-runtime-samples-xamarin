using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;

namespace ArcGISRuntimeXamarin.Samples.SimpleMarkerSymbol
{
    [Register("SimpleMarkerSymbolViewController")]
    public class SimpleMarkerSymbolViewController : UIViewController
    {
        public SimpleMarkerSymbolViewController()
        {
        }

        public async override void ViewDidLoad()
        {

            base.ViewDidLoad();

            //First we create a new tiled layer and pass a Url to the service
            var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/World_Imagery/MapServer"));

            //We need to await the load call for the layer. This is required for layer to initialize all the metadata. If the layer is added without this load call, 
            //then it will not get initialized and no data will be visible on map.    
            await baseLayer.LoadAsync();

            //Create a basemap where we can add this baselayer
            var basemap = new Basemap();

            //Add the ArcGISTiledLayer that we created above to the basemap. 
            basemap.BaseLayers.Add(baseLayer);


            //Now lets create our UI.
            //Create a new mapview control and provide its location coordinates on the frame.
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height)
            };

            //Create a new map instance which holds basemap that was created
            Map myMap = new Map(basemap);

            //Assign this map to the mapview that was created above.
            myMapView.Map = myMap;

            //Create a new instance of GraphicsOverlay where we can create graphics with simple marker symbol 
            GraphicsOverlay graphicsOverlay = new GraphicsOverlay()
            {
                RenderingMode = GraphicsRenderingMode.Static
            };

            //Create a new MapPoint where new Graphic will be located
            var mapPoint = new MapPoint(-117.195646, 34.056397, SpatialReferences.Wgs84);

            // Create a new red circle marker symbol
            var circleSymbol = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol()
            {
                Style= SimpleMarkerSymbolStyle.Circle,
                Size=14
            };

            // Create a new graphic using the map point and the symbol
            var pointGraphic = new Graphic(mapPoint, circleSymbol);

            // Add the graphic to the map
            graphicsOverlay.Graphics.Add(pointGraphic);

            //Add graphicsOverlay to MapView's GraphicsOverlay collection
            myMapView.GraphicsOverlays.Add(graphicsOverlay);

            // Center the map view extent on the point graphic          
            myMapView.SetViewpoint(new Viewpoint(mapPoint, 24000));

            //Finally add the mapview to the Subview
            View.AddSubview(myMapView);
        }
    }
}