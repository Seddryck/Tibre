using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.Model;

namespace Tibre.Core.Objects
{
    public class LinkInfo : Link
    {
        public TSqlColumn AnchorKey { get; internal set; }
        public TSqlColumn InfoKey { get; internal set; }

        public TSqlColumnList UniqueKey
        {
            get
            {
                if (UniqueKeys == null || UniqueKeys.Count == 0)
                    return null;
                else
                    return UniqueKeys[0];
            }
            set
            {
                if (UniqueKeys == null)
                    UniqueKeys = new List<TSqlColumnList>() { value };
                else if (UniqueKeys.Count == 0)
                    UniqueKeys.Add(value);
                else if (UniqueKeys.Count == 1)
                    UniqueKeys[0] = value;
                else
                    throw new InvalidOperationException();
            }
        }
    }
}
