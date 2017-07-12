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
            foreach (var entity in ERModel.Entities)
            {
                var factory = new AnchorFactory();
                var keyColumns = entity.Key.Attributes.Select(k => new ColumnFactory().Build(k.Label, k.DataType, k.IsNullable));
                var anchor = factory.Build(entity.Label, keyColumns);
                yield return anchor;
            }
        }
    }
}
