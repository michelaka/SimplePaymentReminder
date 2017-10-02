using LateListRem2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace LateListRem2
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            mLisView.ItemsSource = GetItemList("");
        }

        private void mLisView_Refreshing(object sender, EventArgs e)
        {
            mLisView.ItemsSource = GetItemList("");
            mLisView.EndRefresh();
        }



        List<LateItem> GetItemList(string searchString)
        {
            var itemsList = new List<LateItem> {
                new LateItem(){CustomerName ="Cust1", TransactionDate = System.DateTime.Today },
                new LateItem(){CustomerName ="Cust2", TransactionDate = System.DateTime.Today.AddDays(-1) }

            };

            if (String.IsNullOrWhiteSpace(searchString))
                return itemsList;

            return itemsList.Where(c => c.CustomerName.ToLower().Contains(searchString.ToLower())).ToList();

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            mLisView.ItemsSource = GetItemList(e.NewTextValue);
        }
    }
}
