using AYASOMPO.CronReminder.Services;

List<string>? urls = new List<string>();
string? minute;

Console.WriteLine("Enter the number of URLs to be awakened:");
if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
{
    throw new ArgumentException("Invalid number of URLs specified.");
}

for (int i = 0; i < count; i++)
{
    Console.WriteLine($"Enter URL {i + 1}:");
    string url = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(url)) throw new Exception("Url must be specified.");

    urls.Add(url); 
}

Console.WriteLine("Enter minute to awake url");
minute = Console.ReadLine();

if (string.IsNullOrWhiteSpace(minute)) throw new Exception("Minutes must be specified.");

Console.WriteLine("Timer Start...\n");
Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);

TimerService _service = new TimerService();
_service.SetTimer(urls, Convert.ToInt32(minute));

Console.WriteLine("\nPress the Enter key to exit the application...\n");
Console.ReadLine();

_service.Dispose();