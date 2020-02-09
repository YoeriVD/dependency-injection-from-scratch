using Microsoft.VisualStudio.TestTools.UnitTesting;
using InjectionByExample;
using System;
using System.Collections.Generic;
using System.Text;

namespace InjectionByExample.Tests
{
    [TestClass()]
    public class ContainerBuilderTests
    {
        ContainerBuilder containerBuilder = new ContainerBuilder();

        public void Register_One_GenericType_Parameter()
        {
            containerBuilder.Register<Driver>();
        }

        public void Register_Two_GenericType_Parameter()
        {
            containerBuilder.Register<ICar, Car>(Lifetime.InstancePerContainer);
            containerBuilder.Register<IEngine, Engine>(Lifetime.SingleInstance);
        }

        [TestMethod()]
        public void Build_And_Resolve_All()
        {
            Register_One_GenericType_Parameter();
            Register_Two_GenericType_Parameter();
            var container = containerBuilder.Build();

            var instanceIEngine = container.Resolve<IEngine>();
            Assert.IsInstanceOfType(instanceIEngine, typeof(Engine));

            var childContainer = container.CreateChild();
            var instanceICar = childContainer.Resolve<ICar>();
            Assert.IsInstanceOfType(instanceICar, typeof(Car));

            var instanceDrive = childContainer.Resolve<Driver>();
            Assert.IsInstanceOfType(instanceDrive, typeof(Driver));
        }
    }
}