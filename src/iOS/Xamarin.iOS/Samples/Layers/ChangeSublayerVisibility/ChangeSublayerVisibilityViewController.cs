using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Foundation;
using System;

using UIKit;

namespace ArcGISRuntimeXamarin.Samples.ChangeSublayerVisibility
{
    [Register("ChangeSublayerVisibilityViewController")]
    public class ChangeSublayerVisibilityViewController : UIViewController
    {
        public ChangeSublayerVisibilityViewController()
        {
            Title = "Change sublayer visibility";
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create a new ArcGISMapImageLayer instance and pass a Url to the service
            var mapImageLayer = new ArcGISMapImageLayer(new Uri("http://sampleserver6.arcgisonline.com/arcgis/rest/services/SampleWorldCities/MapServer"));

            // Await the load call for the layer.
            await mapImageLayer.LoadAsync();

            // Create a new Map instance with the basemap               
            Map myMap = new Map(SpatialReferences.Wgs84);
            myMap.Basemap = Basemap.CreateTopographic();

            // Add the map image layer to the map's operational layers
            myMap.OperationalLayers.Add(mapImageLayer);

            // Create a new MapView control and provide its location coordinates on the frame.
            MapView myMapView = new MapView();
            myMapView.Frame = new CoreGraphics.CGRect(0, 70, View.Bounds.Width, View.Bounds.Height - 40);

            // Assign the Map to the MapView
            myMapView.Map = myMap;

            // Create a button to show sublayers
            UIButton sublayersButton = new UIButton(UIButtonType.Custom);
            sublayersButton.Frame = new CoreGraphics.CGRect(0, myMapView.Bounds.Height, View.Bounds.Width, 40);
            sublayersButton.BackgroundColor = UIColor.White;
            sublayersButton.SetTitle("Sublayers", UIControlState.Normal);
            sublayersButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);

            // Create a new instance of the Sublayers Table View Controller. This View Controller
            // displays a table of sublayers with a switch for setting the layer visibility. 
            SublayersTableViewController sublayersTableView = new SublayersTableViewController();

            // When the sublayers button is clicked, load the Sublayers Table View Controller
            sublayersButton.TouchUpInside += (s, e) =>
            {
                if (mapImageLayer.Sublayers.Count > 0)
                {
                    sublayersTableView.mapImageLayer = mapImageLayer;
                    this.NavigationController.PushViewController(sublayersTableView, true);
                }
            };

            // Add the MapView and Sublayers button to the View
            View.AddSubviews(myMapView, sublayersButton);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


