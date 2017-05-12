using System;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using Akavache;
using CasterStats.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CasterStats.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //loginButton.Clicked += LoginButton_Cliked;

        }
        private async void LoginButton_Cliked(object sender, EventArgs e)
        {

            string name = UserEntry.Text;
            string password = PasswordEntry.Text;
            string uriString = "http://app.casterstats.com/login?user=" + name + "&password=" + password + "&rememberme=false";
            HttpResponseMessage resp;

            using (var client = new HttpClient())
            {
                var uri = new Uri(uriString);
                resp = await client.PostAsync(uri,null);
            }

            string logIdcookies = resp.Headers.GetValues("Set-Cookie").FirstOrDefault(x => x.Contains(".ASPXAUTH"));

            await BlobCache.LocalMachine.InsertObject("loginCookie", logIdcookies);


            var result = await resp.Content.ReadAsStringAsync();

            var r = JsonConvert.DeserializeObject<LoginResponse>(result);



            if (r.Success)
            {
                await Navigation.PushModalAsync(new MenuPage());

            }
            else
            {
                await DisplayAlert("Error", r.Msg, "Ok");
                UserEntry.Focus();
                PasswordEntry.Focus();
            }

            

        }
    }
}

