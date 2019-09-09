﻿using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceConsole
{
    class EntryContext : IEntryContext
    {
        public ITimespan Timespan { get; set; }
        public int Count { get; set; }
    }
}
