﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Interface
{
    internal interface IServiceEventHandle<T, R>
    {
        R OnActiveEvent(T t);
    }
}
