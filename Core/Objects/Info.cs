using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.Model;

namespace Tibre.Core.Objects
{
    public class Info : Table
    {
        public TSqlIdentity Identity { get; internal set; }
        public IList<TSqlColumn> Fields { get; internal set; }

        public override IEnumerable<TSqlColumn> Columns
        {
            get
            {
                return Enumerable.Repeat(Identity, 1).Union(Fields);
            }
        }
    }
}
