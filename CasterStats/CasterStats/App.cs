using Akavache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CasterStats
{
    public class App : Application
    {
        public static double ScreenHeight;
        public static double ScreenWidth;
        public App()
        {

            MainPage = new Views.LoginPage();
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
