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
            , "Link table must have an index with time-based column as first column."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class IsFirstIndexLink : IsLastIndexLink
    {
        public new const string RuleId = "Tibre.CodeAnalysisRule.Link.IsFirstIndexLink";
        public IsFirstIndexLink()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            return OnAnalyze(name, table, Configuration.Link.IsFirst);
        }
    }
}
