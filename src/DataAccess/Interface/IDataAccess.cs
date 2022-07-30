using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace DataAccess.Interface
{
    public interface IDataAccess
    {
        List<RespObj> ExecuteReader<RespObj>(string query, CommandType commandType = CommandType.Text, bool closeConnection = true);
        int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, bool closeConnection = true);
        RespObj ExecuteScalar<RespObj>(string query, CommandType commandType = CommandType.Text, bool closeConnection = true);
    }
}
