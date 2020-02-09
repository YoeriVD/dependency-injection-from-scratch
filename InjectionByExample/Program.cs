using System;

namespace InjectionByExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating container");
            /**
            * I'm using the factory pattern to create a container.
            * This way I can optimize the way injections work. (eg only analyze constructors once)
            */
            var containerBuilder = new ContainerBuilder();
            /**
            * Next up, I will instruct the builder which types I want it to be able to create.
            * Eg. 'if a request to resolve type X comes in, create an instance of this type'
            * As an extra, we can also instruct to container to return
            * the same instance as a previous resolve, or create a new one every time.
            */
            containerBuilder.Register<Driver>();
            containerBuilder.Register<ICar, Car>(Lifetime.InstancePerContainer);
            containerBuilder.Register<IEngine, Engine>(Lifetime.SingleInstance);
            /**
            * Start the analysis of the dependencies
            */
            var container = containerBuilder.Build();

            /*
            * TEST! :-)
             */
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
