using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class TSqlDataType
    {
        public TSqlDataType()
        {}

        public TSqlDataType(SqlDataType sqlDataType)
        : this(sqlDataType, null, null)
        { }

        public TSqlDataType(SqlDataType sqlDataType, int precision)
        : this(sqlDataType, precision, null)
        { }

        public TSqlDataType(SqlDataType sqlDataType, int? precision, int? scale)
        {
            SqlDataType = sqlDataType;
            Precision = precision.HasValue ? precision : null;
            Scale = scale.HasValue ? scale : null;
        }

        public SqlDataType SqlDataType { get; internal set; }
        public int? Precision { get; internal set; }
        public int? Scale { get; internal set; }

        public override string ToString()
        {
            return SqlDataType.ToString()
                + (Precision.HasValue ? "(" + Precision.Value.ToString() : string.Empty)
                + (Scale.HasValue ? ", " + Scale.Value.ToString() : string.Empty)
                + (Precision.HasValue ? ")" : string.Empty);
        }
    }
}
