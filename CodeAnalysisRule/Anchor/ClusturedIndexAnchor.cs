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
            , "info table must have a clustured index corresponding to the identity column."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class ClusturedIndexAnchor : BaseAnchor
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.info.ClusturedIndexinfo";
        public ClusturedIndexAnchor()
        {
            SupportedElementTypes = new[] { Table.TypeClass };
        }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            //Get the columns of the table
            var indexes = table.GetReferencing(Index.IndexedObject);
            if (indexes.Count() == 0)
                yield return new SqlRuleProblem(string.Format("The info table {0} has no index", name), table);

            //Ensure that one of them is effecively a clustured index
            var clusturedIndex = indexes.FirstOrDefault(i => i.GetProperty<bool>(Index.Clustered));
            if (clusturedIndex == null)
                yield return new SqlRuleProblem(string.Format("No clustured index for the info table {0}", name), table);
            else
            {
                //Ensure that this index is effectively unique
                var uniqueClusturedIndex = indexes.FirstOrDefault(i => i.GetProperty<bool>(Index.Clustered) && i.GetProperty<bool>(Index.Unique));
                if (uniqueClusturedIndex == null)
                    yield return new SqlRuleProblem(string.Format("Clustured index for the info table {0} is not unique ", name), table);
                else
                {
                    //Ensure that the clustured index is active only on the identity column
                    var columns = uniqueClusturedIndex.GetReferenced(Index.Columns);
                    if (columns.Count() > 1)
                        yield return new SqlRuleProblem(string.Format("The info table {0} has a clustured index but this index has more than one column.", name), table);

                    if (columns.Count(c => c.GetProperty<bool>(Column.IsIdentity)) == 0)
                        yield return new SqlRuleProblem(string.Format("The info table {0} has a clustured index but this index doesn't include the identity column.", name), table);
                }
            }
        }
    }
}
