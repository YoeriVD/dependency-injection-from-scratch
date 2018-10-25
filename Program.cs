using System;

namespace InjectionByExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating container");
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register<Driver>();
            containerBuilder.Register<ICar, Car>(Lifetime.InstancePerContainer);
            containerBuilder.Register<IEngine, Engine>(Lifetime.SingleInstance);
            var container = containerBuilder.Build();
            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine();
                Console.WriteLine("*******************************");
                Console.WriteLine(container.Resolve<IEngine>());
                Console.WriteLine("------------CHILD--------------");
                var childContainer = container.CreateChild();
                for (var y = 0; y < 2; y++)
                {
                    Console.WriteLine(childContainer.Resolve<Driver>());
                }
            }
        }
    }
}
