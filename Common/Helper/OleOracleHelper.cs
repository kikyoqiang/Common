﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    public sealed class OleOracleHelper
    {

        private static Object lockObj = new object();
        private static Object lockHISObj = new object();
        //单实例对象
        private static OleOracleHelper dataManage = null;
        private static OleOracleHelper dataManageHIS = null;
        private static DbConnection oracleConnection = null;
        private static readonly object locker = new object();
        /// <summary>
        /// 标识当前数据库是否连接是否能成功
        /// </summary>
        private static bool isConnected;

        #region 属性
        //dbServer_
        private string dbServer_;
        public string DbServer
        {
            get { return dbServer_; }
            set { dbServer_ = value; }
        }
        //dbName
        private string dbName_;
        public string DbName
        {
            get { return dbName_; }
            set { dbName_ = value; }
        }
        //dbusername
        private string dbUser_;
        public string DbUser
        {
            get { return dbUser_; }
            set { dbUser_ = value; }
        }
        //dbpassword
        private string dbPassword_;
        public string DbPassword
        {
            get { return dbPassword_; }
            set { dbPassword_ = value; }
        }

        /// <summary>
        /// 数据库连接状态属性
        /// </summary>
        public static bool Db_Status
        {
            set { isConnected = value; }
            get { return isConnected; }
        }
        #endregion

        private OleOracleHelper()
        {
        }

        public static OleOracleHelper Instance()
        {
            if (dataManage == null)
            {
                lock (lockObj)
                {
                    dataManage = new OleOracleHelper();
                }
            }
            return dataManage;
        }

        public static OleOracleHelper HISInstance()
        {
            if (dataManageHIS == null)
            {
                lock (lockHISObj)
                {
                    dataManageHIS = new OleOracleHelper();
                }
            }
            return dataManageHIS;
        }

        #region 数据库连接字符串
        ///<summary>
        ///传入数据库连接字符串 数据库ip，数据库名称，数据库用户名, 密码
        /// </summary>
        /// <returns>无</returns>
        public void Init(string dbServer_, string dbName_, string dbUser_, string dbPassword_)
        {
            this.DbServer = dbServer_;
            this.DbName = dbName_;
            this.DbUser = dbUser_;
            this.DbPassword = dbPassword_;
        }

        private string GetOracleConnectString()
        {
            //设置数据库连接字符串         
            return string.Format("Provider=MSDAORA.1;Data Source ={0};user ID={1};password={2}", DbServer, DbUser, DbPassword);
        }
        #endregion

        #region 数据连接取得
        /// <summary>
        /// 数据连接取得
        /// </summary>
        /// <returns>DbConnection</returns>
        public DbConnection GetOraConnect()
        {
            ////Connection
            //DbConnection dbConnection = null;
            try
            {
                if (oracleConnection == null)
                {
                    oracleConnection = new OleDbConnection();
                    //连接串
                    oracleConnection.ConnectionString = GetOracleConnectString();

                }
                if (oracleConnection.State != ConnectionState.Open)
                {
                    oracleConnection.ConnectionString = GetOracleConnectString();
                    oracleConnection.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return oracleConnection;
        }
        #endregion

        #region 检测数据库是否能够连接
        /// <summary>
        /// 检测数据库是否能够连接
        /// </summary>
        /// <returns></returns>
        public bool ConnectStatusTest()
        {
            DbConnection conn = this.GetOraConnect();
            try
            {
                // conn.Open();
                bool ret = (conn.State == ConnectionState.Open ? true : false);
                Db_Status = ret;
                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Db_Status = false;
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region CloseDb
        /// <summary>
        /// DB数据连接关闭
        /// </summary>
        /// <param name="dbConnection">dbConnection</param>
        public void CloseDb(DbConnection dbConnection)
        {

            try
            {
                ////DB关闭
                if (dbConnection != null && dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ExecQueryTable 返回结果集 SQL文
        /// <summary>
        /// 返回结果集
        /// <param name="strSQL">SQL文</param>
        /// <returns>检索的结果集</returns>
        /// </summary>
        public DataTable ExecuteSqlDataTable(SqlConnection connect, SqlTransaction transaction, string strSQL)
        {
            lock (locker)
            {
                // Create a command and prepare it for execution
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(transaction, cmd, connect, strSQL, null);
                cmd.Connection = connect;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }

        }
        #endregion

        #region ExecuteSqlDataTableProcedure 返回结果集 存储过程 2013-11-19
        /// <summary>
        /// 返回结果集
        /// <param name="strSQL">存储过程</param>
        /// <returns>检索的结果集</returns>
        /// </summary>
        public DataTable ExecuteSqlDataTableProcedure(SqlConnection connect, SqlTransaction transaction, string strSQL, SqlParameter[] commandParameters)
        {
            // Create a command and prepare it for execution
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            PrepareCommandStoredProcedure(transaction, cmd, connect, strSQL, commandParameters);
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(dt);
            }
            return dt;
        }
        #endregion

        #region ExecuteNonQuery 返回更新件数
        /// <summary>
        /// 返回更新件数
        /// <param name="commandText">SQL文</param>
        /// <returns>更新件数</returns>
        /// </summary>
        public int ExecuteSqlNonQuery(SqlConnection connect, SqlTransaction transaction, string strSQL, SqlParameter[] commandParameters)
        {
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(transaction, cmd, connect, strSQL, commandParameters);

            // Finally, execute the command
            return cmd.ExecuteNonQuery();
        }
        #endregion

        #region ExecuteSqlNonQueryProcedure 返回更新件数 存储过程2013-11-19
        /// <summary>
        /// 返回更新件数
        /// <param name="commandText">存储过程</param>
        /// <returns>更新件数</returns>
        /// </summary>
        public int ExecuteSqlNonQueryProcedure(SqlConnection connect, SqlTransaction transaction, string strSQL, SqlParameter[] commandParameters)
        {
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            PrepareCommandStoredProcedure(transaction, cmd, connect, strSQL, commandParameters);

            // Finally, execute the command
            return cmd.ExecuteNonQuery();
        }
        #endregion

        #region PrepareCommand　参数设定
        /// <summary>
        /// 参数设定
        /// <param name="command">命令</param>
        /// <param name="strSQL">SQL文</param>
        /// </summary>
        private void PrepareCommand(SqlTransaction transaction, SqlCommand command, SqlConnection connect, string strSQL, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (strSQL == null || strSQL.Length == 0) throw new ArgumentNullException("strSQL");

            // Associate the connection with the command
            command.Connection = connect;

            // Set the command text (SQL statement)
            command.CommandText = strSQL;

            if (transaction != null)
            {

                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            // Set the command type
            command.CommandType = CommandType.Text;

            return;
        }

        /// <summary>
        /// 参数设定2013-11-19
        /// <param name="command">命令</param>
        /// <param name="strSQL">存储过程</param>
        /// </summary>
        private void PrepareCommandStoredProcedure(SqlTransaction transaction, SqlCommand command, SqlConnection connect, string strSQL, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (strSQL == null || strSQL.Length == 0) throw new ArgumentNullException("strSQL");

            // Associate the connection with the command
            command.Connection = connect;

            // Set the command text (SQL statement)
            command.CommandText = strSQL;

            if (transaction != null)
            {

                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            // Set the command type
            command.CommandType = CommandType.StoredProcedure;

            return;
        }
        /// <summary>
        /// This method is used to attach array of SqlParameters to a SqlCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion

        #region DelSingleQuotes 删除sql文中'

        /// <summary>
        /// 删除sql文中'
        /// </summary>
        public string DelSingleQuotes(string strSQL)
        {
            int intIndex = 0;

            string strWorkChar = "";

            StringBuilder sbSQLBuffer = new StringBuilder();

            string strTampStr = strSQL;

            for (intIndex = 1; intIndex <= strTampStr.Length; intIndex++)
            {

                strWorkChar = strTampStr.Substring(intIndex - 1, 1);

                sbSQLBuffer.Append(strWorkChar);

                if (strWorkChar.Equals("'"))
                {
                    sbSQLBuffer.Append("'");
                }
            }
            return sbSQLBuffer.ToString();
        }

        #endregion

        #region ExecDataSet 返回结果集DataSet
        /// <summary>
        /// 返回结果集
        /// <param name="strSQL">SQL文</param>
        /// <returns>检索的结果集</returns>
        /// </summary>
        public DataSet ExecDataSet(OleDbConnection OracleConnection, string strSQL)
        {
            DataSet ds = new DataSet();
            try
            {
                lock (locker)
                {
                    OleDbCommand cmd = OracleConnection.CreateCommand();
                    cmd.CommandText = strSQL;
                    cmd.CommandType = CommandType.Text;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        #endregion



        #region ole单例
        /// <summary> ole单例 私有 </summary>
        private static OleOracleHelper dataManageOle;
        /// <summary> ole数据库连接 </summary>
        private static OleDbConnection oleDbConnection;
        private static object lockerOle = new object();

        #region ole单例
        /// <summary> ole单例 </summary>
        public static OleOracleHelper InstanceOle
        {
            get
            {
                if (dataManageOle == null)
                {
                    lock (lockerOle)
                    {
                        if (dataManageOle == null)
                        {
                            dataManageOle = new OleOracleHelper();
                        }
                    }
                }
                return dataManageOle;
            }
        }
        #endregion

        #region 查询sql
        /// <summary> 查询sql </summary>
        public DataTable ExecuteQueryOle(string sql)
        {
            return ExecuteQueryOle(sql, null);
        }
        /// <summary> 查询sql </summary>
        public DataTable ExecuteQueryOle(string sql, params OleDbParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OleDbConnection conn = GetOraConnectOle())
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    if (parameters != null && parameters.Count() > 0)
                        cmd.Parameters.AddRange(parameters);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(string.Format("异常SQL语句：{0}\r\n  异常信息：{1}", sql, ex.ToString()));
                throw;
            }
            return dt;
        }
        #endregion

        #region 执行sql
        /// <summary> 执行sql </summary>
        public int ExecuteNonQueryOle(string sql)
        {
            return ExecuteNonQueryOle(sql, null);
        }
        /// <summary> 执行sql </summary>
        public int ExecuteNonQueryOle(string sql, params OleDbParameter[] parameters)
        {
            try
            {
                using (OleDbConnection conn = GetOraConnectOle())
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    if (parameters != null && parameters.Count() > 0)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(string.Format("异常SQL语句：{0}\r\n  异常信息：{1}", sql, ex.ToString()));
                throw;
            }
        }
        #endregion

        #region 获取数据库连接
        /// <summary> 获取old数据库连接 </summary>
        private OleDbConnection GetOraConnectOle()
        {
            if (oleDbConnection == null)
            {
                oleDbConnection = new OleDbConnection();
                oleDbConnection.ConnectionString = GetOracleConnectString();
            }
            if (oleDbConnection.State != ConnectionState.Open)
            {
                oleDbConnection.ConnectionString = GetOracleConnectString();
                oleDbConnection.Open();
            }
            return oleDbConnection;
        }
        #endregion

        #region ole连接测试
        /// <summary> ole连接测试 </summary>
        public bool ConnectStatusTestOle()
        {
            DbConnection conn = this.GetOraConnectOle();
            try
            {
                return conn.State == ConnectionState.Open;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError(ex.ToString());
                return false;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        #endregion

        #endregion

        #region 执行Sql语句 事务
        private OleDbTransaction oleDbTransactionIFM;
        private OleDbConnection oleDbConnectionIFM;
        public void BeginTransactionIFM()
        {
            if (oleDbConnectionIFM != null)
            {
                oleDbConnectionIFM.Dispose();
                oleDbConnectionIFM.Close();
                oleDbConnectionIFM = null;
            }

            oleDbConnectionIFM = new OleDbConnection(GetOracleConnectString());
            oleDbConnectionIFM.Open();
            oleDbTransactionIFM = oleDbConnectionIFM.BeginTransaction();
        }
        public void CommitTransactionIFM()
        {
            if (oleDbTransactionIFM != null)
            {
                oleDbTransactionIFM.Commit();
            }

            if (oleDbConnectionIFM != null)
            {
                oleDbConnectionIFM.Dispose();
                oleDbConnectionIFM.Close();
                oleDbConnectionIFM = null;
            }
        }
        public void RollbackTransactionIFM()
        {
            if (oleDbTransactionIFM != null)
            {
                oleDbTransactionIFM.Rollback();
            }

            if (oleDbConnectionIFM != null)
            {
                oleDbConnectionIFM.Dispose();
                oleDbConnectionIFM.Close();
                oleDbConnectionIFM = null;
            }
        }
        /// <summary> 执行Sql语句 事务 </summary>
        public int ExecuteSqlWithTran(string sql)
        {
            return ExecuteSqlWithTran(sql, null);
        }
        /// <summary> 执行Sql语句 事务 </summary>
        public int ExecuteSqlWithTran(string sql, params OleDbParameter[] parameters)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                try
                {
                    if (oleDbConnectionIFM.State != ConnectionState.Open)
                        oleDbConnectionIFM.Open();

                    if (parameters != null && parameters.Count() > 0)
                        cmd.Parameters.AddRange(parameters);

                    cmd.Connection = oleDbConnectionIFM;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.Transaction = oleDbTransactionIFM;

                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogHelper.Instance.WriteError(string.Format("异常SQL语句：{0}\r\n  异常信息：{1}", sql, ex.ToString()));
                    throw;
                }
            }
        }
        #endregion
    }
}