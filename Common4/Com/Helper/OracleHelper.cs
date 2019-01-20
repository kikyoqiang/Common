using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common4
{
    public class OracleHelper
    {
        private static object lockObj = new object();
        private static OracleHelper dataManage = null;
        private static OracleConnection oracleConnection = null;
        private static readonly object locker = new object();
        private static bool isConnected;
        private OracleHelper() { }

        #region 属性
        //dbServer_
        private string dbServer_;
        public string DbServer
        {
            get { return dbServer_; }
            set { dbServer_ = value; }
        }
        //dbIp
        private string dbIp_;
        public string DbIp
        {
            get { return dbIp_; }
            set { dbIp_ = value; }
        }
        //dbPort
        private string dbPort_;
        public string DbPort
        {
            get { return dbPort_; }
            set { dbPort_ = value; }
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

        public static bool Db_Status
        {
            set { isConnected = value; }
            get { return isConnected; }
        }
        #endregion

        #region Instance
        public static OracleHelper Instance
        {
            get
            {
                if (dataManage == null)
                {
                    lock (lockObj)
                    {
                        if (dataManage == null)
                        {
                            dataManage = new OracleHelper();
                        }
                    }
                }
                return dataManage;
            }
        }
        #endregion

        #region 数据库连接字符串
        ///<summary> 传入数据库连接字符串 数据库ip，数据库名称，数据库用户名, 密码 </summary>
        public void Init(string dbServer_, string dbIp_, string DbPort_, string dbUser_, string dbPassword_)
        {
            this.DbServer = dbServer_;
            this.DbIp = dbIp_;
            this.DbPort = DbPort_;
            this.DbUser = dbUser_;
            this.DbPassword = dbPassword_;

        }
        private string GetOracleConnectString()
        {
            string format = "User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3})))(CONNECT_DATA=(SERVICE_NAME={4})))";
            string result = string.Format(format, DbUser, DbPassword, DbIp, DbPort, DbServer);
            return result;
        }

        //private string GetOracleConnectString_()
        //{
        //    string lsUserIdKey = "user id";
        //    string lsPasswordKey = "password";
        //    string lsKey = "EeiixxASDIFRNOYV1.0LockH";

        //    object loUserid = null;
        //    object loPassword = null;

        //    DbProviderFactory mDbProviderFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["OracleApplicationServices"].ProviderName);

        //    DbConnectionStringBuilder loDbConnectionStringBuilder = new DbConnectionStringBuilder();

        //    loDbConnectionStringBuilder.ConnectionString = ConfigurationManager.ConnectionStrings["OracleApplicationServices"].ConnectionString;

        //    if (loDbConnectionStringBuilder.TryGetValue(lsUserIdKey, out loUserid))
        //    {
        //        loDbConnectionStringBuilder.Add(lsUserIdKey, ComUtil.ComUtilTools.Decrypt3DES(loUserid.ToString(), lsKey));
        //    }

        //    if (loDbConnectionStringBuilder.TryGetValue(lsPasswordKey, out loPassword))
        //    {
        //        loDbConnectionStringBuilder.Add(lsPasswordKey, ComUtil.ComUtilTools.Decrypt3DES(loPassword.ToString(), lsKey));
        //    }

        //    string ConnString = loDbConnectionStringBuilder.ConnectionString;
        //    return ConnString;
        //}

        #endregion

        #region 数据连接取得
        public OracleConnection GetOraConnect()
        {
            try
            {
                if (oracleConnection == null)
                {
                    oracleConnection = new OracleConnection();

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
                throw;
            }

            return oracleConnection;
        }
        #endregion

        #region 检测数据库是否能够连接oracle不需要装客户端
        /// <summary> 检测数据库是否能够连接oracle不需要装客户端 </summary>
        public bool ConnOracleUnwantedClient()
        {
            OracleConnection conn = this.GetOraConnect();
            try
            {
                bool ret = conn.State == ConnectionState.Open;
                Db_Status = ret;
                return ret;
            }
            catch (OracleException ex)
            {
                //LogUtil.LogToFile(LogUtil.LogType.InfoLog, ex.Message);
                Db_Status = false;
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region ExecQuery    返回结果集DataSet
        public DataSet ExecuteQueryDataSet(string strSQL)
        {
            return ExecuteQuery(strSQL, null);
        }
        public DataTable ExecuteQuery(string strSQL)
        {
            return ExecuteQueryDataSet(strSQL).Tables[0];
        }
        public DataSet ExecuteQuery(string strSQL, DbParameter[] parameters)
        {
            OracleConnection OracleConnection = GetOraConnect();
            DataSet ds = new DataSet();
            lock (locker)
            {
                OracleCommand cmd = OracleConnection.CreateCommand();
                cmd.CommandText = strSQL;
                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    foreach (DbParameter Param in parameters)
                    {
                        if (Param == null)
                            continue;
                        if ((Param.Direction == ParameterDirection.InputOutput || Param.Direction == ParameterDirection.Input) && (Param.Value == null))
                        {
                            Param.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(Param);
                    }
                }

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
            }
            return ds;
        }
        #endregion

        #region ExecuteNonQuery    执行SQL 可选事务 参数
        public int ExecuteNonQuery(OracleConnection connect, string strSQL, OracleTransaction transaction = null, OracleParameter[] parameters = null)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connect;
            command.CommandText = strSQL;

            if (transaction != null)
            {
                if (transaction.Connection == null)
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            if (parameters != null)
            {
                foreach (OracleParameter Param in parameters)
                {
                    if (Param == null)
                        continue;
                    if ((Param.Direction == ParameterDirection.InputOutput || Param.Direction == ParameterDirection.Input) && (Param.Value == null))
                    {
                        Param.Value = DBNull.Value;
                    }
                    command.Parameters.Add(Param);
                }
            }

            int res = command.ExecuteNonQuery();

            return res;
        }
        #endregion

        #region CloseDb 
        public void CloseDb(OracleConnection dbConnection)
        {
            if (dbConnection != null && dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }
        #endregion





        #region /////

        #region 检测数据库是否能够连接 需要装客户端
        [Obsolete]
        private bool ConnectStatusTest()
        {
            OracleConnection conn = this.GetOraConnect();
            try
            {
                bool ret = conn.State == ConnectionState.Open;
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

        #endregion
    }
}
