using Microsoft.VisualStudio.TestTools.UnitTesting;
using InjectionByExample;
using System;
using System.Collections.Generic;
using System.Text;

namespace InjectionByExample.Tests
{
    [TestClass()]
    public class RegistrationTests
    {
        IDictionary<Type, Registration> registrations = new Dictionary<Type, Registration>()
        {
            {
                typeof(Driver),
                new Registration(typeof(Driver), typeof(Driver), Lifetime.NewInstance)
            },
            {
                typeof(ICar),
                new Registration(typeof(ICar), typeof(Car), Lifetime.InstancePerContainer)
            },
                        {
                typeof(IEngine),
                new Registration(typeof(IEngine), typeof(Engine), Lifetime.SingleInstance)
            }
        };

        [TestMethod()]
        public void RegistrationTest()
        {
            foreach (var reg in registrations)
            {
                Assert.IsInstanceOfType(reg.Value, typeof(Registration));
            }
        }
    }
}