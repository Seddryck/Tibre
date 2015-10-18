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
        public Link Build(string firstAnchor, string secondAnchor)
        {
            
            var firstAnchorId = new Tuple<string, string>(firstAnchor + "Id", "int");
            var secondAnchorId = new Tuple<string, string>(secondAnchor + "Id", "int");
            var dateId = new Tuple<string, string>("DateId", "int");
            var filters = new List<Tuple<string, string>>();
            filters.Add(new Tuple<string, string>("IsFirstDate", "bit"));
            filters.Add(new Tuple<string, string>("IsLastDate", "bit"));
            return this.Build("dwh", firstAnchor + secondAnchor + "Link", new List<Tuple<string, string>>() {firstAnchorId, secondAnchorId}, dateId, filters);
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
