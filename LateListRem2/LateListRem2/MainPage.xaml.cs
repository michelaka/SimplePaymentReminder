using LateListRem2.Model;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace LateListRem2
{
    public partial class MainPage : ContentPage
    {
        private string mURI = "https://jsonplaceholder.typicode.com/posts";
        private HttpClient mClient = new HttpClient(new NativeMessageHandler());
        private ObservableCollection<PostItem> postItems;

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

        protected override void OnAppearing()
        {

            base.OnAppearing();
            
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var restMessageReply = await mClient.GetAsync(mURI);
            
            if (!restMessageReply.IsSuccessStatusCode)
                mTextResults.Text += " RestFul API Failed! ---";
            var restContent =  await restMessageReply.Content.ReadAsStringAsync();
            
            var _postItems = JsonConvert.DeserializeObject<List<PostItem>>(restContent);
            postItems = new ObservableCollection<PostItem>(_postItems);

            for(int i=0;i<=12;i++)
            {
                mTextResults.Text += string.Format("Object [{0}] has id {1} title = {2} || body = {3} \n",i,postItems[i].Id, postItems[i].Title, postItems[i].Body.Substring(0,10));
            }
            
        }
    }
}
