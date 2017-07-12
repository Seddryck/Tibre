using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class SqlIdentifier
    {
        public string Schema { get; private set; }
        public string Name { get; private set; }

        public SqlIdentifier(string schema, string name)
        {
            Schema = schema;
            Name = name;
        }
    }
}
