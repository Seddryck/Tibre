using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class Knot : Table
    {
        public SqlIdentity Identity { get; internal set; }
        public SqlColumn Label { get; internal set; }
        public SqlColumn SortPosition { get; internal set; }
        public IList<SqlColumn> Components { get; internal set; }

        public override IEnumerable<SqlColumn> Columns
        {
            get
            {
                return Enumerable.Repeat(Identity, 1).Cast<SqlColumn>()
                    .Union(Enumerable.Repeat(Label,1))
                    .Union(Enumerable.Repeat(SortPosition,1))
                    .Union(Components);
            }
        }
    }
}
