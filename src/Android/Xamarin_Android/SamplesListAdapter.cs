using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using ArcGISRuntimeXamarin.Models;

namespace ArcGISRuntimeXamarin
{
   /// <summary>
   /// Custom ArrayAdapter to display the list of Samples
   /// </summary>
   class SamplesListAdapter : BaseAdapter<SampleModel>
    {
        Activity context;
        List<SampleModel> items;

        public SamplesListAdapter(Activity context, List<SampleModel> sampleItems) : base()
        {
            this.context = context;
            this.items = sampleItems;
        }
        public override int Count
        {
            get { return items.Count; }
        }

        public override SampleModel this[int position]
        {
            get { return items[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = context.LayoutInflater.Inflate(Resource.Layout.SamplesLayout, parent, false);
            var name = view.FindViewById<TextView>(Resource.Id.sampleNameTextView);

            name.Text = items[position].Name;
            return view;
        }
    }
}