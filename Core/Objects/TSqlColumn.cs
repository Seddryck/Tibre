using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class TSqlColumn
    {
        public string Name { get; internal set; }
        public virtual TSqlDataType DataType { get; internal set; }
        public virtual bool IsNull { get; internal set; }
        public virtual bool IsIdentity { get; internal set; }
    }
}
