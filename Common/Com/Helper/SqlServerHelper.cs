using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    public class SqlServerHelper
    {
        #region Ready
        public string ConnectionString { get; set; }
        private static object locker = new object();
        private static SqlServerHelper instance;
        private SqlConnection conn;
        #endregion

        #region Instance
        public static SqlServerHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new SqlServerHelper();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region ExecuteQuery
        public DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            using (conn = GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        #endregion
        
        #region ExecuteQuery
        public DataTable ExecuteQuery(string sql, params SqlParameter[] parms)
        {
            DataTable dt = new DataTable();
            using (conn = GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parms);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(string sql)
        {
            using (conn = GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
        } 
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(string sql, params SqlParameter[] parms)
        {
            using (conn = GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parms);
                return cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region SqlServerHelper
        private SqlServerHelper() { }
        #endregion

        #region GetConnection
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        #endregion

        #region Init
        public void Init(string ip, string db, string user, string pwd)
        {
            ConnectionString = string.Format("server={0};database={1};user ID={2};password={3};", ip, db, user, pwd);
        }
        #endregion
    }
}
