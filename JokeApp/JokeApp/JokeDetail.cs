using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using static JokeApp.Joke;
namespace JokeApp
{
    class JokeDetail : ContentPage
    {
        private Value val;

        public JokeDetail(Value v)
        {
            val = v;

            Display();
        }

        public void Display()
        {
            try
            {
                Label lblJoke = new Label();
                lblJoke.LineBreakMode = LineBreakMode.WordWrap;

                lblJoke.Text = val.joke;

                Content = lblJoke;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}