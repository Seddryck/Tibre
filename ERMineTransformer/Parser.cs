using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibre.Core.Objects;
using Tibre.Core.Factories;

namespace Tibre.ERMineTransformer
{
    public class Parser
    {
        private IList<Builders.AbstractTibreBuilder> builders { get; set; }

        public Parser()
        {
            builders = new List<Builders.AbstractTibreBuilder>()
            {
                new Builders.AnchorsBuilder(),
                new Builders.InfosBuilder(),
                new Builders.LinkInfosBuilder(),
                new Builders.LinksBuilder(),
                new Builders.KnotsBuilder()
            };
        }

        public Model Parse(string text)
        {
            var internalParser = new ERMine.Core.Parsing.Parser();
            var ermineModel = internalParser.Parse(text);
            return Parse(ermineModel);
        }

        public Model ParseFile(string path)
        {
            var internalParser = new ERMine.Core.Parsing.Parser();
            var ermineModel = internalParser.ParseFile(path);
            return Parse(ermineModel);
        }

        protected Model Parse(ERMine.Core.Modeling.Model model)
        {
            var tibre = new Model();
            foreach (var builder in builders)
            {
                builder.Setup(model);
                builder.Build();
                tibre.Tables.AddRange(builder.GetTables());
            }
            return tibre;
        }
    }
}

