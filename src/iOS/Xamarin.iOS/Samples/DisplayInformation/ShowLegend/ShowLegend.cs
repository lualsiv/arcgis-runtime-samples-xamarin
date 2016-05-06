using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Mapping;

namespace ArcGISRuntimeXamarin.Samples.ShowLegend
{
    [Register("ShowLegend")]
    public class ShowLegend : UIViewController
    {
        // Create and hold reference to the used MapView
        private MapView _myMapView = new MapView();

        public ShowLegend()
        {
            this.Title = "Show Legend";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Create the UI, setup the control references and execute initialization
            CreateLayout();
            Initialize();
        }

        private void Initialize()
        {
            // Create new Map with basemap
            Map myMap = new Map(Basemap.CreateImageryWithLabels());

            // Provide used Map to the MapView
            _myMapView.Map = myMap;

            // Zoom to the last bookmark
            myMap.InitialViewpoint = myMap.Bookmarks.Last().Viewpoint;
        }

    }
}