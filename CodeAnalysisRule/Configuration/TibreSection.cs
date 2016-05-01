using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Tibre.CodeAnalysisRule.Configuration
{
    class TibreSection : ConfigurationSection
    {
        [ConfigurationProperty("time-based")]
        public TimeBasedElement TimeBased
        {
            get
            {
                return (TimeBasedElement)this["time-based"];
            }
            set
            { this["time-based"] = value; }
        }
        
        [ConfigurationProperty("anchor")]
        public AnchorElement Anchor
        {
            get
            {
                return (AnchorElement)this["anchor"];
            }
            set
            { this["anchor"] = value; }
        }

        [ConfigurationProperty("info")]
        public InfoElement Info
        {
            get
            {
                return (InfoElement)this["info"];
            }
            set
            { this["info"] = value; }
        }

        [ConfigurationProperty("link")]
        public LinkElement Link
        {
            get
            {
                return (LinkElement)this["link"];
            }
            set
            { this["link"] = value; }
        }
    }
}