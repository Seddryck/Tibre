using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    class IdentityFactory
    {
        public SqlIdentity Build(string name)
        {
            return new SqlIdentity() { Name = name };
        }
    }
}
