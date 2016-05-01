using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;
using Microsoft.SqlServer.Dac.Model;

namespace Tibre.Core.UnitTesting.Factories
{
    [TestClass]
    public class LinkFactoryTest
    {
        [TestMethod]
        public void Build_Entity_LinkBetweenTwoEntities()
        {
            var factory = new LinkFactory();
            var link = factory.Build("Student", "Course");

            Assert.IsInstanceOfType(link, typeof(Link));
            Assert.IsNotNull(link);
            Assert.AreEqual("StudentCourseLink", link.Shortname);

            Assert.AreEqual("StudentId", link.ForeignKeys[0].Name);
            Assert.AreEqual(SqlDataType.Int, link.ForeignKeys[0].DataType.SqlDataType);

            Assert.AreEqual("CourseId", link.ForeignKeys[1].Name);
            Assert.AreEqual(SqlDataType.Int, link.ForeignKeys[1].DataType.SqlDataType);

            Assert.AreEqual(3, link.ForeignKeys.Count);
            Assert.IsTrue(link.ForeignKeys.Contains(link.DateKey));
        }

        public void Build_Entity_LinkWithCorrectFilters()
        {
            var factory = new LinkFactory();
            var link = factory.Build("Student", "Course");

            Assert.AreEqual("IsFirstDate", link.Filters[0].Name);
            Assert.AreEqual(SqlDataType.Bit, link.Filters[0].DataType.SqlDataType);

            Assert.AreEqual("IsLastDate", link.Filters[1].Name);
            Assert.AreEqual(SqlDataType.Bit, link.Filters[1].DataType.SqlDataType);
        }

    }
}
