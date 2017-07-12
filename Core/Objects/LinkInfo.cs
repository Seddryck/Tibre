using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class LinkInfo : Link
    {
        public SqlColumn AnchorKey { get; internal set; }
        public SqlColumn InfoKey { get; internal set; }

        public SqlColumnList UniqueKey
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
                    UniqueKeys = new List<SqlColumnList>() { value };
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
