using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.Model;

namespace Tibre.Core.Objects
{
    public class Link : Table
    {
        public TSqlColumn DateKey { get; internal set; }
        public IList<TSqlColumn> UniqueKeys { get; internal set; }
        public IList<TSqlColumn> ForeignKeys { get; internal set; }
        public IList<TSqlColumn> Filters { get; internal set; }
    }
}
