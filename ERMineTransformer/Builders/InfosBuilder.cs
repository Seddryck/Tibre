using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Factories;
using Tibre.Core.Objects;

namespace Tibre.ERMineTransformer.Builders
{
    public class InfosBuilder : AbstractTibreBuilder
    { 
        protected override IEnumerable<Table> OnBuild()
        {
            foreach (var entity in BluePrint.Entities)
            {
                var factory = new InfoFactory();
                var items = entity.Attributes.Where(a => a.IsPartOfPrimaryKey == false).Select(a => Tuple.Create(a.Label, a.DataType));
                var info = factory.Build(entity.Label, items);
                yield return info;
            }
        }
    }
}
