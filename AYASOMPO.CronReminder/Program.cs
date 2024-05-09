using AYASOMPO.CronReminder.Services;

string? url;
string? minute;

Console.WriteLine("Enter url to be awaken");
url = Console.ReadLine();

Console.WriteLine("Enter minute to awake url");
minute = Console.ReadLine();

if (string.IsNullOrWhiteSpace(url)) throw new Exception("Url must be specified.");
if (string.IsNullOrWhiteSpace(minute)) throw new Exception("Minutes must be specified.");

Console.WriteLine("Timer Start...\n");
Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);

TimerService _service = new TimerService();
_service.SetTimer(url, Convert.ToInt32(minute));

Console.WriteLine("\nPress the Enter key to exit the application...\n");
Console.ReadLine();