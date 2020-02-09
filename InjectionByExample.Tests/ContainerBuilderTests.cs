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

        [TestMethod()]
        public void RegisterOneGenericType()
        {
            containerBuilder.Register<Driver>();

            Type t = containerBuilder.GetType();
            Assert.IsInstanceOfType(containerBuilder, t);
        }

        [TestMethod()]
        public void RegisterTwoGenericType()
        {
            containerBuilder.Register<Driver, Driver>();

            Type t = containerBuilder.GetType();
            Assert.IsInstanceOfType(containerBuilder, t);
        }

        [TestMethod()]
        public void BuildTest()
        {
            containerBuilder.Register<Driver>();

            var container = containerBuilder.Build();

            Type t = container.GetType();
            Assert.IsInstanceOfType(container, t);
        }
    }
}