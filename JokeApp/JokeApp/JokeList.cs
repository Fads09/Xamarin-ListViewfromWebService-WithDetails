﻿using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using static JokeApp.Joke;

namespace JokeApp
{
    public class JokeList : ContentPage
    {
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
                var jokeDisplay = new Joke();
                ListView listViewJson = new ListView();
                listViewJson.HasUnevenRows = true;
                if (json != "")
                {
                    jokeDisplay = JsonConvert.DeserializeObject<Joke>(json);
                }
                DataTemplate template = new
                DataTemplate(typeof(CustomCell));
                listViewJson.ItemTemplate = template;
                listViewJson.ItemsSource = jokeDisplay.value;
                Content = listViewJson;
            }
            catch (InvalidCastException e)
            {
                throw e;
            }
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