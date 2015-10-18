using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.CodeAnalysisRule.Anchor;

namespace Tibre.CodeAnalysisRule.UnitTesting.info
{
    [TestClass]
    public class IdentityColumnAnchorTest
    {
        [TestInitialize()]
        public void Startup()
        {
            if (File.Exists("Tibre.CodeAnalysisRule.dll.config"))
                File.Delete("Tibre.CodeAnalysisRule.dll.config");
        }

        [TestMethod]
        public void Analyze_OneColumnOneIdentity_NoProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.Customer (c1 int identity(1,1))", "NoProblems.sql"),
            };

            using(RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(IdentityColumnAnchor.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(0, problems.Count, "Expect 0 problem to have been found");
                });
            }
        }

        [TestMethod]
        public void Analyze_TwoColumnOneIdentity_NoProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.Customer (c1 int identity(1,1), c2 int)", "NoProblems.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(IdentityColumnAnchor.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(0, problems.Count, "Expect 0 problem to have been found");
                });
            }
        }

        [TestMethod]
        public void Analyze_OneColumnNoIdentity_OneProblem()
        {
            const string expectedProblemFile = "OneProblem.sql";
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.Customer (c1 int)", expectedProblemFile),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(IdentityColumnAnchor.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual(expectedProblemFile, problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }

        [TestMethod]
        public void Analyze_OneColumnNoIdentityWithConfig_OneProblem()
        {
            System.IO.File.Copy("Sample.config", "Tibre.CodeAnalysisRule.dll.config");

            const string expectedProblemFile = "OneProblem.sql";
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE TABLE dbo.D_CustomerAnchor (c1 int)", expectedProblemFile),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(IdentityColumnAnchor.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual(expectedProblemFile, problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }
    }
}
