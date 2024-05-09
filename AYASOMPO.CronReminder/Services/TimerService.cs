using RestSharp;
using System.Threading;
using System.Timers;

namespace AYASOMPO.CronReminder.Services
{
    public class TimerService
    {
        private System.Timers.Timer aTimer;
        private List<string>? _urls;

        public void SetTimer(List<string> urls, int time)
        {
            _urls = urls;

            time = (int)TimeSpan.FromMinutes(time).TotalMilliseconds;
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(time);
            // Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += OnTimedEvent;
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true;

            for (int i = 0; i < urls.Count; i++)
            {
                aTimer.Elapsed += (sender, e) => OnTimedEvent(urls[i], e);
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
                //aTimer.Add(timer);
            }
        }

        private async void OnTimedEvent(string url, ElapsedEventArgs e)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url, Method.Get);
            var response = await client.GetAsync(request);

            if (response.IsSuccessful)
                Console.WriteLine(response.StatusCode);
            else
                Console.WriteLine($"Error: {response.StatusCode}");

            Console.WriteLine($"The Elapsed event for {url} was raised at {e.SignalTime:HH:mm:ss.fff}");
        }

        public void Dispose()
        {
            aTimer.Dispose();
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
