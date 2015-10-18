using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.Model;

namespace Tibre.Core.Objects
{
    public class Link
    {
        public ObjectIdentifier Name {get; internal set;}
        public string Shortname
        {
            get
            {
                return Name.Parts.Last();
            }
        }
        public string Schema
        {
            get
            {
                return Name.Parts.Reverse().Take(2).Last();
            }
        }

        public TSqlColumn DateKey { get; internal set; }
        public IList<TSqlColumn> UniqueKeys { get; internal set; }
        public IList<TSqlColumn> ForeignKeys { get; internal set; }
        public IList<TSqlColumn> Filters { get; internal set; }
    }
}
