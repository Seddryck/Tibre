using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class SqlColumn
    {
        public string Name { get; internal set; }
        public virtual SqlDataType DataType { get; internal set; }
        public virtual bool IsNullable { get; internal set; }
        public virtual bool IsIdentity { get; internal set; }
        public virtual bool IsSparse { get; internal set; }
        public virtual string Default { get; internal set; }
        public virtual bool IsDefault { get { return !string.IsNullOrEmpty(Default); } }
        public virtual string Derived { get; internal set; }
        public virtual bool IsDerived { get { return !string.IsNullOrEmpty(Derived); } }
        public virtual bool IsImmutable { get; internal set; }

    }
}
