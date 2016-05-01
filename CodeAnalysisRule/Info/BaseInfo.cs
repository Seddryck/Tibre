using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Tibre.CodeAnalysisRule.Info
{
    public abstract class BaseInfo : BaseRule
    {

        public BaseInfo()
        {
            SupportedElementTypes = new[] { Table.TypeClass };
        }
    
        protected bool IsInfo(TSqlObject table)
        {
            return
                table.Name.HasName
                && table.Name.Parts.Reverse().Take(2).Last().Equals(Configuration.Info.Schema, StringComparison.OrdinalIgnoreCase)
                && table.Name.Parts.Last().StartsWith(Configuration.Info.Prefix, StringComparison.OrdinalIgnoreCase)
                && table.Name.Parts.Last().EndsWith(Configuration.Info.Suffix, StringComparison.OrdinalIgnoreCase);
        }

        public override IList<SqlRuleProblem> Analyze(SqlRuleExecutionContext ruleExecutionContext)
        {
            var tableElement = ruleExecutionContext.ModelElement;
            var tableName = GetElementName(ruleExecutionContext, tableElement);

            //Ensure that it's effectively an info table
            if (IsInfo(tableElement))
                return OnAnalyze(tableName, tableElement).ToList();

            return new List<SqlRuleProblem>();
        }

        protected abstract IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table);
        
    }
}
