﻿using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace Database.Interface
{
    /// <summary>
    /// 数据库工厂类上下文接口，自动使用，不必理会
    /// </summary>
    public interface IBaseContext
    {
        string ConnectionString { get; set; }
        SqlConnection Connection { get; set; }
        SqlTransaction Transaction { get; set; }
        Tuple<bool, object> DbCommit();
        Tuple<bool, object> DbRollback();
    }
}
