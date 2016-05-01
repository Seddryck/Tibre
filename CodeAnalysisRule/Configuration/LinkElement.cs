using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.CodeAnalysisRule.Configuration
{
    class LinkElement : ConfigurationElement
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

        [ConfigurationProperty("suffix", DefaultValue = "Link", IsRequired = false)]
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


        [ConfigurationProperty("is-first", DefaultValue = "IsFirstDate", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String IsFirst
        {
            get
            {
                return (String)this["is-first"];
            }
            set
            { this["is-first"] = value; }
        }


        [ConfigurationProperty("is-last", DefaultValue = "IsLastDate", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String IsLast
        {
            get
            {
                return (String)this["is-last"];
            }
            set
            { this["is-last"] = value; }
        }
    }
}
