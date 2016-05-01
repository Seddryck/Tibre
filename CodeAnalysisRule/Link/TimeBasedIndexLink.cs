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
            , "Link table must have an index on time-based column and this index includes other Id columns."
            , Category = "Tibre.Rules.Design"
            , RuleScope = SqlRuleScope.Element)]
    public class TimeBasedIndexLink : BaseLink
    {
        public const string RuleId = "Tibre.CodeAnalysisRule.Link.TimeBasedIndexLink";
        public TimeBasedIndexLink()
            : base()
        { }

        protected override IEnumerable<SqlRuleProblem> OnAnalyze(string name, TSqlObject table)
        {
            var indexes = table.GetReferencing(Index.IndexedObject);
            var timeBasedIndexes = indexes.Where(i => i.GetReferenced(Index.Columns).First().Name.Parts.Last()
                                                    == Configuration.TimeBased.Key);
            if (timeBasedIndexes.Count() == 0)
                yield return new SqlRuleProblem(string.Format("No index where first column is '{0}' for link table {1}", Configuration.TimeBased.Key, name), table);

            foreach (var tbIndex in timeBasedIndexes)
            {
                var unexpectedColumns = tbIndex.GetReferenced(Index.Columns).Where(c => c.Name.Parts.Last() != Configuration.TimeBased.Key);
                if (unexpectedColumns.Count()>0)
                {
                    yield return new SqlRuleProblem(
                            string.Format(
                                    "The time-based index '{0}' for link table '{1}' contains additional columns. Unexpected column{2} '{3}'"
                                    , tbIndex.Name
                                    , name
                                    , (unexpectedColumns.Count() == 1) ? " is " : "s are  "
                                    , string.Join("', '", unexpectedColumns.Select(c => c.Name.Parts.Last()))
                            ), table);
                }

                var idColumns = table.GetReferenced(Table.Columns).Where(c => c.Name.Parts.Last() != Configuration.TimeBased.Key && c.Name.Parts.Last().EndsWith("Id"));
                var includedColumns = tbIndex.GetReferenced(Index.IncludedColumns);

                var missingColumns = idColumns.Except(includedColumns);
                if (missingColumns.Count()>0)
                {
                    yield return new SqlRuleProblem(
                            string.Format(
                                    "The time-based index '{0}' for link table '{1}' doesn't include some Id columns. Missing column{2} '{3}'"
                                    , tbIndex.Name
                                    , name
                                    , (missingColumns.Count() == 1) ? " is " : "s are  "
                                    , string.Join("', '", missingColumns.Select(c => c.Name.Parts.Last()))
                            ), table);
                }
                    
            }
        }
    }
}
