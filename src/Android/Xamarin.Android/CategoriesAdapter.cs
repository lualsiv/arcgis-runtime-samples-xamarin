using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using ArcGISRuntimeXamarin.Managers;

namespace ArcGISRuntimeXamarin
{
    /// <summary>
    /// Custom ArrayAdapter to display the list of Categories
    /// </summary>
    class CategoriesAdapter : BaseAdapter<TreeItem>
    {

        List<TreeItem> items;
        Activity context;

        public CategoriesAdapter(Activity context, List<TreeItem> items) : base()
        {
            this.items = items;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override TreeItem this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.CategoriesLayout, parent, false);

            var name = view.FindViewById<TextView>(Resource.Id.nameTextView);

            name.Text = items[position].Name;

            return view;
        }
    }
}