using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Implementation
{
    public class DataAccessSql<RespObj> : IDataAccess<RespObj>
    {
        public SqlCommand comm;
        private SqlConnection _conn;
        private SharedUtil _util;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="connect"></param>
        public DataAccessSql(string connection, bool connect = false)
        {
            _conn = new SqlConnection(connection);
            comm = new SqlCommand();
            comm.CommandTimeout = 0;
            comm.Connection = _conn;
            
            if (connect)
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
            }
            _util = new SharedUtil();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="closeConnection"></param>
        /// <returns></returns>
        public List<RespObj> ExecuteReader(string query, CommandType commandType = CommandType.Text, bool closeConnection = true)
        {
            comm.CommandText = query.ToString();
            comm.CommandType = commandType;
            DataTable resultTable = new DataTable();
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                SqlDataAdapter Adapter = new SqlDataAdapter(comm);
                Adapter.Fill(resultTable);

                List<RespObj> ReturnObject = _util.convertDataTable<RespObj>(resultTable);
                return ReturnObject;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                comm = new SqlCommand();
                if (closeConnection)
                {
                    comm.Dispose();
                    if (_conn.State != ConnectionState.Closed)
                        _conn.Close();
                }
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
            comm.CommandText = query.ToString();
            comm.CommandType = commandType;
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                return comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                comm = new SqlCommand();
                if (closeConnection)
                {
                    comm.Dispose();
                    if (_conn.State != ConnectionState.Closed)
                        _conn.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="closeConnection"></param>
        /// <returns></returns>
        public RespObj ExecuteScalar(string query, CommandType commandType = CommandType.Text, bool closeConnection = true)
        {
            comm.CommandText = query.ToString();
            comm.CommandType = commandType;
            try
            {
                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                return (RespObj)comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                comm = new SqlCommand();
                if (closeConnection)
                {
                    comm.Dispose();
                    if (_conn.State != ConnectionState.Closed)
                        _conn.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseConnection()
        {
            comm.Dispose();
            if (_conn.State != ConnectionState.Closed)
                _conn.Close();
        }

    }
}
