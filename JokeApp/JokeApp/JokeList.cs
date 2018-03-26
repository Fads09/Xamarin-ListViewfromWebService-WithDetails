using System;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;
using static JokeApp.Joke;

namespace JokeApp
{
    public class JokeList : ContentPage
    {
        ObservableCollection<object> jokesCollection = new ObservableCollection<object>();         Joke jokeDisplay = new Joke();
        public JokeList()
        {
            GetJSON();
        }

        public async void GetJSON()
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync("http://api.icndb.com/jokes");
                string json = await response.Content.ReadAsStringAsync();

                ListView listViewJson = new ListView();
                listViewJson.HasUnevenRows = true;
                listViewJson.ItemSelected += listViewJson_ItemSelected;
                if (json != "")
                {
                    jokeDisplay = JsonConvert.DeserializeObject<Joke>(json);
                }
                DataTemplate template = new
                DataTemplate(typeof(CustomCell));
                listViewJson.ItemTemplate = template;
                listViewJson.ItemsSource = jokesCollection;

                for (int i = 0; i < 11; i++ )
                {
                    jokesCollection.Add(jokeDisplay.value.ElementAt(i));
                }

                listViewJson.ItemAppearing += (object sender, ItemVisibilityEventArgs e) =>
                {
                    var viewCell = e.Item as object;
                    var viewIndex = jokesCollection.IndexOf(viewCell);
                    if (viewIndex == jokesCollection.Count - 1)                     {                         var page = (jokesCollection.Count / 10);                         //skip already shown, add new ones                         for (int i = page * 10; i < (page * 10) + 10; i++)                         {                             jokesCollection.Add(jokeDisplay.value.ElementAt(i));                         }                      } 

                };
                Content = listViewJson;
            }
            catch (InvalidCastException e)
            {
                throw e;
            }
        }

        async private void listViewJson_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;

            await Navigation.PushAsync(new NavigationPage(new JokeDetail((JokeApp.Value)item)));
        }
    }

    public class CustomCell : ViewCell
    {
        public CustomCell()
        {

            Label lblJoke = new Label();
            lblJoke.LineBreakMode = LineBreakMode.TailTruncation;
            lblJoke.SetBinding(Label.TextProperty, "joke");
            View = lblJoke;
        }
    }
}