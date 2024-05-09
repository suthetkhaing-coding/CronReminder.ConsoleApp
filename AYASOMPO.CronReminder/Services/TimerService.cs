using RestSharp;
using System.Timers;

namespace AYASOMPO.CronReminder.Services
{
    public class TimerService
    {
        private System.Timers.Timer aTimer;
        private string? _url;

        public void SetTimer(string url, int time)
        {
            _url = url;

            time = (int)TimeSpan.FromMinutes(time).TotalMilliseconds;
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(time);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:7169");
            //    var response = await client.GetAsync("");
            //    Console.WriteLine("\nCall Success...\n");
            //}
            //string endpoint = "https://localhost:7169";
            //string endpoint = "http://localhost:8024";

            string endpoint = _url;
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(endpoint, Method.Get);
            var response = await client.GetAsync(request);

            if (response.IsSuccessful)
                Console.WriteLine(response.StatusCode);
            else
                Console.WriteLine($"Error: {response.StatusCode}");

            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",                              e.SignalTime);
        }
    }
}
