using Serilog;
using Serilog.Events;

namespace RabbitWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging((_, logging) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .CreateLogger();

                    logging.ClearProviders();
                    logging.AddSerilog();
                })
                .ConfigureServices(services => { services.AddHostedService<Worker>(); })

                .Build();

            host.Run();
        }
    }
}