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
        public void Container_With_Null_Parent()
        {
            Assert.IsNull(container.Parent);
        }

        [TestMethod()]
        public void Container_With_Not_Null_Parent()
        {
            var childContainer = container.CreateChild();

            Assert.IsNotNull(childContainer.Parent);
        }

        [TestMethod()]
        public void Resolve_With_A_Parameter_Type()
        {
            var instance = container.Resolve(typeof(IEngine));

            Assert.IsInstanceOfType(instance, typeof(Engine));
        }

        [TestMethod()]
        public void Resolve_With_A_Generic_Type()
        {
            var instance = container.Resolve<IEngine>();
            Assert.IsInstanceOfType(instance, typeof(Engine));
        }

        [TestMethod()]
        public void Resolve_Instance_Per_Container()
        {
            var childContainer = container.CreateChild();
            var anotherChildContainer = childContainer.CreateChild();

            childContainer.Resolve<ICar>();
            var instance1 = childContainer.Parent.HasInstance(typeof(ICar));
            Assert.IsFalse(instance1);

            anotherChildContainer.Resolve<ICar>();
            var instance2 = anotherChildContainer.Parent.HasInstance(typeof(ICar));
            Assert.IsTrue(instance2);
        }

        [TestMethod()]
        public void CreateChild_Test()
        {
            var childContainer = container.CreateChild();

            Assert.IsInstanceOfType(childContainer, typeof(Container));
        }

        [TestMethod()]
        public void ChildContainer_Should_Share_Parent_Instance_For_InstancePerContainer()
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