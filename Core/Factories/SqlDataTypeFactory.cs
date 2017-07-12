using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    class SqlDataTypeFactory
    {
        
        public SqlDataType Build(string value)
        {
            value = value.Replace(" ", "");
            var sqlDef = value;
            if (value.Contains("("))
                sqlDef = value.Substring(0, value.IndexOf("(")).ToLower();
            
            int? precision = null;
            int? scale = null;
            if (value.Contains("("))
            {
                var figure = value.Replace(")","").Substring(value.IndexOf("(")+1);
                var array = figure.Split(',');
                precision = Convert.ToInt32(array[0]);
                if (array.Length>1)
                    scale = Convert.ToInt32(array[1]);
            }

            var args = new List<int>();
            if (precision.HasValue)
                args.Add(precision.Value);
            if (scale.HasValue)
                args.Add(scale.Value);

            switch (sqlDef)
            {
                
                case "bit":
                case "boolean": return new BooleanDataType();
                case "time": return new TimeDataType();
                case "date": return new DateDataType();
                case "char": return new FixedLengthCharacterStringDataType(precision.HasValue ? precision.Value : 1);
                case "varchar": return new VaryingLengthCharacterStringDataType(precision.HasValue ? precision.Value : 255);
                case "int":
                case "integer": return new IntegerDataType(precision.HasValue ? precision.Value : 8);
                case "float": return new FloatDataType(precision.HasValue ? precision.Value : 8);
                case "datetime": return new DateTimeDataType(precision.HasValue ? precision.Value : 8);
                case "decimal":
                case "numeric": return new DecimalDataType(precision.HasValue ? precision.Value : 8, scale.HasValue ? scale.Value : 3);
                default:
                    return new NonGenericDataType(sqlDef, args);
            }
        }
    }
}
