﻿using Database.Helper;
using Database.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Database.Accessor
{
    /// <summary>
    /// 数据适配器-数据库存取类
    /// </summary>
    internal class SqlAdapterAccessor : ISqlAdapterAccessor
    {
        public Tuple<bool, object> Get(SqlCommand command)
        {
            try
            {
                using (var adapter = new SqlDataAdapter(command))
                {
                    var tb = new DataTable();
                    adapter.Fill(tb);
                    return new Tuple<bool, object>(true, tb);
                }
            }
            catch(Exception e)
            {
                return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
            }
            
        }
        public Tuple<bool, object> Set(SqlCommand command, DataSet dataSet)
        {
            try
            {
                using(var adapter = new SqlDataAdapter(command))
                {
                    return new Tuple<bool, object>(true, adapter.Update(dataSet));
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, object>(false, new ExceptionMessage(e).ExMessage);
            }
        }
    }
}
