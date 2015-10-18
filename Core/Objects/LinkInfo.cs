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
    }
}
