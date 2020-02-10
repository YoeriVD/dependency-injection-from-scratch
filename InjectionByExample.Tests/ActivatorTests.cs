using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InjectionByExample.Tests
{
    [TestClass]
    public class ActivatorTests
    {
        private readonly IList<Registration> registrations = new List<Registration>
        {
            new Registration(typeof(Driver), typeof(Driver)),
            new Registration(typeof(ICar), typeof(Car), Lifetime.InstancePerContainer),
            new Registration(typeof(IEngine), typeof(Engine), Lifetime.SingleInstance)
        };

        [TestMethod]
        public void Check_Lifetime_With_RegisteredType()
        {
            foreach (var reg in registrations)
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