using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Factories;
using Tibre.Core.Objects;

namespace Tibre.ERMineTransformer.Builders
{
    public class KnotsBuilder : AbstractTibreBuilder
    { 
        protected override IEnumerable<Table> OnBuild()
        {
            foreach (var domain in ERModel.Domains)
            {
                var factory = new KnotFactory();
                var knot = factory.Build(domain.Label, domain.Values.ToArray());
                yield return knot;
            }
        }
    }
}
