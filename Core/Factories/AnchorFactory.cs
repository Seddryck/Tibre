﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    public class AnchorFactory
    {
        public Anchor Build(string name, string businessKey, string dataType)
        {
            return this.Build("dwh", name, new Tuple<string, string>[] { new Tuple<string, string>(businessKey, dataType) });
        }

        public Anchor Build(string name, IEnumerable<Tuple<string, string>> businessKeys)
        {
            return this.Build("dwh", name, businessKeys);
        }

        public Anchor Build(string schema, string name, IEnumerable<Tuple<string, string>> businessKeys)
        {
            var businessKeyColumns = new List<SqlColumn>(); 
            foreach (var businessKey in businessKeys)
            {
                var businessKeyName = businessKey.Item1;

                var sqlDataTypeFactory = new SqlDataTypeFactory();
                var sqlDataType = sqlDataTypeFactory.Build(businessKey.Item2);

                var columnFactory = new ColumnFactory();
                var column = columnFactory.Build(businessKeyName, sqlDataType);
                businessKeyColumns.Add(column);
            }

            return Build(schema, name, businessKeyColumns);
        }

        public Anchor Build(string name, IEnumerable<SqlColumn> businessKeyColumns)
        {
            return this.Build("dwh", name, businessKeyColumns);
        }

        public Anchor Build(string schema, string name, IEnumerable<SqlColumn> businessKeyColumns)
        {
            var tableName = new SqlIdentifier(schema, name );

            var identityFactory = new IdentityFactory();
            var identity = identityFactory.Build(name + "Id");

            var anchor = new Anchor()
            {
                Fullname = tableName,
                Identity = identity,
                BusinessKey = new List<SqlColumn>(businessKeyColumns)
            };

            return anchor;
        }

    }
}
