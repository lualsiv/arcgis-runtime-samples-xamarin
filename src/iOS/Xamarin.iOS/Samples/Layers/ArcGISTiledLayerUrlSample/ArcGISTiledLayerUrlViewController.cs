using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime;

namespace ArcGISRuntimeXamarin.Samples.ArcGISTiledLayerUrlSample
{
   
    [Register("ArcGISTiledLayerUrlViewController")]
    public class ArcGISTiledLayerUrlViewController : UIViewController
    {
        public ArcGISTiledLayerUrlViewController()
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //First we create a new tiled layer and pass a Url
            var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/NatGeo_World_Map/MapServer"));

            //We need to await the load call for the layer. This is required for layer to initialize. If the layer is added without this load call, 
            //then it will not get initialized and no data will be visible on map.    
            await baseLayer.LoadAsync();

            //Create a basemap where we can add this baselayer
            var basemap = new Basemap();

            //Add the tiled layer to the basemap. 
            basemap.BaseLayers.Add(baseLayer);

            //Now lets create our UI.

            //Create a variable to hold the Y coordinate of the control
            var yOffset = 70;

            //Create a new mapview control and provide its location coordinates on the frame
            MapView myMapView = new MapView()
            {
                Frame = new CoreGraphics.CGRect(0, yOffset, View.Bounds.Width, View.Bounds.Height - yOffset)
            };

            //Create a new map instance and set its basemap
            Map myMap = new Map(basemap);


            //Assign this map as the Map of the mapview that was created above.
            myMapView.Map = myMap;

            //Finally add the mapview to the Subview
            View.AddSubview(myMapView);
        }
    }
}