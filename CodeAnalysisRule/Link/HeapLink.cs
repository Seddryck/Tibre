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
            , "Link table must be heap."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class HeapLink : BaseLink
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.Link.HeapLink";
        public HeapLink()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            var indexes = table.GetReferencing(Index.IndexedObject);
            var firstTimeIndexes = indexes.Where(i => i.GetReferenced(Index.Columns).First().Name.Parts.Last()
                                                    == Configuration.Link.IsFirst);
            if (firstTimeIndexes.Count() == 0)
                yield return new SqlRuleProblem(string.Format("No index where is-first column is '{0}' for link table {1}", Configuration.TimeBased.Key, name), table);

            foreach (var fIndex in firstTimeIndexes)
            {
                var indexColumns = fIndex.GetReferenced(Index.Columns).Where(c => c.Name.Parts.Last() != Configuration.TimeBased.Key);
                var includedColumns = fIndex.GetReferenced(Index.IncludedColumns);
                var allColumns = indexColumns.Union(includedColumns);

                if (allColumns.Count() == 0)
                    yield return new SqlRuleProblem(string.Format("The time-based index {0} for link table {1} has no additional or included columns", fIndex.Name, name), table);
                yield return new SqlRuleProblem(string.Format("The time-based index {0} for link table {1} has no additional or included columns", fIndex.Name, name), table);
            }
        }
    }
}
