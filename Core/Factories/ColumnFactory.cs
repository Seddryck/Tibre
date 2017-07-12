using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    public class ColumnFactory
    {
        public SqlColumn Build(string name, string dataType, bool isNullable = false, bool isIdentity = false, string defaultFormula = "", string derivedFormula = "", bool isSparse = false)
        {
            var factory = new SqlDataTypeFactory();
            var tsqlDataType = factory.Build(dataType);
            return this.Build(name, tsqlDataType, isNullable, isIdentity, defaultFormula, derivedFormula, isSparse);
        }

        public SqlColumn Build(string name, SqlDataType dataType, bool isNullable=false, bool isIdentity = false, string defaultFormula = "", string derivedFormula = "", bool isSparse = false)
        {
            return new SqlColumn()
            {
                Name = name
                , DataType = dataType
                , IsNullable = isNullable
                , IsIdentity = isIdentity
                , Default = defaultFormula
                , Derived = derivedFormula
                , IsSparse = isSparse
            };
        }
    }
}
