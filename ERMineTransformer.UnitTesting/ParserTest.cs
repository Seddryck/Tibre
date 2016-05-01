using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tibre.ERMineTransformer;
using Tibre.Core.Objects;

namespace Tibre.Core.UnitTesting
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parse_Entity_Anchor()
        {
            var input = "[Student]";
            var parser = new Parser();
            var model = parser.Parse(input);
            var anchors = model.Tables.Where(t => t is Anchor);

            Assert.AreEqual(1, anchors.Count());
            Assert.AreEqual("Student", anchors.ElementAt(0).Shortname);
        }

        [TestMethod]
        public void Parse_Entity_BusinessKey()
        {
            var input = "[Student]\r\n* StudentNr char(10)";
            var parser = new Parser();
            var model = parser.Parse(input);
            var anchor = model.Tables.ElementAt(0) as Anchor;

            Assert.IsNotNull(anchor);
            Assert.AreEqual(1, anchor.BusinessKey.Count());
            Assert.AreEqual("StudentNr", anchor.BusinessKey.ElementAt(0).Name);
            Assert.AreEqual(Microsoft.SqlServer.Dac.Model.SqlDataType.Char, anchor.BusinessKey.ElementAt(0).DataType.SqlDataType);
        }

        [TestMethod]
        public void Parse_EntityCompositeBK_BusinessKey()
        {
            var input = "[Student]\r\n* StudentNr char(10)\r\n* Year char(2)\r\nName varchar(50)";
            var parser = new Parser();
            var model = parser.Parse(input);
            var anchor = model.Tables.ElementAt(0) as Anchor;

            Assert.IsNotNull(anchor);
            Assert.AreEqual(2, anchor.BusinessKey.Count());
        }

        [TestMethod]
        public void Parse_Entity_Info()
        {
            var input = "[Student]\r\n* StudentNr char(10)";
            var parser = new Parser();
            var model = parser.Parse(input);
            var infos = model.Tables.Where(t => t is Info);

            Assert.AreEqual(1, infos.Count());
            Assert.AreEqual("StudentInfo", infos.ElementAt(0).Shortname);
        }

        [TestMethod]
        public void Parse_Entity_InfoAttributes()
        {
            var input = "[Student]\r\n* StudentNr char(10)\r\n* Year char(2)\r\nName varchar(50)";
            var parser = new Parser();
            var model = parser.Parse(input);
            var info = model.Tables.Where(t => t is Info).ElementAt(0) as Info;

            Assert.IsNotNull(info);
            Assert.AreEqual(1, info.Fields.Count());
            Assert.AreEqual("Name", info.Fields.ElementAt(0).Name);
        }

        [TestMethod]
        public void Parse_Entity_LinkInfo()
        {
            var input = "[Student]\r\n* StudentNr char(10)";
            var parser = new Parser();
            var model = parser.Parse(input);
            var linkInfos = model.Tables.Where(t => t is LinkInfo);

            Assert.AreEqual(1, linkInfos.Count());
            Assert.AreEqual("StudentLink", linkInfos.ElementAt(0).Shortname);
        }

        [TestMethod]
        public void Parse_Entities_Link()
        {
            var input = "[Student]\r\n* StudentNr char(10)\r\n[Course] * CourseKey char(5)\r\n [Student] *-follow-* [Course]\r\n";
            var parser = new Parser();
            var model = parser.Parse(input);
            var links = model.Tables.Where(t => t.GetType()==typeof(Link));

            Assert.AreEqual(1, links.Count());
            Assert.AreEqual("StudentCourseLink", links.ElementAt(0).Shortname);
        }
    }
}
