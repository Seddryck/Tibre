using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.CodeAnalysisRule.Configuration
{
    class TimeBasedElement : ConfigurationElement
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

        [ConfigurationProperty("table", DefaultValue = "DimDate", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Table
        {
            get
            {
                return (String)this["table"];
            }
            set
            {
                this["table"] = value;
            }
        }

        [ConfigurationProperty("key", DefaultValue = "DateId", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Key
        {
            get
            {
                return (String)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("first", DefaultValue = "IsFirstDate", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String First
        {
            get
            {
                return (String)this["first"];
            }
            set
            {
                this["first"] = value;
            }
        }

        [ConfigurationProperty("last", DefaultValue = "IsLastDate", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Last
        {
            get
            {
                return (String)this["last"];
            }
            set
            {
                this["last"] = value;
            }
        }
    }
}
