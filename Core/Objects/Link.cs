using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class Link : Table
    {
        public SqlColumn DateKey { get; internal set; }
        public IList<SqlColumnList> UniqueKeys { get; internal set; }
        public IList<SqlColumn> ForeignKeys { get; internal set; }
        public IList<SqlColumn> Filters { get; internal set; }


        public override IEnumerable<SqlColumn> Columns
        {
            get
            {
                return ForeignKeys.Union(Enumerable.Repeat(DateKey, 1)).Union(Filters);
            }
        }
    }
}
