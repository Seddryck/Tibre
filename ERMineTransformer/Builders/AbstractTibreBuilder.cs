using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;

namespace Tibre.ERMineTransformer.Builders
{
    public abstract class AbstractTibreBuilder
    {
        protected bool IsBuilt {private get; set;}
        protected bool IsSetup { private get; set; }
        protected IEnumerable<Table> Artefact { get; private set; }

        protected ERMine.Core.Modeling.Model BluePrint { get; private set; }

        public void Setup(ERMine.Core.Modeling.Model blueprint)
        {
            this.BluePrint = blueprint;
            IsSetup = true;
        }

        public void Build()
        {
            if (!IsSetup)
                throw new InvalidOperationException();

            if (IsBuilt)
                throw new InvalidOperationException();
            Artefact = OnBuild();
            IsBuilt = true;
        }

        protected abstract IEnumerable<Table> OnBuild();
        public virtual IEnumerable<Table> GetTables()
        {
            if (!IsBuilt)
                throw new InvalidOperationException();

            return Artefact;
        }
    }
}
