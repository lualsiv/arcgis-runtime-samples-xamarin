using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Geometry;
using System.Collections.Generic;
using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Layers;

namespace ArcGISRuntimeXamarin.Samples.ChangeViewpoint
{
  
    [Register("ChangeViewpointViewController")]
    public class ChangeViewpointViewController : UIViewController
    {
        private readonly MapPoint LondonCoords = new MapPoint(-13881.7678417696, 6710726.57374296, SpatialReferences.WebMercator);
        private readonly Double LondonScale = 8762.7156655228955;
        private readonly Polygon EdinburghEnvelope = new Polygon(new List<MapPoint> {
            (new MapPoint(-13049785.1566222, 4032064.6003424)),
            (new MapPoint(-13049785.1566222, 4040202.42595729)),
            (new MapPoint(-13037033.5780234, 4032064.6003424)),
            (new MapPoint(-13037033.5780234, 4040202.42595729))},
          SpatialReferences.WebMercator);
        private readonly Polygon RedlandsEnvelope = new Polygon(new List<MapPoint> {
            (new MapPoint(-354262.156621384, 7548092.94093301)),
            (new MapPoint(-354262.156621384, 7548901.50684376)),
            (new MapPoint(-353039.164455303, 7548092.94093301)),
            (new MapPoint(-353039.164455303, 7548901.50684376))},
            SpatialReferences.WebMercator);

        private MapView myMapView;
        public ChangeViewpointViewController()
        {
        }

        public async override void ViewDidLoad()
        {          
            base.ViewDidLoad();

            try
            {
                //First we create a new tiled layer and pass a Url to the service
                var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/World_Topo_Map/MapServer"));

                //We need to await the load call for the layer. This is required for layer to initialize all the metadata. If the layer is added without this load call, 
                //then it will not get initialized and no data will be visible on map.    
                await baseLayer.LoadAsync();

                //Create a basemap where we can add this baselayer
                var basemap = new Basemap();

                //Add the ArcGISTiledLayer that we created above to the basemap. 
                basemap.BaseLayers.Add(baseLayer);

                //Lets now create the UI

                //Create a variable to hold the height of the button that we will be adding later on in the UI.
                var height = 45;

                //Create a new MapView control and provide its location coordinates on the frame.
                myMapView = new MapView()
                {
                    Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height - height)
                    
                };

                //Create a new Map instance with the basemap that we created                
                Map myMap = new Map(basemap);

                //Assign this map to the MapView that was created above.
                myMapView.Map = myMap;

                //Create a segmented control to display buttons
                UISegmentedControl segmentControl = new UISegmentedControl() { BackgroundColor = UIColor.White };
                segmentControl.Frame = new CoreGraphics.CGRect(0, myMapView.Bounds.Height, View.Bounds.Width, height);
                segmentControl.InsertSegment("Geometry", 0, false);
                segmentControl.InsertSegment("Center & Scale", 1, false);
                segmentControl.InsertSegment("Animate", 2, false);

                segmentControl.ValueChanged += async(sender, e) => {
                    var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
                    //Do something with selectedSegmentId
                    switch(selectedSegmentId)
                    {
                        case 0:
                            //Set viewpoint using Rendlands envelope defined above and a padding of 20
                            await myMapView.SetViewpointGeometryAsync(RedlandsEnvelope,20);
                            break;
                        case 1:
                            //Set Viewpoint such that it is centered on the London coordinates defined above
                            await myMapView.SetViewpointCenterAsync(LondonCoords);
                            //Set Viewpoint's scale to match the specified scale 
                            await myMapView.SetViewpointScaleAsync(LondonScale);
                            break;
                        case 2:
                            //create a new Viewpoint using the specified geometry
                            var viewpoint = new Esri.ArcGISRuntime.Viewpoint(EdinburghEnvelope);
                            //Set Viewpoint of MapView to the Viewpoint created above and animate to it using a timespan of 5 seconds
                            await myMapView.SetViewpointAsync(viewpoint, System.TimeSpan.FromSeconds(5));
                            break;
                    }
                };

                //Finally add the MapView to the Subview
                View.AddSubviews(myMapView, segmentControl);
            }
            catch (Exception ex)
            {
                UIAlertController errorAlert = UIAlertController.Create("error", ex.Message, UIAlertControllerStyle.Alert);
            }
        }
    }
}