using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;

namespace Tibre.Core.UnitTesting.Factories
{
    [TestClass]
    public class InfoFactoryTest
    {
        [TestMethod]
        public void Build_EntityWithOneField_Info()
        {
            var factory = new InfoFactory();
            var info = factory.Build("Student", new List<Tuple<string, string>>() { new Tuple<string, string>("LastName", "varchar(200)") });

            Assert.IsInstanceOfType(info, typeof(Info));
            Assert.IsNotNull(info);
            Assert.AreEqual("StudentInfo", info.Shortname);
            Assert.AreEqual("LastName", info.Fields[0].Name);
            Assert.IsInstanceOfType(info.Fields[0].DataType, typeof(VaryingLengthCharacterStringDataType));
            var dataType = info.Fields[0].DataType as VaryingLengthCharacterStringDataType;
            Assert.AreEqual(200, dataType.Length);
            Assert.AreEqual("StudentInfoId", info.Identity.Name);
            Assert.IsInstanceOfType(info.Identity.DataType, typeof(IntegerDataType));
        }

        [TestMethod]
        public void Build_EntityWithTwoFields_Anchor()
        {
            var factory = new InfoFactory();
            var info = factory.Build("Student", new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("LastName", "varchar(200)")
                        , new Tuple<string, string>("AvgScore", "decimal(6,3)")
                    }
            );

            Assert.IsInstanceOfType(info, typeof(Info));
            Assert.IsNotNull(info);
            Assert.AreEqual("StudentInfo", info.Shortname);

            Assert.AreEqual("LastName", info.Fields[0].Name);
            Assert.IsInstanceOfType(info.Fields[0].DataType, typeof(VaryingLengthCharacterStringDataType));
            var varcharDataType = info.Fields[0].DataType as VaryingLengthCharacterStringDataType;
            Assert.AreEqual(200, varcharDataType.Length);

            Assert.AreEqual("AvgScore", info.Fields[1].Name);
            Assert.IsInstanceOfType(info.Fields[1].DataType, typeof(DecimalDataType));
            var decimalDataType = info.Fields[1].DataType as DecimalDataType;
            Assert.AreEqual(6, decimalDataType.Precision);
            Assert.AreEqual(3, decimalDataType.Scale);

            Assert.AreEqual("StudentInfoId", info.Identity.Name);
            Assert.IsInstanceOfType(info.Identity.DataType, typeof(IntegerDataType));
        }

        [TestMethod]
        public void Build_EntityWithTwoNonGenericFields_Anchor()
        {
            var factory = new InfoFactory();
            var info = factory.Build("Student", new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("Biography", "text")
                        , new Tuple<string, string>("Picture", "binary(5000)")
                    }
            );

            Assert.IsInstanceOfType(info, typeof(Info));
            Assert.IsNotNull(info);
            Assert.AreEqual("StudentInfo", info.Shortname);

            Assert.AreEqual("Biography", info.Fields[0].Name);
            Assert.IsInstanceOfType(info.Fields[0].DataType, typeof(NonGenericDataType));
            var nonGenericDataType1 = info.Fields[0].DataType as NonGenericDataType;
            Assert.AreEqual("text", nonGenericDataType1.Name);
            Assert.AreEqual(0, nonGenericDataType1.Arguments.Count);

            Assert.AreEqual("Picture", info.Fields[1].Name);
            Assert.IsInstanceOfType(info.Fields[1].DataType, typeof(NonGenericDataType));
            var nonGenericDataType2 = info.Fields[1].DataType as NonGenericDataType;
            Assert.AreEqual("binary", nonGenericDataType2.Name);
            Assert.AreEqual(1, nonGenericDataType2.Arguments.Count);
            Assert.AreEqual(5000, nonGenericDataType2.Arguments[0]);
        }
    }
}
