using System;
using Microsoft.Owin.Hosting;

namespace HREmployeeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var consoleCanceler = new ConsoleCanceler();

            var startUpSettings = new StartUpSettings
            {
                Scheme = "http",
                Port = 9000
            };

            var url = new UriBuilder(startUpSettings.Scheme, "*", startUpSettings.Port).ToString();
  
            try
            {
                using (var it = WebApp.Start<StartUp>(url))
                {
                    Console.WriteLine("HR Employee Service has been started with '{0}', press Ctrl+C to exit", url);

                    consoleCanceler.WaitForCancel();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred on startup {0}", ex);
            }
            finally
            {
                Console.WriteLine("Employee Service has exited");
            }
        }
    }
}
