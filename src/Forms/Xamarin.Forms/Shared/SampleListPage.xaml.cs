using System;
using System.Collections.Generic;
using System.Linq;
using ArcGISRuntimeXamarin.Models;
using Xamarin.Forms;
using ArcGISRuntimeXamarin.Managers;

namespace ArcGISRuntimeXamarin
{
    public partial class SampleListPage : ContentPage
    {
        string _categoryName;
        List<SampleModel> _listSampleItems;
        public SampleListPage(string name)
        {
            this._categoryName = name;
            Initialize();

            InitializeComponent();

            Title = this._categoryName;
        }

        void Initialize()
        {
            var sampleCategories = SampleManager.Current.GetSamplesAsTree();
            var category = sampleCategories.FirstOrDefault(x => x.Name == this._categoryName) as TreeItem;

            List<object> listSubCategories = new List<object>();
            for (int i = 0; i < category.Items.Count; i++)
            {
                listSubCategories.Add((category.Items[i] as TreeItem).Items);
            }

            _listSampleItems = new List<SampleModel>();
            foreach (List<object> subCategoryItem in listSubCategories)
            {
                foreach (var sample in subCategoryItem)
                {
                    _listSampleItems.Add(sample as SampleModel);
                }
            }

            BindingContext = _listSampleItems;
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (SampleModel)e.Item;
            var sampleName = item.SampleName;
            var sampleNamespace = item.SampleNamespace;

            Type t = Type.GetType(sampleNamespace + "." + sampleName);

            await Navigation.PushAsync((ContentPage)Activator.CreateInstance(t));
        }
    }
}
