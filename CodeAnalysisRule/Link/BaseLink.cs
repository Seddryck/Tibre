using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Tibre.CodeAnalysisRule.Link
{
    public abstract class BaseLink : BaseRule
    {
        public BaseLink()
        {
            SupportedElementTypes = new[] { Table.TypeClass };
        }

        protected bool IsLink(TSqlObject table)
        {
            return
                table.Name.HasName
                && table.Name.Parts.Reverse().Take(2).Last().Equals(Configuration.Link.Schema, StringComparison.OrdinalIgnoreCase)
                && table.Name.Parts.Last().StartsWith(Configuration.Link.Prefix, StringComparison.OrdinalIgnoreCase)
                && table.Name.Parts.Last().EndsWith(Configuration.Link.Suffix, StringComparison.OrdinalIgnoreCase);
        }

        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            var tableElement = ruleExecutionContext.ModelElement;
            var tableName = GetElementName(ruleExecutionContext, tableElement);

            //Ensure that it's effectively an info table
            if (IsLink(tableElement))
                return OnAnalyze(tableName, tableElement).ToList();

            return new List<SqlRuleProblem>();
        }

        protected abstract IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table);
        
    }
}
