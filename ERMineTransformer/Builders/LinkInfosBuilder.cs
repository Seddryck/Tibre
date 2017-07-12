using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Factories;
using Tibre.Core.Objects;

namespace Tibre.ERMineTransformer.Builders
{
    public class LinkInfosBuilder : AbstractTibreBuilder
    { 
        protected override IEnumerable<Table> OnBuild()
        {
            foreach (var entity in ERModel.Entities)
            {
                var factory = new LinkInfoFactory();
                var linkInfo = factory.Build(entity.Label);
                yield return linkInfo;
            }
        }
    }
}
