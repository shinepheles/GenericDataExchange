﻿using Core;
using Core.Interface;
using Database;
using Database.Interface;
using Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.Entity;

namespace WCFService
{
    public static class WCFServiceInitialization
    {
        public static void Start(string[] codes)
        {
            var kernel = new IoCKernelImpl();
            kernel.Bind<IIoCKernel>().To<IoCKernelImpl>();
            var core = kernel.Resolve<CoreInitialization>();
            //初始化核心入列事件依赖
            core.Initialization();
            //初始化核心无返回值消息处理事件依赖
            core.Initialization<IGenericEventArg<IFactoryContext>>();
            //初始化核心有返回值消息处理事件依赖
            core.Initialization<IGenericEventArg<IFactoryContext>, IGenericResult>();
            //初始化数据库依赖
            kernel.Resolve<DatabaseInitialization>().Initialization();
            //初始化队列器依赖，服务启动
            kernel.Resolve<Dipper>().DipBind();
            //启动队列器，启动数据库配件
            kernel.Resolve<QueueInitialization>().PeristalticStart(new PeristalticConfiguration(codes));
        }
    }
}