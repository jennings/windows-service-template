using System;

namespace Service.ServiceHost
{
    public interface INotifier
    {
        void PrintMessage();
    }

    public class Notifier : INotifier
    {
        public void PrintMessage()
        {
            Console.WriteLine("It worked!");
        }
    }
}
