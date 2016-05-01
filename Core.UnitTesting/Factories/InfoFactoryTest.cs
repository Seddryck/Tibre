using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;
using Microsoft.SqlServer.Dac.Model;

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
            Assert.AreEqual(SqlDataType.VarChar, info.Fields[0].DataType.SqlDataType);
            Assert.AreEqual(200, info.Fields[0].DataType.Precision);
            Assert.AreEqual("StudentInfoId", info.Identity.Name);
            Assert.AreEqual(SqlDataType.Int, info.Identity.DataType.SqlDataType);
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
            Assert.AreEqual(SqlDataType.VarChar, info.Fields[0].DataType.SqlDataType);
            Assert.AreEqual(200, info.Fields[0].DataType.Precision);

            Assert.AreEqual("AvgScore", info.Fields[1].Name);
            Assert.AreEqual(SqlDataType.Decimal, info.Fields[1].DataType.SqlDataType);
            Assert.AreEqual(6, info.Fields[1].DataType.Precision);
            Assert.AreEqual(3, info.Fields[1].DataType.Scale);

            Assert.AreEqual("StudentInfoId", info.Identity.Name);
            Assert.AreEqual(SqlDataType.Int, info.Identity.DataType.SqlDataType);
        }
    }
}
