using System;
using System.Threading;

namespace HREmployeeService
{
    public class ConsoleCanceler
    {
        private static ManualResetEvent _exitEvent;

        public void WaitForCancel()
        {
            _exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                _exitEvent.Set();
            };

            _exitEvent.WaitOne();
        }
    }
}