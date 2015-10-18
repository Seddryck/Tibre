﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.CodeAnalysisRule.Configuration
{
    class InfoElement : ConfigurationElement
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

        [ConfigurationProperty("sufix", DefaultValue = "Info", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 0, MaxLength = 60)]
        public String Sufix
        {
            get
            {
                return (String)this["sufix"];
            }
            set
            {
                this["sufix"] = value;
            }
        }
    }
}
