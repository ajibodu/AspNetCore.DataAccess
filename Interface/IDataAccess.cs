using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAccess.Interface
{
    public interface IDataAccess<RespObj>
    {
        List<RespObj> ExecuteReader(string query, CommandType commandType = CommandType.Text, bool closeConnection = true);
        int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, bool closeConnection = true);
        RespObj ExecuteScalar(string query, CommandType commandType = CommandType.Text, bool closeConnection = true);
    }
}
