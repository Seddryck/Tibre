using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class SqlIdentity : SqlColumn
    {
        private static SqlDataType identityTSqlDataType = new IntegerDataType(8);

        public override SqlDataType DataType { get { return identityTSqlDataType; } }
        public override bool IsNullable { get { return false; } }
        public override bool IsIdentity { get { return true; } }
    }
}
