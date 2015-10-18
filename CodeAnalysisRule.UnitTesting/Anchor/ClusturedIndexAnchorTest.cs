using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.CodeAnalysisRule.Anchor;

namespace Tibre.CodeAnalysisRule.UnitTesting.Anchor
{
    [TestClass]
    public class ClusturedIndexAnchorTest
    {
        [TestInitialize()]
        public void Startup()
        {
            if (File.Exists("Tibre.CodeAnalysisRule.dll.config"))
                File.Delete("Tibre.CodeAnalysisRule.dll.config");
        }

        [TestMethod]
        public void Analyze_OneClusturedOneColumnIdentity_NoProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.Customer (c1 int identity(1,1))\r\nGO\r\nCREATE UNIQUE CLUSTERED INDEX ci ON dwh.Customer(c1)", "NoProblem.sql"),
            };

            using(RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(ClusturedIndexAnchor.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(0, problems.Count, "Expect 0 problem to have been found");
                });
            }
        }

        [TestMethod]
        public void Analyze_OneClusturedNoIdentity_OneProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.Customer (c1 int)\r\nGO\r\nCREATE UNIQUE CLUSTERED INDEX ci ON dwh.Customer(c1)", "WithProblem.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(ClusturedIndexAnchor.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual("WithProblem.sql", problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }

        [TestMethod]
        public void Analyze_OneClusturedNotUnique_OneProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.Customer (c1 int identity(1,1))\r\nGO\r\nCREATE CLUSTERED INDEX ci ON dwh.Customer(c1)", "WithProblem.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(ClusturedIndexAnchor.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual("WithProblem.sql", problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }

    }
}
