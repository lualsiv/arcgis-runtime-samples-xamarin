using ArcGISRuntimeXamarin.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ArcGISRuntimeXamarin
{
	public partial class CategoryListPage : ContentPage
	{
        List<TreeItem> _sampleCategories;
		public CategoryListPage ()
		{
            Initialize();
			InitializeComponent ();
		}

        async void Initialize()
        {
            await SampleManager.Current.InitializeAsync();
            _sampleCategories= SampleManager.Current.GetSamplesAsTree();
            BindingContext = _sampleCategories;

        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as TreeItem;

            await Navigation.PushAsync(new SampleListPage(item.Name));
        }
    }
}
