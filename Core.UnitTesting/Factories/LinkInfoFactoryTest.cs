using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;

namespace Tibre.Core.UnitTesting.Factories
{
    [TestClass]
    public class LinkInfoFactoryTest
    {
        [TestMethod]
        public void Build_Entity_LinkInfoWithCorrectKeys()
        {
            var factory = new LinkInfoFactory();
            var linkInfo = factory.Build("Student");

            Assert.IsInstanceOfType(linkInfo, typeof(LinkInfo));
            Assert.IsNotNull(linkInfo);
            Assert.AreEqual("StudentLink", linkInfo.Shortname);

            Assert.AreEqual("StudentId", linkInfo.AnchorKey.Name);
            Assert.IsInstanceOfType(linkInfo.AnchorKey.DataType, typeof(IntegerDataType));

            Assert.AreEqual("StudentInfoId", linkInfo.InfoKey.Name);
            Assert.IsInstanceOfType(linkInfo.InfoKey.DataType, typeof(IntegerDataType));

            Assert.AreEqual("DateId", linkInfo.DateKey.Name);
            Assert.IsInstanceOfType(linkInfo.DateKey.DataType, typeof(IntegerDataType));

            Assert.AreEqual(3, linkInfo.ForeignKeys.Count);
            Assert.IsTrue(linkInfo.ForeignKeys.Contains(linkInfo.AnchorKey));
            Assert.IsTrue(linkInfo.ForeignKeys.Contains(linkInfo.InfoKey));
            Assert.IsTrue(linkInfo.ForeignKeys.Contains(linkInfo.DateKey));
        }

        [TestMethod]
        public void Build_Entity_LinkInfoWithCorrectFilters()
        {
            var factory = new LinkInfoFactory();
            var linkInfo = factory.Build("Student");

            Assert.AreEqual("IsFirstDate", linkInfo.Filters[0].Name);
            Assert.IsInstanceOfType(linkInfo.Filters[0].DataType, typeof(BooleanDataType));

            Assert.AreEqual("IsLastDate", linkInfo.Filters[1].Name);
            Assert.IsInstanceOfType(linkInfo.Filters[1].DataType, typeof(BooleanDataType));
        }

        [TestMethod]
        public void Build_Entity_LinkInfoWithCorrectUniqueKey()
        {
            var factory = new LinkInfoFactory();
            var linkInfo = factory.Build("Student");

            Assert.AreEqual(2, linkInfo.UniqueKey.Count);
            Assert.IsTrue(linkInfo.UniqueKey.Contains(linkInfo.AnchorKey));
            Assert.IsTrue(linkInfo.UniqueKey.Contains(linkInfo.DateKey));
        }

    }
}
