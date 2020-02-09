using Microsoft.VisualStudio.TestTools.UnitTesting;
using InjectionByExample;
using System;
using System.Collections.Generic;
using System.Text;

namespace InjectionByExample.Tests
{
    [TestClass()]
    public class ContainerTests
    {
        Container container;

        public ContainerTests()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register<Driver>();
            containerBuilder.Register<ICar, Car>(Lifetime.InstancePerContainer);
            containerBuilder.Register<IEngine, Engine>(Lifetime.SingleInstance);

            container = containerBuilder.Build();
        }

        [TestMethod()]
        public void ContainerWithParentNull()
        {
            Assert.IsNull(container.Parent);
        }

        [TestMethod()]
        public void ContainerWithParentNotNull()
        {
            var childContainer = container.CreateChild();

            Assert.IsNotNull(childContainer.Parent);
        }

        [TestMethod()]
        public void ResolveWithParameterType()
        {
            var instance = container.Resolve(typeof(IEngine));

            Assert.IsInstanceOfType(instance, typeof(Engine));
        }

        [TestMethod()]
        public void ResolveWithGenericType()
        {
            var instance = container.Resolve<IEngine>();
            Assert.IsInstanceOfType(instance, typeof(Engine));
        }

        [TestMethod()]
        public void ResolveInstancePerContainer()
        {
            var childContainer = container.CreateChild();

            var instance = childContainer.Resolve<ICar>();

            Assert.IsInstanceOfType(instance, typeof(ICar));
        }

        [TestMethod()]
        public void CreateChildTest()
        {
            var childContainer = container.CreateChild();

            Assert.IsInstanceOfType(childContainer, typeof(Container));
        }

        [TestMethod()]
        public void HasInstanceTest()
        {
            var childContainer = container.CreateChild();
            var anotherChildContainer = childContainer.CreateChild();

            childContainer.Resolve(typeof(ICar));
            anotherChildContainer.Resolve(typeof(ICar));

            var hasInstance = anotherChildContainer.Parent.HasInstance(typeof(ICar));
            Assert.IsTrue(hasInstance);
        }
    }
}