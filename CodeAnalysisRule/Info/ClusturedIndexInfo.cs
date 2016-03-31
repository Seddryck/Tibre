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
    [ExportCodeAnalysisRuleAttribute(RuleId
            , "Info table must have a clustered index corresponding to the identity column."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class ClusturedIndexInfo : BaseInfo
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.Info.ClusturedIndexInfo";
        public ClusturedIndexInfo()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            //Get the indexes of the table
            var indexes = table.GetReferencing(Index.IndexedObject);
            if (indexes.Count() == 0)
                yield return new SqlRuleProblem(string.Format("The info table {0} has no index", name), table);

            //Ensure that one of them is effecively a clustered index
            var clusteredIndex = indexes.FirstOrDefault(i => i.GetProperty<bool>(Index.Clustered));
            if (clusteredIndex == null)
                yield return new SqlRuleProblem(string.Format("No clustered index for the info table {0}", name), table);
            else
            {
                //Ensure that this index is effectively unique
                var uniqueClusturedIndex = indexes.FirstOrDefault(i => i.GetProperty<bool>(Index.Clustered) && i.GetProperty<bool>(Index.Unique));
                if (uniqueClusturedIndex == null)
                    yield return new SqlRuleProblem(string.Format("Clustured index for the info table {0} is not unique ", name), table);
                else
                {
                    //Ensure that the clustered index is active only on the identity column
                    var columns = uniqueClusturedIndex.GetReferenced(Index.Columns);
                    if (columns.Count() > 1)
                        yield return new SqlRuleProblem(string.Format("The info table {0} has a clustered index but this index has more than one column.", name), table);

                    if (columns.Count(c => c.GetProperty<bool>(Column.IsIdentity)) == 0)
                        yield return new SqlRuleProblem(string.Format("The info table {0} has a clustered index but this index doesn't include the identity column.", name), table);
                }
            }
        }
    }
}
