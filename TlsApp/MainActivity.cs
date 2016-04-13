using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using ModernHttpClient;
using System.Net.Http.Headers;
using Shared;

namespace TlsApp
{
    [Activity(Label = "TlsApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            try
            {
                var lookup = new LookupAPI();
                await lookup.GetLookupAsync();

                alert.SetTitle("OK");
                alert.Show();
            }catch(Exception ex)
            {
                alert.SetTitle(ex.Message);
                alert.Show();
            }

        }
    }
}

