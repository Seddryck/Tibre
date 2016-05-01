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
            , "Anchor table must have an index on the business key."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class BusinessKeyIndexAnchor : BaseAnchor
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.Anchor.BusinessKeyIndexAnchor";

        public BusinessKeyIndexAnchor()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            //Get the indexes of the table
            var indexes = table.GetReferencing(Index.IndexedObject);
            if (indexes.Count() == 0)
                yield return new SqlRuleProblem(string.Format("The anchor table {0} has no index", name), table);

            //Ensure that one of them is effecively not a clustered index
            var nonClusturedIndexes = indexes.Where(i => !i.GetProperty<bool>(Index.Clustered) && i.GetProperty<bool>(Index.Unique));
            if (nonClusturedIndexes == null)
                yield return new SqlRuleProblem(string.Format("No existing non-clustered unique index for the anchor table {0}", name), table);
            else
            {
                //Ensure that at least one of them is name BK
                var bkIndexes = nonClusturedIndexes.Where(i => i.Name.Parts.Last().StartsWith(Configuration.Anchor.BusinessKeyPrefix));
                if (bkIndexes.Count()==0)
                    yield return new SqlRuleProblem(string.Format("None of the non-clustered unique indexes for the anchor table {0} are starting by BK_", name), table);
                else
                {
                    foreach (var bkIndex in bkIndexes)
                    {
                        //Ensure that the unique index is not active on the identity column
                        var columns = bkIndex.GetReferenced(Index.Columns).Where(c => c.GetProperty<bool>(Column.IsIdentity));        
                        if (columns.Count()>0)
                            yield return new SqlRuleProblem(string.Format("The business key (non-clustered unique index) {1} for the anchor table {0} contains the identity column.", name, bkIndex.Name), table);

                        //By default SQL Server will include the indentity column (because this column should be the clustered index)
                        var includedColumns = bkIndex.GetReferenced(Index.IncludedColumns).Where(c => c.GetProperty<bool>(Column.IsIdentity));
                        if (includedColumns.Count() > 0)
                            yield return new SqlRuleProblem(string.Format("The business key (non-clustered unique index) {1} for the anchor table {0} includes the identity column.", name, bkIndex.Name), table);
                    }
                }
            }
        }
    }
}
