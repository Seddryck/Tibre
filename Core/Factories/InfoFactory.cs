﻿using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    public class InfoFactory
    {
        public Info Build(string name, IEnumerable<Tuple<string, string>> fields)
        {
            return this.Build(name, null, fields);
        }

        public Info Build(string name, Tuple<string, string> key, IEnumerable<Tuple<string, string>> fields)
        {
            return this.Build("dwh", name, "Info", key, fields);
        }
        
        public Info Build(string schema, string name, string suffix, Tuple<string, string> key, IEnumerable<Tuple<string, string>> fields)
        {
            var tableName = new ObjectIdentifier(new string[] { schema, name + suffix });
            var identity = new IdentityFactory().Build(name + suffix + "Id");
            var fieldColumns = new List<TSqlColumn>(); 

            var sqlDataTypeFactory = new TSqlDataTypeFactory();
            var columnFactory = new ColumnFactory();

            TSqlDataType sqlDataType;
            TSqlColumn keyColumn = null;

            if (key!=null)
            {
                sqlDataType = sqlDataTypeFactory.Build(key.Item2);
                keyColumn = columnFactory.Build(key.Item1, sqlDataType);
            }


            foreach (var field in fields)
            {
                var fieldName = field.Item1;

                sqlDataType = sqlDataTypeFactory.Build(field.Item2);
                var column = columnFactory.Build(fieldName, sqlDataType);
                fieldColumns.Add(column);
            }

            var info = new Info()
            {
                Name = tableName,
                Identity = identity,
                Fields = fieldColumns
            };

            return info;
        }

        public Info Build(string name, IEnumerable<TSqlColumn> fieldColumns)
        {
            var tableName = new ObjectIdentifier(new string[] { "dwh", name + "Info" });
            var identity = new IdentityFactory().Build(name + "Info" + "Id");

            var info = new Info()
            {
                Name = tableName,
                Identity = identity,
                Fields = new List<TSqlColumn>(fieldColumns)
            };

            return info;
        }

    }
}
