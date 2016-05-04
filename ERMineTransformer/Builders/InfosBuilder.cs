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
                if (entity.Attributes.Any(a => a.IsMultiValued))
                    throw new ArgumentException("Multivalued attributes are not supported at the moment.");

                var items = entity.Attributes.Where(a => a.IsPartOfPrimaryKey == false).Select(a => new ColumnFactory().Build(a.Label, a.DataType, a.IsNullable, false, a.DefaultFormula, a.DerivedFormula, a.IsSparse));
                var info = factory.Build(entity.Label, items);
                yield return info;
            }
        }
    }
}
