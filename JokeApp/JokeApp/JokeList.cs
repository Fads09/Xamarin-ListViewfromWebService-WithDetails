﻿using System; using System.Collections.ObjectModel; using System.Linq; using System.Windows.Input; using Newtonsoft.Json; using Xamarin.Forms; using static JokeApp.Joke;   namespace JokeApp {     public class JokeList : ContentPage     {          ObservableCollection<object> jokesCollection = new ObservableCollection<object>();         Joke jokeDisplay = new Joke();         ListView listViewJson = new ListView();                public JokeList()         {             GetJSON();              ViewModel = new PullToRefresh();              this.BindingContext = ViewModel;                listViewJson = new ListView             {                 HorizontalOptions = LayoutOptions.FillAndExpand,                 VerticalOptions = LayoutOptions.FillAndExpand,                 IsPullToRefreshEnabled = true,                 RefreshCommand = LoadTestCommand,             } ;              listViewJson.SetBinding(ListView.IsRefreshingProperty, "IsBusy", BindingMode.OneWay);         }          public async void GetJSON()         {             try             {                 var client = new System.Net.Http.HttpClient();                 var response = await client.GetAsync("http://api.icndb.com/jokes");                 string json = await response.Content.ReadAsStringAsync();                  listViewJson.HasUnevenRows = true;                  listViewJson.ItemSelected += listViewJson_ItemSelected;                   if (json != "")                 {                     jokeDisplay = JsonConvert.DeserializeObject<Joke>(json);                 }                 DataTemplate template = new                 DataTemplate(typeof(CustomCell));                 listViewJson.ItemTemplate = template;                 listViewJson.IsPullToRefreshEnabled = true;                   listViewJson.ItemsSource = jokesCollection;                 for (int i = 0; i < 10; i++)                 {                     jokesCollection.Add(jokeDisplay.value.ElementAt(i));                 }                  listViewJson.ItemAppearing += (object sender, ItemVisibilityEventArgs e) => {                     var viewCellDetails = e.Item as object;                     int viewCellIndex = jokesCollection.IndexOf(viewCellDetails);                     if (viewCellIndex == jokesCollection.Count - 1)                     {                         var page = (jokesCollection.Count / 10);                         //skip already shown, add new ones                         for (int i = page * 10; i < (page * 10) + 10; i++)                         {                             jokesCollection.Add(jokeDisplay.value.ElementAt(i));                         }                      }                 } ;                   listViewJson.IsPullToRefreshEnabled = true;                  Content = listViewJson;             }             catch (InvalidCastException e)             {                 throw e;             }         }         private void listViewJson_ItemSelected(object sender, SelectedItemChangedEventArgs e)         {             var item = e.SelectedItem as Value;              Navigation.PushAsync(new JokeDetail(item));          }         private PullToRefresh ViewModel { get; set; }          private Command loadTestCommand;          public Command LoadTestCommand         {             get             {                 return loadTestCommand ?? (loadTestCommand = new Command(ExecuteLoadTestCommand, () => { return !ViewModel.IsBusy; } ));             }         }          private async void ExecuteLoadTestCommand()         {             if (ViewModel.IsBusy) return;             ViewModel.IsBusy = true;             LoadTestCommand.ChangeCanExecute();             //DoStuff              listViewJson.ItemsSource = jokesCollection;              ViewModel.IsBusy = false;             LoadTestCommand.ChangeCanExecute();             listViewJson.EndRefresh();         }       }      public class CustomCell : ViewCell     {         public CustomCell()         {             //instantiate each of our views             Label lblJoke = new Label();             lblJoke.LineBreakMode = LineBreakMode.WordWrap;             lblJoke.SetBinding(Label.TextProperty, "joke");             View = lblJoke;           }     } } 