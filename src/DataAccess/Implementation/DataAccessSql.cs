using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Implementation
{
    public class DataAccessSql : IDataAccess
    {
        public SqlCommand Comm;
        private readonly SqlConnection _conn;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        internal DataAccessSql(string connection)
        {
            _conn = new SqlConnection(connection);
            Comm = new SqlCommand();
            Comm.CommandTimeout = 0;
            Comm.Connection = _conn;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="closeConnection"></param>
        /// <returns></returns>
        public List<TRespObj> ExecuteReader<TRespObj>(string query, CommandType commandType = CommandType.Text, bool closeConnection = true)
        {
            Comm.CommandText = query;
            Comm.CommandType = commandType;
            var resultTable = new DataTable();
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                var adapter = new SqlDataAdapter(Comm);
                adapter.Fill(resultTable);

                var returnObject = SharedUtil.ConvertDataTable<TRespObj>(resultTable);
                return returnObject;
            }
            finally
            {
                if (closeConnection)
                    CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="closeConnection"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, bool closeConnection = true)
        {
            Comm.CommandText = query.ToString();
            Comm.CommandType = commandType;
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                return Comm.ExecuteNonQuery();
            }
            finally
            {
                if (closeConnection)
                    CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="closeConnection"></param>
        /// <returns></returns>
        public TRespObj ExecuteScalar<TRespObj>(string query, CommandType commandType = CommandType.Text, bool closeConnection = true)
        {
            Comm.CommandText = query;
            Comm.CommandType = commandType;
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                return (TRespObj)Comm.ExecuteScalar();
            }
            finally
            {
                if (closeConnection)
                    CloseConnection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseConnection()
        {
            Comm = new SqlCommand();
            Comm.Dispose();
            if (_conn.State != ConnectionState.Closed)
                _conn.Close();
        }

    }
}
