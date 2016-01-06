using System;
using UIKit;
using Foundation;
using Esri.ArcGISRuntime.Layers;
using System.Collections.Generic;

namespace ArcGISRuntimeXamarin.Samples.ChangeSublayerVisibility
{
    [Register("SublayersTableViewController")]
    public class SublayersTableViewController : UITableViewController
    {
        public ArcGISMapImageLayer mapImageLayer;

        public SublayersTableViewController()
        {
            Title = "Sublayers";
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            List<ArcGISSublayer> sublayers = new List<ArcGISSublayer>();

            // Add all the map image layer sublayers to a list and then pass that list to the SublayerDataSource
            if (mapImageLayer != null)
            {
                foreach (var item in mapImageLayer.Sublayers)
                {
                    sublayers.Add(item);
                }

                TableView.Source = new SublayerDataSource(sublayers);
                TableView.Frame = new CoreGraphics.CGRect(0, 0, 100, 100);
            }
        }
    }

    public class SublayerDataSource : UITableViewSource
    {
        private List<ArcGISSublayer> sublayers;

        static string CELL_ID = "cellid";

        public SublayerDataSource(List<ArcGISSublayer> sublayers)
        {
            this.sublayers = sublayers;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Create the cells in the table
            var cell = new UITableViewCell(UITableViewCellStyle.Default, CELL_ID);

            var sublayer = sublayers[indexPath.Row] as ArcGISMapImageSublayer;
            cell.TextLabel.Text = sublayer.Name;

            // Create a UISwitch for controlling the layer visibility
            var visibilitySwitch = new UISwitch()
            {
                Frame = new CoreGraphics.CGRect(cell.Bounds.Width - 60, 7, 50, cell.Bounds.Height)
            };
            visibilitySwitch.Tag = indexPath.Row;
            visibilitySwitch.On = sublayer.IsVisible;
            visibilitySwitch.ValueChanged += VisibilitySwitch_ValueChanged;

            // Add the UISwitch to the cell's content view
            cell.ContentView.AddSubview(visibilitySwitch);

            return cell;
        }

        private void VisibilitySwitch_ValueChanged(object sender, EventArgs e)
        {
            // Get the row containing the UISwitch that was changed
            var index = (sender as UISwitch).Tag;

            // Set the sublayer visibility according to the UISwitch setting
            var sublayer = sublayers[(int)index] as ArcGISMapImageSublayer;
            sublayer.IsVisible = (sender as UISwitch).On;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return sublayers.Count;
        }
    }
}