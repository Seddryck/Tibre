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
            , "Link table must have an index with is-last and this index should be filtered"
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class IsLastIndexLink : BaseLink
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.Link.IsLastIndexLink";
        public IsLastIndexLink()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            return OnAnalyze(name, table, Configuration.Link.IsLast);
        }

        protected IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table, string columnName)
        {
            var indexes = table.GetReferencing(Index.IndexedObject);
            var lastIndexes = indexes.Where(i => i.GetReferenced(Index.Columns).Last().Name.Parts.Last()
                                                    == columnName);
            if (lastIndexes.Count() == 0)
                yield return new SqlRuleProblem(string.Format("No index on the column '{0}' for link table {1}", columnName, name), table);

            var filteredIndexes = lastIndexes.Where(i => i.GetProperty<bool>(Index.FilterPredicate));
            if (filteredIndexes.Count() == 0)
                yield return new SqlRuleProblem(string.Format("An index exists on the column '{0}' for link table {0} but this index is not filtered.", columnName, name), table);

            foreach (var lastIndex in lastIndexes)
            {
                var indexColumns = lastIndex.GetReferenced(Index.Columns).Where(c => c.Name.Parts.Last() != columnName);
                var includedColumns = lastIndex.GetReferenced(Index.IncludedColumns);
                var allColumns = indexColumns.Union(includedColumns);

                if (allColumns.Count() == 0)
                    yield return new SqlRuleProblem(string.Format("The index {0} for link table {1} has no additional column or included column.", lastIndex.Name, name), table);
            }
        }
    }
}
