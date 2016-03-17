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
using System;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Geometry;
using System.Collections.Generic;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Mapping;

namespace ArcGISRuntimeXamarin.Samples.ChangeViewpoint
{
    [Register("ChangeViewpointViewController")]
    public class ChangeViewpointViewController : UIViewController
    {
        private readonly MapPoint LondonCoords = new MapPoint(-13881.7678417696, 6710726.57374296, SpatialReferences.WebMercator);
        private readonly Double LondonScale = 8762.7156655228955;
        private readonly Polygon RedlandsEnvelope = new Polygon(new List<MapPoint> {
            (new MapPoint(-13049785.1566222, 4032064.6003424)),
            (new MapPoint(-13049785.1566222, 4040202.42595729)),
            (new MapPoint(-13037033.5780234, 4032064.6003424)),
            (new MapPoint(-13037033.5780234, 4040202.42595729))},
          SpatialReferences.WebMercator);
        private readonly Polygon EdinburghEnvelope = new Polygon(new List<MapPoint> {
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
                // Create a new tiled layer and pass a Url to the service
                var baseLayer = new ArcGISTiledLayer(new Uri("http://services.arcgisonline.com/arcgis/rest/services/World_Topo_Map/MapServer"));

                // TODO - Remove this once #2915 is fixed
                await baseLayer.LoadAsync();

                // Create a basemap to add the baselayer
                var myBasemap = new Basemap();

                // Add the ArcGISTiledLayer created above to the basemap. 
                myBasemap.BaseLayers.Add(baseLayer);

                // Create a variable to hold the height of the segmented control.
                var height = 45;

                // Create a new MapView control and provide its location coordinates on the frame.
                myMapView = new MapView()
                {
                    Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, View.Bounds.Height - height)
                };

                // Create a new Map instance with the basemap               
                Map myMap = new Map(SpatialReferences.WebMercator) { Basemap = myBasemap };

                // Assign the Map to the MapView
                myMapView.Map = myMap;

                // Create a segmented control to display buttons
                UISegmentedControl segmentControl = new UISegmentedControl() { BackgroundColor = UIColor.White };
                segmentControl.Frame = new CoreGraphics.CGRect(0, myMapView.Bounds.Height, View.Bounds.Width, height);
                segmentControl.InsertSegment("Geometry", 0, false);
                segmentControl.InsertSegment("Center & Scale", 1, false);
                segmentControl.InsertSegment("Animate", 2, false);

                segmentControl.ValueChanged += async (sender, e) =>
                {
                    var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;

                    switch (selectedSegmentId)
                    {
                        case 0:
                            // Set Viewpoint using Redlands envelope defined above and a padding of 20
                            await myMapView.SetViewpointGeometryAsync(RedlandsEnvelope, 20);
                            break;
                        case 1:
                            // Set Viewpoint so that it is centered on the London coordinates defined above
                            await myMapView.SetViewpointCenterAsync(LondonCoords);
                            // Set the Viewpoint scale to match the specified scale 
                            await myMapView.SetViewpointScaleAsync(LondonScale);
                            break;
                        case 2:
                            // Navigate to full extent of the first baselayer before animating to specified geometry
                            await myMapView.SetViewpointAsync(new Viewpoint(baseLayer.FullExtent));
                            // Create a new Viewpoint using the specified geometry
                            var viewpoint = new Viewpoint(EdinburghEnvelope);
                            // Set Viewpoint of MapView to the Viewpoint created above and animate to it using a timespan of 5 seconds
                            await myMapView.SetViewpointAsync(viewpoint, System.TimeSpan.FromSeconds(5));
                            break;
                    }
                };

                // Add the MapView to the Subview
                View.AddSubviews(myMapView, segmentControl);
            }
            catch (Exception ex)
            {
                UIAlertController errorAlert = UIAlertController.Create("error", ex.Message, UIAlertControllerStyle.Alert);
            }
        }
    }
}