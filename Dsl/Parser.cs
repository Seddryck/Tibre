using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace Tibre.Dsl
{
    public class Parsing
    {
        public readonly static Parser<Concept> Concept =
        (
            from label in Grammar.Line
            from anchorKeyword in Keyword.Anchor
            from anchorField in Grammar.Line
            select new Concept() { Label = label , Anchor = anchorField}
        );

        public readonly static Parser<IEnumerable<string>> Field =
        (
            from labels in Grammar.Tabs
            select labels
        );
    }
}
