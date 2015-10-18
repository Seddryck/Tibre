using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Tibre.CodeAnalysisRule.Anchor
{
    public abstract class BaseAnchor : BaseRule
    {
        protected bool IsAnchor(TSqlObject table)
        {
            return
                table.Name.HasName
                && table.Name.Parts.Reverse().Take(2).Last().Equals(Configuration.Anchor.Schema, StringComparison.OrdinalIgnoreCase)
                && table.Name.Parts.Last().StartsWith(Configuration.Anchor.Prefix, StringComparison.OrdinalIgnoreCase)
                && table.Name.Parts.Last().EndsWith(Configuration.Anchor.Sufix, StringComparison.OrdinalIgnoreCase);
        }

        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            var tableElement = ruleExecutionContext.ModelElement;
            var tableName = GetElementName(ruleExecutionContext, tableElement);

            //Ensure that it's effectively an info table
            if (IsAnchor(tableElement))
                return OnAnalyze(tableName, tableElement).ToList();

            return new List<SqlRuleProblem>();
        }

        protected abstract IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table);
        
    }
}
