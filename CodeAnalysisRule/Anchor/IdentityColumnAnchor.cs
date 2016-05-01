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
            , "Anchor table must have an identity column."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class IdentityColumnAnchor : BaseAnchor
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.Anchor.IdentityColumnAnchor";

        public IdentityColumnAnchor()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            //Get the columns of the table
            var columns = table.GetReferenced(Table.Columns);
            if (columns.Count() == 0)
                yield return new SqlRuleProblem(string.Format("The anchor table {0} has no column", name), table);

            //Ensure that one of them is effecively an identity
            var identityColumn = columns.FirstOrDefault(c => c.GetProperty<bool>(Column.IsIdentity));
            if (identityColumn == null)
                yield return new SqlRuleProblem(string.Format("No identity column for the anchor table {0}", name), table);
            else
            {
                if (!string.IsNullOrEmpty(Configuration.Info.IdentityNamingConvention))
                {
                    //Ensure that this column has correct naming convention
                    var actualIdentityColumnName = identityColumn.Name.Parts.Last();
                    var expectedIdentityColumnName = string.Format(Configuration.Anchor.IdentityNamingConvention, table.Name.Parts.Last());
                    if (string.Compare(actualIdentityColumnName, expectedIdentityColumnName, false) != 0)
                        yield return new SqlRuleProblem(string.Format("Identity column for the anchor table {0} doesn't follow the naming convention: '{1}' in place of '{2}'", name, actualIdentityColumnName, expectedIdentityColumnName), table);
                }
            }

        }
    }
}
