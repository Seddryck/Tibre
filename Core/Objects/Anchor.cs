﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tibre.Core.Objects
{
    public class Anchor : Table
    {        
        public SqlIdentity Identity { get; internal set; }
        public IList<SqlColumn> BusinessKey { get; internal set; }

        public override IEnumerable<SqlColumn> Columns
        {
            get
            {
                return Enumerable.Repeat(Identity, 1).Union(BusinessKey);
            }
        }
    }
}
