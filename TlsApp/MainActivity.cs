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

    public class Lookup
    {

    }

    public class LookupAPI
    {
        public async Task<Lookup> GetLookupAsync()
        {
            using (var client = HttpClientExtensions.Instance())
            {
                var payload = await client.GetJsonAsync<Lookup>("v2/lookup/").ConfigureAwait(false);
                return payload;
            };
        }
    }

    public static class HttpClientExtensions
    {
        public static async Task<V> GetJsonAsync<V>(this HttpClient self, string url)
        {
            var response = await self.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            var resultPayload = JsonConvert.DeserializeObject<V>(result);
            return resultPayload;
        }

        public static HttpClient Instance()
        {
            var handler = new NativeMessageHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Automatic;

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://migrantfootprints.info");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}

