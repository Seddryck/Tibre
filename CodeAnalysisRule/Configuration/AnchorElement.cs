using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.CodeAnalysisRule.Configuration
{
    class AnchorElement : ConfigurationElement
    {
        [ConfigurationProperty("schema", DefaultValue = "dwh", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Schema
        {
            get
            {
                return (String)this["schema"];
            }
            set
            {
                this["schema"] = value;
            }
        }

        [ConfigurationProperty("prefix", DefaultValue = "", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 0, MaxLength = 60)]
        public String Prefix
        {
            get
            {
                return (String)this["prefix"];
            }
            set
            {
                this["prefix"] = value;
            }
        }

        [ConfigurationProperty("suffix", DefaultValue = "", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 0, MaxLength = 60)]
        public String Suffix
        {
            get
            {
                return (String)this["suffix"];
            }
            set
            {
                this["suffix"] = value;
            }
        }

        [ConfigurationProperty("identity-naming-convention", DefaultValue = "{0}Id", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]/;'\"|\\", MinLength = 0, MaxLength = 60)]
        public String IdentityNamingConvention
        {
            get
            {
                return (String)this["identity-naming-convention"];
            }
            set
            {
                this["identity-naming-convention"] = value;
            }
        }

        [ConfigurationProperty("business-key-prefix", DefaultValue = "BK_", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]/;'\"|\\", MinLength = 0, MaxLength = 60)]
        public String BusinessKeyPrefix
        {
            get
            {
                return (String)this["business-key-prefix"];
            }
            set
            {
                this["business-key-prefix"] = value;
            }
        }
    }
}
