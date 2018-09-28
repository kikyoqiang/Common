using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Common
{
    //public class OracleHelper
    //{
    //    private static Object lockObj = new object();
    //    //单实例对象
    //    private static OracleHelper dataManage = null;
    //    //private static DbConnection oracleConnection = null;
    //    private static OracleConnection oracleConnection = null;
    //    private static readonly object locker = new object();
    //    /// <summary>
    //    /// 标识当前数据库是否连接是否能成功
    //    /// </summary>
    //    private static bool isConnected;

    //    #region 属性
    //    //dbServer_
    //    private string dbServer_;
    //    public string DbServer
    //    {
    //        get { return dbServer_; }
    //        set { dbServer_ = value; }
    //    }
    //    //dbIp
    //    private string dbIp_;
    //    public string DbIp
    //    {
    //        get { return dbIp_; }
    //        set { dbIp_ = value; }
    //    }
    //    //dbPort
    //    private string dbPort_;
    //    public string DbPort
    //    {
    //        get { return dbPort_; }
    //        set { dbPort_ = value; }
    //    }
    //    //dbusername
    //    private string dbUser_;
    //    public string DbUser
    //    {
    //        get { return dbUser_; }
    //        set { dbUser_ = value; }
    //    }
    //    //dbpassword
    //    private string dbPassword_;
    //    public string DbPassword
    //    {
    //        get { return dbPassword_; }
    //        set { dbPassword_ = value; }
    //    }

    //    /// <summary>
    //    /// 数据库连接状态属性
    //    /// </summary>
    //    public static bool Db_Status
    //    {
    //        set { isConnected = value; }
    //        get { return isConnected; }
    //    }
    //    #endregion

    //    private OracleHelper()
    //    {
    //    }

    //    public static OracleHelper Instance()
    //    {
    //        if (dataManage == null)
    //        {
    //            lock (lockObj)
    //            {
    //                dataManage = new OracleHelper();
    //            }
    //        }
    //        return dataManage;
    //    }

    //    #region 数据库连接字符串
    //    ///<summary>
    //    ///传入数据库连接字符串 数据库ip，数据库名称，数据库用户名, 密码
    //    /// </summary>
    //    /// <returns>无</returns>
    //    public void Init(string dbServer_, string dbIp_, string DbPort_, string dbUser_, string dbPassword_)
    //    {
    //        this.DbServer = dbServer_;
    //        this.DbIp = dbIp_;
    //        this.DbPort = DbPort_;
    //        this.DbUser = dbUser_;
    //        this.DbPassword = dbPassword_;
    //    }

    //    private string GetOracleConnectString()
    //    {
    //        //设置数据库连接字符串
    //        return string.Format("User Id=" + this.DbUser + ";Password=" + this.DbPassword + ";Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + this.DbIp + ")(PORT=" + this.DbPort + ")))(CONNECT_DATA=(SERVICE_NAME=" + this.DbServer + ")))");
    //    }
    //    #endregion

    //    #region 数据连接取得
    //    /// <summary>
    //    /// 数据连接取得
    //    /// </summary>
    //    /// <returns>DbConnection</returns>
    //    public OracleConnection GetOraConnect()
    //    {
    //        ////Connection
    //        //DbConnection dbConnection = null;
    //        try
    //        {
    //            if (oracleConnection == null)
    //            {
    //                oracleConnection = new OracleConnection();
    //                //连接串
    //                oracleConnection.ConnectionString = GetOracleConnectString();
    //            }
    //            if (oracleConnection.State != ConnectionState.Open)
    //            {
    //                oracleConnection.ConnectionString = GetOracleConnectString();
    //                oracleConnection.Open();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            throw ex;
    //        }

    //        return oracleConnection;
    //    }
    //    #endregion

    //    /// <summary>
    //    /// 检测数据库是否能够连接
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool ConnectStatusTest()
    //    {
    //        OracleConnection conn = this.GetOraConnect();
    //        try
    //        {
    //            // conn.Open();
    //            bool ret = (conn.State == ConnectionState.Open ? true : false);
    //            Db_Status = ret;
    //            return ret;
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e.Message);
    //            Db_Status = false;
    //            return false;
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //    }

    //    /// <summary>
    //    /// 检测数据库是否能够连接oracle不需要装客户端
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool ConnOracleUnwantedClient()
    //    {
    //        OracleConnection conn = this.GetOraConnect();
    //        try
    //        {
    //            bool ret = (conn.State == ConnectionState.Open ? true : false);
    //            Db_Status = ret;
    //            return ret;
    //        }
    //        catch (OracleException ex)
    //        {
    //            Db_Status = false;
    //            return false;
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //    }

    //    #region CloseDb
    //    /// <summary>
    //    /// DB数据连接关闭
    //    /// </summary>
    //    /// <param name="dbConnection">dbConnection</param>
    //    public void CloseDb(OracleConnection dbConnection)
    //    {

    //        try
    //        {
    //            ////DB关闭
    //            if (dbConnection != null && dbConnection.State == System.Data.ConnectionState.Open)
    //            {
    //                dbConnection.Close();
    //                dbConnection.Dispose();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    #endregion

    //    #region ExecQueryTable 返回结果集 SQL文
    //    /// <summary>
    //    /// 返回结果集
    //    /// <param name="strSQL">SQL文</param>
    //    /// <returns>检索的结果集</returns>
    //    /// </summary>
    //    public DataTable ExecuteSqlDataTable(SqlConnection connect, SqlTransaction transaction, string strSQL)
    //    {
    //        lock (locker)
    //        {
    //            // Create a command and prepare it for execution
    //            SqlCommand cmd = new SqlCommand();
    //            PrepareCommand(transaction, cmd, connect, strSQL, null);
    //            cmd.Connection = connect;
    //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //            DataTable dt = new DataTable();
    //            sda.Fill(dt);
    //            return dt;
    //        }

    //    }
    //    #endregion

    //    #region ExecuteSqlDataTableProcedure 返回结果集 存储过程 2013-11-19
    //    /// <summary>
    //    /// 返回结果集
    //    /// <param name="strSQL">存储过程</param>
    //    /// <returns>检索的结果集</returns>
    //    /// </summary>
    //    public DataTable ExecuteSqlDataTableProcedure(SqlConnection connect, SqlTransaction transaction, string strSQL, SqlParameter[] commandParameters)
    //    {
    //        // Create a command and prepare it for execution
    //        DataTable dt = new DataTable();
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.Connection = connect;
    //        PrepareCommandStoredProcedure(transaction, cmd, connect, strSQL, commandParameters);
    //        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //        {
    //            sda.Fill(dt);
    //        }
    //        return dt;
    //    }
    //    #endregion

    //    #region ExecuteNonQuery 返回更新件数
    //    /// <summary>
    //    /// 返回更新件数
    //    /// <param name="commandText">SQL文</param>
    //    /// <returns>更新件数</returns>
    //    /// </summary>
    //    public int ExecuteSqlNonQuery(SqlConnection connect, SqlTransaction transaction, string strSQL, SqlParameter[] commandParameters)
    //    {
    //        // Create a command and prepare it for execution
    //        SqlCommand cmd = new SqlCommand();
    //        PrepareCommand(transaction, cmd, connect, strSQL, commandParameters);

    //        // Finally, execute the command
    //        return cmd.ExecuteNonQuery();
    //    }
    //    #endregion

    //    #region ExecuteSqlNonQueryProcedure 返回更新件数 存储过程2013-11-19
    //    /// <summary>
    //    /// 返回更新件数
    //    /// <param name="commandText">存储过程</param>
    //    /// <returns>更新件数</returns>
    //    /// </summary>
    //    public int ExecuteSqlNonQueryProcedure(SqlConnection connect, SqlTransaction transaction, string strSQL, SqlParameter[] commandParameters)
    //    {
    //        // Create a command and prepare it for execution
    //        SqlCommand cmd = new SqlCommand();
    //        PrepareCommandStoredProcedure(transaction, cmd, connect, strSQL, commandParameters);

    //        // Finally, execute the command
    //        return cmd.ExecuteNonQuery();
    //    }
    //    #endregion

    //    #region PrepareCommand　参数设定
    //    /// <summary>
    //    /// 参数设定
    //    /// <param name="command">命令</param>
    //    /// <param name="strSQL">SQL文</param>
    //    /// </summary>
    //    private void PrepareCommand(SqlTransaction transaction, SqlCommand command, SqlConnection connect, string strSQL, SqlParameter[] commandParameters)
    //    {
    //        if (command == null) throw new ArgumentNullException("command");
    //        if (strSQL == null || strSQL.Length == 0) throw new ArgumentNullException("strSQL");

    //        // Associate the connection with the command
    //        command.Connection = connect;

    //        // Set the command text (SQL statement)
    //        command.CommandText = strSQL;

    //        if (transaction != null)
    //        {

    //            if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
    //            command.Transaction = transaction;
    //        }

    //        // Attach the command parameters if they are provided
    //        if (commandParameters != null)
    //        {
    //            AttachParameters(command, commandParameters);
    //        }

    //        // Set the command type
    //        command.CommandType = CommandType.Text;

    //        return;
    //    }

    //    /// <summary>
    //    /// 参数设定2013-11-19
    //    /// <param name="command">命令</param>
    //    /// <param name="strSQL">存储过程</param>
    //    /// </summary>
    //    private void PrepareCommandStoredProcedure(SqlTransaction transaction, SqlCommand command, SqlConnection connect, string strSQL, SqlParameter[] commandParameters)
    //    {
    //        if (command == null) throw new ArgumentNullException("command");
    //        if (strSQL == null || strSQL.Length == 0) throw new ArgumentNullException("strSQL");

    //        // Associate the connection with the command
    //        command.Connection = connect;

    //        // Set the command text (SQL statement)
    //        command.CommandText = strSQL;

    //        if (transaction != null)
    //        {

    //            if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
    //            command.Transaction = transaction;
    //        }

    //        // Attach the command parameters if they are provided
    //        if (commandParameters != null)
    //        {
    //            AttachParameters(command, commandParameters);
    //        }

    //        // Set the command type
    //        command.CommandType = CommandType.StoredProcedure;

    //        return;
    //    }
    //    /// <summary>
    //    /// This method is used to attach array of SqlParameters to a SqlCommand.
    //    /// 
    //    /// This method will assign a value of DbNull to any parameter with a direction of
    //    /// InputOutput and a value of null.  
    //    /// 
    //    /// This behavior will prevent default values from being used, but
    //    /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
    //    /// where the user provided no input value.
    //    /// </summary>
    //    /// <param name="command">The command to which the parameters will be added</param>
    //    /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
    //    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    //    {
    //        if (command == null) throw new ArgumentNullException("command");
    //        if (commandParameters != null)
    //        {
    //            foreach (SqlParameter p in commandParameters)
    //            {
    //                if (p != null)
    //                {
    //                    // Check for derived output value with no value assigned
    //                    if ((p.Direction == ParameterDirection.InputOutput ||
    //                        p.Direction == ParameterDirection.Input) &&
    //                        (p.Value == null))
    //                    {
    //                        p.Value = DBNull.Value;
    //                    }
    //                    command.Parameters.Add(p);
    //                }
    //            }
    //        }
    //    }
    //    #endregion

    //    #region DelSingleQuotes 删除sql文中'

    //    /// <summary>
    //    /// 删除sql文中'
    //    /// </summary>
    //    public string DelSingleQuotes(string strSQL)
    //    {
    //        int intIndex = 0;

    //        string strWorkChar = "";

    //        StringBuilder sbSQLBuffer = new StringBuilder();

    //        string strTampStr = strSQL;

    //        for (intIndex = 1; intIndex <= strTampStr.Length; intIndex++)
    //        {

    //            strWorkChar = strTampStr.Substring(intIndex - 1, 1);

    //            sbSQLBuffer.Append(strWorkChar);

    //            if (strWorkChar.Equals("'"))
    //            {
    //                sbSQLBuffer.Append("'");
    //            }
    //        }
    //        return sbSQLBuffer.ToString();
    //    }

    //    #endregion

    //    #region ExecDataSet 返回结果集DataSet
    //    /// <summary>
    //    /// 返回结果集
    //    /// <param name="strSQL">SQL文</param>
    //    /// <returns>检索的结果集</returns>
    //    /// </summary>
    //    public DataSet ExecDataSet(OracleConnection OracleConnection, string strSQL)
    //    {
    //        DataSet ds = new DataSet();
    //        try
    //        {
    //            lock (locker)
    //            {
    //                OracleCommand cmd = OracleConnection.CreateCommand();
    //                cmd.CommandText = strSQL;
    //                cmd.CommandType = CommandType.Text;
    //                OracleDataAdapter da = new OracleDataAdapter(cmd);
    //                da.Fill(ds);
    //                return ds;
    //            }
    //        }
    //        catch (OracleException ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    #endregion

    //}
}