using RestSharp;
using System.Threading;
using System.Timers;
using Timer = System.Threading.Timer;

namespace AYASOMPO.CronReminder.Services
{
    public class TimerService
    {
        private Timer _timer;
        private List<string>? _urls;

        public void SetTimer(List<string> urls, int minute)
        {
            _urls = urls;

            int interval = minute * 60 * 1000;

            _timer = new Timer(OnTimedEvent, null, TimeSpan.Zero, TimeSpan.FromMinutes(minute));
        }

        private async void OnTimedEvent(object state)
        {
            if (_urls != null)
            {
                foreach (var url in _urls)
                {
                    await MakeRequest(url);
                }
            }
        }

        private async Task MakeRequest(string url)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url, Method.Get); //HTTP requests (GET, POST, PUT, etc.)
            var response = await client.ExecuteAsync(request); 

            if (response.IsSuccessful)
                Console.WriteLine(response.StatusCode);
            else
                Console.WriteLine($"Error: {response.StatusCode}");

            Console.WriteLine($"Request made to {url} at {DateTime.Now:HH:mm:ss.fff}");
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        #region for One Url(Not Used)
        //private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    //using (var client = new HttpClient())
        //    //{
        //    //    client.BaseAddress = new Uri("https://localhost:7169");
        //    //    var response = await client.GetAsync("");
        //    //    Console.WriteLine("\nCall Success...\n");
        //    //}
        //    //string endpoint = "https://localhost:7169";
        //    //string endpoint = "http://localhost:8024";

        //    string endpoint = _url;
        //    RestClient client = new RestClient();
        //    RestRequest request = new RestRequest(endpoint, Method.Get);
        //    var response = await client.GetAsync(request);

        //    if (response.IsSuccessful)
        //        Console.WriteLine(response.StatusCode);
        //    else
        //        Console.WriteLine($"Error: {response.StatusCode}");

        //    Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",                              e.SignalTime);
        //}
        #endregion
    }
}
