using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Factories;
using Tibre.Core.Objects;

namespace Tibre.ERMineTransformer.Builders
{
    public class AnchorsBuilder : AbstractTibreBuilder
    { 
        protected override IEnumerable<Table> OnBuild()
        {
            foreach (var entity in BluePrint.Entities)
            {
                var factory = new AnchorFactory();
                var keyItems = entity.Key.Attributes.Select(k => Tuple.Create(k.Label, k.DataType));
                var anchor = factory.Build(entity.Label, keyItems);
                yield return anchor;
            }
        }
    }
}
