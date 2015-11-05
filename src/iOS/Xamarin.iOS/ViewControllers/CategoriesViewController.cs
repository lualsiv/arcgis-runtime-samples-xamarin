using ArcGISRuntimeXamarin.Managers;
using CoreGraphics;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using ArcGISRuntimeXamarin.Samples;

namespace ArcGISRuntimeXamarin
{
	partial class CategoriesViewController : UITableViewController
	{
		public CategoriesViewController(IntPtr handle)
			: base(handle)
		{

		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			await SampleManager.Current.InitializeAsync();
			var data = SampleManager.Current.GetSamplesAsTree();
			this.TableView.Source = new CategoryDataSource(this, data);

			this.TableView.ReloadData();

		}

		public class CategoryDataSource : UITableViewSource
		{
			private UITableViewController controller;
			private List<TreeItem> data;

			static string CELL_ID = "cellid";

			public CategoryDataSource(UITableViewController controller, List<TreeItem> data)
			{
				this.data = data;
				this.controller = controller;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(CELL_ID, indexPath);
				var item = data[indexPath.Row];
				cell.TextLabel.Text = item.Name;
				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return data.Count;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				try
				{
					var selected = data[indexPath.Row];
					controller.NavigationController.PushViewController(new SamplesViewController(selected), true);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
