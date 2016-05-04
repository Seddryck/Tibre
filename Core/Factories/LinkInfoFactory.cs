using Microsoft.SqlServer.Dac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.Core.Factories
{
    public class LinkInfoFactory
    {
        public LinkInfo Build(string name)
        {
            
            var anchorId = new Tuple<string, string>(name + "Id", "int");
            var infoId = new Tuple<string, string>(name + "Info" + "Id", "int");
            var dateId = new Tuple<string, string>("DateId", "int");
            var filters = new List<Tuple<string, string>>();
            filters.Add(new Tuple<string, string>("IsFirstDate", "bit"));
            filters.Add(new Tuple<string, string>("IsLastDate", "bit"));
            return this.Build("dwh", name + "Link", anchorId, infoId, dateId, filters);
        }

        public LinkInfo Build(string schema, string name, Tuple<string, string> anchorId, Tuple<string, string> infoId, Tuple<string, string> dateId, IEnumerable<Tuple<string, string>> filters)
        {
            var tableName = new ObjectIdentifier(new string[] { schema, name });
            var foreignKeyColumns = new List<TSqlColumn>();
            
            var sqlDataTypeFactory = new TSqlDataTypeFactory();
            var sqlDataType = sqlDataTypeFactory.Build(anchorId.Item2);

            var columnFactory = new ColumnFactory();
            var anchorKey = columnFactory.Build(anchorId.Item1, sqlDataType);
            foreignKeyColumns.Add(anchorKey);

            sqlDataType = sqlDataTypeFactory.Build(infoId.Item2);
            var infoKey = columnFactory.Build(infoId.Item1, sqlDataType);
            foreignKeyColumns.Add(infoKey);

            sqlDataType = sqlDataTypeFactory.Build(dateId.Item2);
            var dateKey = columnFactory.Build(dateId.Item1, sqlDataType);
            foreignKeyColumns.Add(dateKey);

            var uniqueKeyColumns = new TSqlColumnList();
            uniqueKeyColumns.Add(anchorKey);
            uniqueKeyColumns.Add(dateKey);

            var filterColumns = new List<TSqlColumn>(); 
            foreach (var filter in filters)
            {
                sqlDataType = sqlDataTypeFactory.Build(filter.Item2);
                var column = columnFactory.Build(filter.Item1, sqlDataType);
                filterColumns.Add(column);
            }
            

            var link = new LinkInfo()
            {
                Name = tableName,
                UniqueKey = uniqueKeyColumns,
                DateKey = dateKey,
                AnchorKey = anchorKey,
                InfoKey = infoKey,
                ForeignKeys = foreignKeyColumns,
                Filters = filterColumns
            };
            return link;
        }
        
    }
}
