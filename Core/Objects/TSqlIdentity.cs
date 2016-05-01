using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class TSqlIdentity : TSqlColumn
    {
        private static TSqlDataType identityTSqlDataType = new TSqlDataType(SqlDataType.Int);

        public override TSqlDataType DataType { get { return identityTSqlDataType; } }
        public override bool IsNull { get { return false; } }
        public override bool IsIdentity { get { return true; } }
    }
}
