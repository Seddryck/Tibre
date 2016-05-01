using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core.Objects
{
    public class Model
    {
        public List<Table> Tables { get; private set; }

        public Model()
        {
            Tables = new List<Table>();
        }
    }
}
