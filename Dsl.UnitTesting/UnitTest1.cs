using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Tibre.Dsl;
using Sprache;

namespace Dsl.UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Concept_OneWord_Label()
        {
            var input = "Brp";
            var concept = Parsing.Concept.Parse(input);

            Assert.AreEqual(input, concept.Label);

        }

        [TestMethod]
        public void Concept_SeveralWord_Label()
        {
            var input = "Balance Responsible Party";
            var concept = Parsing.Concept.Parse(input);

            Assert.AreEqual(input, concept.Label);

        }

        [TestMethod]
        public void Concept_SeveralWordLineEnd_Label()
        {
            var input = "Balance Responsible Party\r\n******";
            var concept = Parsing.Concept.Parse(input);

            Assert.AreEqual(input.Split('\r')[0], concept.Label);
        }

        [TestMethod]
        public void Concept_Anchor_Label()  
        {
            var input = "Eic\tvarchar(50)\t?";
            var concept = Parsing.Field.Parse(input);

            Assert.AreEqual("Eic", concept.ElementAt(0));
            Assert.AreEqual("varchar(50)", concept.ElementAt(1));
            Assert.AreEqual("?", concept.ElementAt(2));
        }
    }
}
