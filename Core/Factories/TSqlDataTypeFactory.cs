using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    class TSqlDataTypeFactory
    {
        public TSqlDataType Build(SqlDataType value)
        {
            return new TSqlDataType()
            {
                SqlDataType = value,
            };
        }

        public TSqlDataType Build(string value)
        {
            value = value.Replace(" ", "");
            var sqlDef = value;
            if (value.Contains("("))
                sqlDef = value.Substring(0, value.IndexOf("("));
            var sqlDataType = (SqlDataType)Enum.Parse(typeof(SqlDataType), sqlDef, true);
            
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

            return new TSqlDataType()
            {
                SqlDataType = sqlDataType,
                Precision = precision,
                Scale = scale
            };
        }
    }
}
