using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using static JokeApp.Joke;
namespace JokeApp
{
    class JokeDetail : ContentPage
    {
        private Value val;
        Label labl = new Label { Text = "Chuck Norris Jokes",
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 20,
            TextColor = Color.Purple,
            FontAttributes = FontAttributes.Bold};
        
        Label lblJoke = new Label{ Text = ""};

        public JokeDetail(Value v)
        {
            val = v;

            Display();

        }

        public void Display()
        {
            try
            {
                
                lblJoke.LineBreakMode = LineBreakMode.WordWrap;

                lblJoke.Text = val.joke;


            }
            catch (Exception e)
            {
                throw e;
            }
           
            var stack = new StackLayout { 
                
                Spacing = 10,
                Padding = 20,
                VerticalOptions = LayoutOptions.CenterAndExpand, 
                Children = {labl, lblJoke}};

            Content = stack;

               

          
        }


    }
}