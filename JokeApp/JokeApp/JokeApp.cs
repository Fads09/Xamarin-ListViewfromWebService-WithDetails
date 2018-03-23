using System;

using Xamarin.Forms;

namespace JokeApp
{
    public class App : Application
    {
        public App()
        {
            //var tabsCs = new TabbedPage { Title = "ListView" };
            //tabsCs.Children.Add(new JokeList { Title = "Basic" });
            MainPage = new NavigationPage(new JokeList());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
