using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Tibre.CodeAnalysisRule.Configuration;

namespace Tibre.CodeAnalysisRule
{
    public abstract class BaseRule : SqlCodeAnalysisRule
    {
        public BaseRule()
        {
            var config = new CodeAnalysisRuleConfiguration();
            Configuration = config.Read();
        }

        protected string GetElementName(SqlRuleExecutionContext ruleExecutionContext, TSqlObject modelElement)
        {
            // Get the element name using the built in DisplayServices. This provides a number of 
            // useful formatting options to
            // make a name user-readable
            var displayServices = ruleExecutionContext.SchemaModel.DisplayServices;
            string elementName = displayServices.GetElementName(modelElement, ElementNameStyle.EscapedFullyQualifiedName);
            return elementName;
        }

        internal TibreSection Configuration {get; private set;}
    }
}
