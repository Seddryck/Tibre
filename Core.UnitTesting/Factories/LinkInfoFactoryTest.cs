using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;
using Microsoft.SqlServer.Dac.Model;

namespace Tibre.Core.UnitTesting.Factories
{
    [TestClass]
    public class LinkInfoFactoryTest
    {
        [TestMethod]
        public void Build_Entity_InfoWithCorrectKeys()
        {
            var factory = new LinkInfoFactory();
            var linkInfo = factory.Build("Student");

            Assert.IsInstanceOfType(linkInfo, typeof(LinkInfo));
            Assert.IsNotNull(linkInfo);
            Assert.AreEqual("StudentLink", linkInfo.Shortname);

            Assert.AreEqual("StudentId", linkInfo.AnchorKey.Name);
            Assert.AreEqual(SqlDataType.Int, linkInfo.AnchorKey.DataType.SqlDataType);

            Assert.AreEqual("StudentInfoId", linkInfo.InfoKey.Name);
            Assert.AreEqual(SqlDataType.Int, linkInfo.InfoKey.DataType.SqlDataType);

            Assert.AreEqual("DateId", linkInfo.DateKey.Name);
            Assert.AreEqual(SqlDataType.Int, linkInfo.DateKey.DataType.SqlDataType);

            Assert.AreEqual(3, linkInfo.ForeignKeys.Count);
            Assert.IsTrue(linkInfo.ForeignKeys.Contains(linkInfo.AnchorKey));
            Assert.IsTrue(linkInfo.ForeignKeys.Contains(linkInfo.InfoKey));
            Assert.IsTrue(linkInfo.ForeignKeys.Contains(linkInfo.DateKey));
        }

        public void Build_Entity_InfoWithCorrectFilters()
        {
            var factory = new LinkInfoFactory();
            var linkInfo = factory.Build("Student");

            Assert.AreEqual("IsFirstDate", linkInfo.Filters[0].Name);
            Assert.AreEqual(SqlDataType.Bit, linkInfo.Filters[0].DataType.SqlDataType);

            Assert.AreEqual("IsLastDate", linkInfo.Filters[1].Name);
            Assert.AreEqual(SqlDataType.Bit, linkInfo.Filters[1].DataType.SqlDataType);
        }

        public void Build_Entity_InfoWithCorrectUniqueKey()
        {
            var factory = new LinkInfoFactory();
            var linkInfo = factory.Build("Student");

            Assert.AreEqual(2, linkInfo.UniqueKeys.Count);
            Assert.IsTrue(linkInfo.UniqueKeys.Contains(linkInfo.AnchorKey));
            Assert.IsTrue(linkInfo.UniqueKeys.Contains(linkInfo.DateKey));
        }

    }
}
