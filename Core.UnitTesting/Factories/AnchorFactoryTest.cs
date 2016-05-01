using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;
using Microsoft.SqlServer.Dac.Model;

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
            Assert.AreEqual(SqlDataType.Char, anchor.BusinessKey[0].DataType.SqlDataType);
            Assert.AreEqual(10, anchor.BusinessKey[0].DataType.Precision);
            Assert.AreEqual("StudentId", anchor.Identity.Name);
            Assert.AreEqual(SqlDataType.Int, anchor.Identity.DataType.SqlDataType);
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
            Assert.AreEqual(SqlDataType.Char, anchor.BusinessKey[0].DataType.SqlDataType);
            Assert.AreEqual(10, anchor.BusinessKey[0].DataType.Precision);
            Assert.AreEqual("Year", anchor.BusinessKey[1].Name);
            Assert.AreEqual(SqlDataType.Int, anchor.BusinessKey[1].DataType.SqlDataType);
            Assert.AreEqual("StudentId", anchor.Identity.Name);
            Assert.AreEqual(SqlDataType.Int, anchor.Identity.DataType.SqlDataType);
        }
    }
}
