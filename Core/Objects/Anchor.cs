﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tibre.Core.Objects
{
    public class Anchor : Table
    {        
        public TSqlIdentity Identity { get; internal set; }
        public IList<TSqlColumn> BusinessKey { get; internal set; }
    }
}
