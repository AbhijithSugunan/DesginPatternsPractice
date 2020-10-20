using System;
using System.Collections.Generic;
using System.Threading;

namespace ObserverPattern
{
    interface IPublisher 
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    interface IObserver
    {
        void Update(IPublisher publisher);
    }

    class Publisher : IPublisher
    {
        private List<IObserver> _oberverRegistry;
        public int State { get; set; } = -0;

        public Publisher()
        {
            _oberverRegistry = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            Console.WriteLine("Publisher: A new Subscriber is added.");
            _oberverRegistry.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Console.WriteLine("Publisher: A Subscriber is removed.");
            _oberverRegistry.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _oberverRegistry)
            {
                observer.Update(this);
            }
        }

        public void DoSomeBusiness()
        {
            State = new Random().Next(0, 10);
            Thread.Sleep(100);
            Console.WriteLine($"Publisher state changed to {State}");
            Notify();
        }

    }
        class ConcreateSubscriberOne : IObserver
        {
            public void Update(IPublisher publisher)
            {
                if ((publisher as Publisher).State < 2)
                {
                    Console.WriteLine("ConcreateSubscriberOne is responded to the notification");
                }
            }
        }

        class ConcreateSubscriberTwo : IObserver
        {
            public void Update(IPublisher publisher)
            {
                if ((publisher as Publisher).State >= 2)
                {
                    Console.WriteLine("ConcreateSubscriberTwo is responded to the notification");
                }
            }
        }

    class Program
    {
        static void Main(string[] args)
        {
            var publisher = new Publisher();

            var subscriberA = new ConcreateSubscriberOne();
            publisher.Attach(subscriberA);
            publisher.DoSomeBusiness();

            var subscriberB = new ConcreateSubscriberTwo();
            publisher.Attach(subscriberB);
            publisher.DoSomeBusiness();

            publisher.Detach(subscriberB);

            publisher.DoSomeBusiness();
        }
    }
}
