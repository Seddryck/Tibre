using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    public class LinkFactory
    {
        public Link Build(IEnumerable<string> anchors)
        {
            var linkName = string.Empty;
            var listId = new List<Tuple<string, string>>();
            foreach (var anchor in anchors)
            {
                linkName += anchor;
                listId.Add(new Tuple<string, string>(anchor + "Id", "int"));
            }

            var dateId = new Tuple<string, string>("DateId", "int");
            var filters = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("IsFirstDate", "bit")
                , new Tuple<string, string>("IsLastDate", "bit")
            };

            return this.Build("dwh", linkName + "Link", listId, dateId, filters);
        }

        public Link Build(string firstAnchor, string secondAnchor)
        {
            return this.Build(new List<string>() { firstAnchor, secondAnchor });
        }

        public Link Build(string firstAnchor, string secondAnchor, Connectivity connectivity)
        {
            var link = this.Build(new List<string>() { firstAnchor, secondAnchor });
            link.UniqueKeys = new List<TSqlColumnList>();

            switch (connectivity)
            {
                case Connectivity.OneToOne:
                    link.UniqueKeys.Add(new TSqlColumnList() { link.ForeignKeys[0], link.DateKey });
                    link.UniqueKeys.Add(new TSqlColumnList() { link.ForeignKeys[1], link.DateKey });
                    break;
                case Connectivity.OneToMany:
                    link.UniqueKeys.Add(new TSqlColumnList()
                    {
                        link.ForeignKeys[0]
                        , link.DateKey
                    });
                    break;
                case Connectivity.ManyToOne:
                    link.UniqueKeys.Add(new TSqlColumnList()
                    {
                        link.ForeignKeys[1]
                        , link.DateKey
                    });
                    break;
                case Connectivity.ManyToMany:
                    link.UniqueKeys.Add(new TSqlColumnList()
                    {
                        link.ForeignKeys[0]
                        , link.ForeignKeys[1]
                        , link.DateKey
                    });
                    break;
                default:
                    break;
            }

            return link;
        }

        public Link Build(string schema, string name, IEnumerable<Tuple<string, string>> anchors, Tuple<string, string> dateId, IEnumerable<Tuple<string, string>> filters)
        {
            var tableName = new ObjectIdentifier(new string[] { schema, name });
            var anchorsColumns = new List<TSqlColumn>();

            var sqlDataTypeFactory = new TSqlDataTypeFactory();
            TSqlDataType sqlDataType;
            var columnFactory = new ColumnFactory();

            foreach (var anchor in anchors)
            {
                sqlDataType = sqlDataTypeFactory.Build(anchor.Item2);
                var column = columnFactory.Build(anchor.Item1, sqlDataType);
                anchorsColumns.Add(column);
            }

            sqlDataType = sqlDataTypeFactory.Build(dateId.Item2);
            var dateColumn = columnFactory.Build(dateId.Item1, sqlDataType);

            var filterColumns = new List<TSqlColumn>();
            foreach (var filter in filters)
            {
                sqlDataType = sqlDataTypeFactory.Build(filter.Item2);
                var column = columnFactory.Build(filter.Item1, sqlDataType);
                filterColumns.Add(column);
            }

            anchorsColumns.Add(dateColumn);
            var link = new Link()
            {
                Name = tableName,
                ForeignKeys = anchorsColumns,
                DateKey = dateColumn,
                Filters = filterColumns
            };
            return link;
        }

    }
}
