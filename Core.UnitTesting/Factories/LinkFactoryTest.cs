using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.Core.Factories;
using System.Collections.Generic;
using Tibre.Core.Objects;

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
            Assert.IsInstanceOfType(link.ForeignKeys[0].DataType, typeof(IntegerDataType));

            Assert.AreEqual("CourseId", link.ForeignKeys[1].Name);
            Assert.IsInstanceOfType(link.ForeignKeys[1].DataType, typeof(IntegerDataType));

            Assert.AreEqual(3, link.ForeignKeys.Count);
            Assert.IsTrue(link.ForeignKeys.Contains(link.DateKey));
        }

        [TestMethod]
        public void Build_Entity_LinkWithCorrectFilters()
        {
            var factory = new LinkFactory();
            var link = factory.Build("Student", "Course");

            Assert.AreEqual("IsFirstDate", link.Filters[0].Name);
            Assert.IsInstanceOfType(link.Filters[0].DataType, typeof(BooleanDataType));

            Assert.AreEqual("IsLastDate", link.Filters[1].Name);
            Assert.IsInstanceOfType(link.Filters[1].DataType, typeof(BooleanDataType));
        }

        [TestMethod]
        public void Build_EntitiesWithManyToMany_LinkWithCorrectUniqueKeys()
        {
            var factory = new LinkFactory();
            var link = factory.Build("Student", "Course", Connectivity.ManyToMany);

            Assert.AreEqual(1, link.UniqueKeys.Count);
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.ForeignKeys[0]));
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.ForeignKeys[1]));
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.DateKey));
        }

        [TestMethod]
        public void Build_EntitiesWithManyToOne_LinkWithCorrectUniqueKeys()
        {
            var factory = new LinkFactory();
            var link = factory.Build("Student", "Course", Connectivity.ManyToOne);

            Assert.AreEqual(1, link.UniqueKeys.Count);
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.ForeignKeys[1]));
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.DateKey));
        }

        [TestMethod]
        public void Build_EntitiesWithOneToMany_LinkWithCorrectUniqueKeys()
        {
            var factory = new LinkFactory();
            var link = factory.Build("Student", "Course", Connectivity.OneToMany);

            Assert.AreEqual(1, link.UniqueKeys.Count);
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.ForeignKeys[0]));
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.DateKey));
        }

        [TestMethod]
        public void Build_EntitiesWithOneToOne_LinkWithCorrectUniqueKeys()
        {
            var factory = new LinkFactory();
            var link = factory.Build("Student", "Course", Connectivity.OneToOne);

            Assert.AreEqual(2, link.UniqueKeys.Count);
            Assert.AreEqual(2, link.UniqueKeys[0].Count);
            Assert.IsTrue(link.UniqueKeys[0].Contains(link.DateKey));
            Assert.AreEqual(2, link.UniqueKeys[1].Count);
            Assert.IsTrue(link.UniqueKeys[1].Contains(link.DateKey));
        }

    }
}
