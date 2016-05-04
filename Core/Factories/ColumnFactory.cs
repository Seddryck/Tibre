using Microsoft.SqlServer.Dac.Model;
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
        public TSqlColumn Build(string name, string dataType, bool isNullable = false, bool isIdentity = false, string defaultFormula = "", string derivedFormula = "", bool isSparse = false)
        {
            var factory = new TSqlDataTypeFactory();
            var tsqlDataType = factory.Build(dataType);
            return this.Build(name, tsqlDataType, isNullable, isIdentity, defaultFormula, derivedFormula, isSparse);
        }

        public TSqlColumn Build(string name, TSqlDataType dataType, bool isNullable=false, bool isIdentity = false, string defaultFormula = "", string derivedFormula = "", bool isSparse = false)
        {
            return new TSqlColumn()
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
