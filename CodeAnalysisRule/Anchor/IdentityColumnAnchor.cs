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
    [ExportCodeAnalysisRuleAttribute(RuleId
            , "info table must have an identity column."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class IdentityColumnAnchor : BaseAnchor
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.info.IdentityColumninfo";

        public IdentityColumnAnchor()
        {
            SupportedElementTypes = new[] { Table.TypeClass };
        }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            //Get the columns of the table
            var columns = table.GetReferenced(Table.Columns);
            if (columns.Count() == 0)
                yield return new SqlRuleProblem(string.Format("The info table {0} has no column", name), table);

            //Ensure that one of them is effecively an identity
            var identityColumn = columns.FirstOrDefault(c => c.GetProperty<bool>(Column.IsIdentity));
            if (identityColumn == null)
                yield return new SqlRuleProblem(string.Format("No identity column for the info table {0}", name), table);
        }
    }
}
