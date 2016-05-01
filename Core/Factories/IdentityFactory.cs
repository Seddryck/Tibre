using Microsoft.SqlServer.Dac.Model;
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
        public TSqlIdentity Build(string name)
        {
            return new TSqlIdentity() { Name = name };
        }
    }
}
