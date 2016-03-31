using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.CodeAnalysisRule.Link
{
    [ExportCodeAnalysisRuleAttribute(RuleId
            , "Link table must have a time-based column."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class TimeBasedColumnLink : BaseLink
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.Link.TimeBasedColumnLink";
        public TimeBasedColumnLink()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            //Get the columns of the table
            var columns = table.GetReferenced(Table.Columns);
            if (columns.Count() == 0)
                yield return new SqlRuleProblem(string.Format("The link table {0} has no column", name), table);

            //Ensure that one of them is effecively a DateId
            var timeBasedColumn = columns.FirstOrDefault(i => i.Name.Parts.Last() == Configuration.TimeBased.Key);
            if (timeBasedColumn == null)
                yield return new SqlRuleProblem(string.Format("No column named '{0}' for link table {1}", Configuration.TimeBased.Key, name), table);           
        }
    }
}
