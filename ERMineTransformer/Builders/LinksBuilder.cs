using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Factories;
using Tibre.Core.Objects;

namespace Tibre.ERMineTransformer.Builders
{
    public class LinksBuilder : AbstractTibreBuilder
    { 
        protected override IEnumerable<Table> OnBuild()
        {
            foreach (var relationship in BluePrint.Relationships)
            {
                var factory = new LinkFactory();
                var link = factory.Build(relationship.Entities.Select(e=> e.Label));
                yield return link;
            }
        }
    }
}
