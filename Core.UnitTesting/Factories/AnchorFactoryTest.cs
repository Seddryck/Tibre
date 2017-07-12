using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;

namespace Tibre.Core.UnitTesting.Factories
{
    [TestClass]
    public class AnchorFactoryTest
    {
        [TestMethod]
        public void Build_EntityWithOneBK_Anchor()
        {
            var factory = new AnchorFactory();
            var anchor = factory.Build("Student", new List<Tuple<string, string>>() { new Tuple<string, string>("StudentNr", "char(10)") });

            Assert.IsInstanceOfType(anchor, typeof(Anchor));
            Assert.IsNotNull(anchor);
            Assert.AreEqual("Student", anchor.Shortname);
            Assert.AreEqual("StudentNr", anchor.BusinessKey[0].Name);
            Assert.IsInstanceOfType(anchor.BusinessKey[0].DataType, typeof(FixedLengthCharacterStringDataType));
            var charDataType = anchor.BusinessKey[0].DataType as FixedLengthCharacterStringDataType;
            Assert.AreEqual(10, charDataType.Length);
            Assert.AreEqual("StudentId", anchor.Identity.Name);
            Assert.IsInstanceOfType(anchor.Identity.DataType, typeof(IntegerDataType));
        }

        [TestMethod]
        public void Build_EntityWithTwoBK_Anchor()
        {
            var factory = new AnchorFactory();
            var anchor = factory.Build("Student", new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("StudentNr", "char(10)")
                        , new Tuple<string, string>("Year", "int")
                    }
            );

            Assert.IsInstanceOfType(anchor, typeof(Anchor));
            Assert.IsNotNull(anchor);
            Assert.AreEqual("Student", anchor.Shortname);
            Assert.AreEqual("StudentNr", anchor.BusinessKey[0].Name);
            Assert.IsInstanceOfType(anchor.BusinessKey[0].DataType, typeof(FixedLengthCharacterStringDataType));
            var charDataType = anchor.BusinessKey[0].DataType as FixedLengthCharacterStringDataType;
            Assert.AreEqual(10, charDataType.Length);
            Assert.AreEqual("Year", anchor.BusinessKey[1].Name);
            Assert.IsInstanceOfType(anchor.BusinessKey[1].DataType, typeof(IntegerDataType));
            Assert.AreEqual("StudentId", anchor.Identity.Name);
            Assert.IsInstanceOfType(anchor.Identity.DataType, typeof(IntegerDataType));
        }
    }
}
