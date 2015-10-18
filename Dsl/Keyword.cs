using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Dsl
{
    class Keyword
    {
        public static readonly Parser<string> Anchor = Parse.IgnoreCase("Anchor").Text().Token();
    }
}
