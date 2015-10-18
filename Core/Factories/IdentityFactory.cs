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
        public TSqlColumn Build(string name)
        {
            var factory = new TSqlDataTypeFactory();
            var dataType = factory.Build(SqlDataType.Int);

            return new TSqlColumn()
            {
                IsIdentity = true,
                Name = name,
                DataType = dataType
            };
        }
    }
}
