using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public abstract class Table
    {
        public SqlIdentifier Fullname { get; internal set; }
        public string Shortname
        {
            get
            {
                return Fullname.Name;
            }
        }
        public string Schema
        {
            get
            {
                return Fullname.Schema;
            }
        }

        public abstract IEnumerable<SqlColumn> Columns { get; }
    }
}
