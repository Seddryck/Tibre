using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace Tibre.CodeAnalysisRule.Configuration
{
    class CodeAnalysisRuleConfiguration
    {
        public CodeAnalysisRuleConfiguration()
        {
        }

        protected internal virtual TibreSection Read()
        {
            var dllPath = AssemblyDirectory + Path.DirectorySeparatorChar + Path.GetFileName(Assembly.GetExecutingAssembly().Location);

            if (!File.Exists(dllPath + ".config"))
                return new TibreSection();

            var dllConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            return (TibreSection)dllConfig.Sections["tibre"];
        }

        protected static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
