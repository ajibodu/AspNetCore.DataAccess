using AdoNetCore.AseClient;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Implementation
{
    public class DataAccessSybase : IDataAccess
    {
        public AseCommand Comm;
        private readonly AseConnection _conn;
        private readonly SharedUtil _util;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="connect"></param>
        internal DataAccessSybase(string connection)
        {
            _conn = new AseConnection(connection);
            Comm = new AseCommand();
            Comm.CommandTimeout = 0;
            Comm.Connection = _conn;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _util = new SharedUtil();
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
                var adapter = new AseDataAdapter(Comm);
                adapter.Fill(resultTable);

                var returnObject = _util.convertDataTable<TRespObj>(resultTable);
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
            Comm.CommandText = query;
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
            Comm = new AseCommand();
            Comm.Dispose();
            if (_conn.State != ConnectionState.Closed)
                _conn.Close();
        }
        
    }
}
