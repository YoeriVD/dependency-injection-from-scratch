using Microsoft.VisualStudio.TestTools.UnitTesting;
using InjectionByExample;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InjectionByExample.Tests
{
    [TestClass()]
    public class ActivatorTests
    {
        IList<Registration> registrations = new List<Registration>()
        {
            new Registration(typeof(Driver), typeof(Driver), Lifetime.NewInstance),
            new Registration(typeof(ICar), typeof(Car), Lifetime.InstancePerContainer),
            new Registration(typeof(IEngine), typeof(Engine), Lifetime.SingleInstance)
        };

        Container container;

        public ActivatorTests()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register<Driver>();
            containerBuilder.Register<ICar, Car>(Lifetime.InstancePerContainer);
            containerBuilder.Register<IEngine, Engine>(Lifetime.SingleInstance);

            container = containerBuilder.Build();
        }

        [TestMethod()]
        public void CreateInstanceTest()
        {
            foreach (var reg in registrations)
            {
                switch (reg.Lifetime)
                {
                    case Lifetime.NewInstance:
                        Assert.AreSame(reg.RegisteredType, typeof(Driver));
                        break;
                    case Lifetime.InstancePerContainer:
                        Assert.AreSame(reg.RegisteredType, typeof(ICar));
                        break;
                    case Lifetime.SingleInstance:
                        Assert.AreSame(reg.RegisteredType, typeof(IEngine));
                        break;
                }
            }
        }
    }
}