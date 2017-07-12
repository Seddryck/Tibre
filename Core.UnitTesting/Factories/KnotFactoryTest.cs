using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;

namespace Tibre.Core.UnitTesting.Factories
{
    [TestClass]
    public class KnotFactoryTest
    {
        [TestMethod]
        public void Build_DomainWithThreeValues_Knot()
        {
            var factory = new KnotFactory();
            var values = new string[] { "Monday", "Tuesday", "Wednesday" };
            var knot = factory.Build("Weekday", values);

            Assert.IsInstanceOfType(knot, typeof(Knot));
            Assert.IsNotNull(knot);
            Assert.AreEqual("Weekday", knot.Shortname);

            Assert.AreEqual("WeekdayId", knot.Identity.Name);
            Assert.IsTrue(knot.Identity.IsIdentity);
            Assert.IsInstanceOfType(knot.Identity.DataType, typeof(IntegerDataType));

            Assert.AreEqual("Label", knot.Label.Name);
            Assert.IsFalse(knot.Label.IsIdentity);
            Assert.IsInstanceOfType(knot.Label.DataType, typeof(VaryingLengthCharacterStringDataType));

            var charDataType = knot.Label.DataType as VaryingLengthCharacterStringDataType;
            Assert.AreEqual(9, charDataType.Length);
        }
        
    }
}
