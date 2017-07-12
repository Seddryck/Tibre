using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    public class KnotFactory
    {
        public Knot Build(string name, string[] labels)
        {
            var maxLength = labels.Max(x => x.Length);

            var columnFactory = new ColumnFactory();
            var labelColumn = columnFactory.Build("Label", $"varchar({maxLength})");

            return Build("dwh", name, labelColumn);
        }
        
        public Knot Build(string schema, string name, SqlColumn labelColumn)
        {
            var tableName = new SqlIdentifier(schema, name );

            var identityFactory = new IdentityFactory();
            var identity = identityFactory.Build(name + "Id");

            var knot = new Knot()
            {
                Fullname = tableName,
                Identity = identity,
                Label = labelColumn
            };

            return knot;
        }

    }
}
