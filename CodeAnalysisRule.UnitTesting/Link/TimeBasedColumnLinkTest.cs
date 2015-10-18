using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.CodeAnalysisRule.Link;

namespace Tibre.CodeAnalysisRule.UnitTesting.Link
{
    [TestClass]
    public class TimeBasedColumnLinkTest
    {
        [TestInitialize()]
        public void Startup()
        {
            if (File.Exists("Tibre.CodeAnalysisRule.dll.config"))
                File.Delete("Tibre.CodeAnalysisRule.dll.config");
        }

        [TestMethod]
        public void Analyze_OneColumnTimeBased_NoProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.CustomerLink (c1 int, c2 int, DateId int)\r\nGO\r\nCREATE INDEX cx ON dwh.CustomerLink(DateId)", "NoProblems.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(TimeBasedColumnLink.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(0, problems.Count, "Expect 0 problem to have been found");
                });
            }
        }

        [TestMethod]
        public void Analyze_OneColumnTimeBasedIndexTwoColumns_NoProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.CustomerLink (c1 int, c2 int, DateId int)\r\nGO\r\nCREATE INDEX cx ON dwh.CustomerLink(DateId, c1)", "NoProblems.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(TimeBasedColumnLink.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(0, problems.Count, "Expect 0 problem to have been found");
                });
            }
        }

        [TestMethod]
        public void Analyze_ColumnTimeBasedMissing_OneProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.CustomerLink (c1 int, c2 int)\r\nGO\r\nCREATE INDEX cx ON dwh.CustomerLink(c1)", "OneProblems.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(TimeBasedColumnLink.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual("OneProblems.sql", problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }

        [TestMethod]
        public void Analyze_ColumnTimeBasedMissingFromIndex_OneProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.CustomerLink (c1 int, c2 int, DateId int)\r\nGO\r\nCREATE INDEX cx ON dwh.CustomerLink(c1)", "OneProblems.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(TimeBasedColumnLink.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual("OneProblems.sql", problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }

        [TestMethod]
        public void Analyze_ColumnTimeBasedMissingIndex_OneProblem()
        {
            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE SCHEMA [dwh]", "schema"),
                Tuple.Create("CREATE TABLE dwh.CustomerLink (c1 int, c2 int, DateId int)", "OneProblems.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(TimeBasedColumnLink.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual("OneProblems.sql", problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }

        [TestMethod]
        public void Analyze_ColumnTimeBasedMissingIndexWithConfig_OneProblem()
        {
            System.IO.File.Copy("Sample.config", "Tibre.CodeAnalysisRule.dll.config");

            var testScripts = new Tuple<string, string>[]
            {
                Tuple.Create("CREATE TABLE dbo.FF_CustomerLinks (c1 int, c2 int, DateId int)", "OneProblems.sql"),
            };

            using (RuleTest test = new RuleTest(testScripts, new TSqlModelOptions(), SqlServerVersion.Sql120))
            {
                test.RunTest(TimeBasedColumnLink.RuleId, (result, problemsString) =>
                {
                    var problems = result.Problems;
                    Assert.AreEqual(1, problems.Count, "Expect 1 problem to have been found");
                    Assert.AreEqual("OneProblems.sql", problems[0].SourceName, "Expect the source name to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartLine, "Expect the line to match where the problem was found");
                    Assert.AreEqual(1, problems[0].StartColumn, "Expect the column to match where the problem was found");
                });
            }
        }
    }
}
