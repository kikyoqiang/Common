using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary> 连接Oracle数据库 32位可用 </summary>
    [Obsolete]
    public class OracleHelper
    {
        #region Ready
        public string ConnectString { get; set; }
        /// <summary> 服务名 </summary>
        public string DbServer { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public bool DbStatus { get; set; }
        private static OracleConnection oracleConnection;
        private static object locker = new object();
        private static OracleHelper _Instance;
        private OracleTransaction transaction;
        private OracleCommand command;
        private OracleHelper() { }
        public static OracleHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locker)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new OracleHelper();
                        }
                    }
                }
                return _Instance;
            }
        }
        #endregion

        #region ExecuteQuery    执行查询

        public DataTable ExecuteQuery(string sql)
        {
            return ExecuteQuery(GetConnect(), sql);
        }

        public DataTable ExecuteQuery(OracleConnection OracleConnection, string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                lock (locker)
                {
                    OracleCommand cmd = OracleConnection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        #endregion

        #region ExecuteNonQuery    执行sql

        public int ExecuteNonQuery(string sql, OracleParameter[] parameters = null)
        {
            oracleConnection = GetConnect();
            if (transaction != null)
                transaction.Rollback();
            return ExecuteNonQuery(oracleConnection, null, sql, parameters);
        }

        public int ExecuteNonQuery_Transaction(string sql, OracleParameter[] parameters = null)
        {
            oracleConnection = GetConnect();
            return ExecuteNonQuery(oracleConnection, transaction, sql, parameters);
        }

        public int ExecuteNonQuery(OracleConnection conn, string sql, OracleParameter[] parameters = null)
        {
            return ExecuteNonQuery(conn, null, sql, parameters);
        }

        public int ExecuteNonQuery(OracleConnection conn, OracleTransaction transaction, string sql, OracleParameter[] parameters)
        {
            command = new OracleCommand();
            command.Connection = conn;
            command.CommandText = sql;
            if (transaction != null)
            {
                if (transaction.Connection == null)
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }
            if (parameters != null)
            {
                AddParameters(command, parameters);
            }
            command.CommandType = CommandType.StoredProcedure;
            return command.ExecuteNonQuery();
        }

        #endregion

        #region AddParameters    加参数
        private static void AddParameters(OracleCommand command, OracleParameter[] parameters)
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
        #endregion

        #region GetConnectString    连接字符串
        private string GetConnectString()
        {
            return string.Format("Data Source ={0};user={1};password={2}", DbServer, DbUser, DbPassword);
        }
        #endregion

        #region GetConnect    连接获取
        public OracleConnection GetConnect()
        {
            if (oracleConnection == null)
            {
                oracleConnection = new OracleConnection();
                oracleConnection.ConnectionString = GetConnectString();
            }
            if (oracleConnection.State != ConnectionState.Open)
            {
                oracleConnection.ConnectionString = GetConnectString();
                oracleConnection.Open();
            }
            return oracleConnection;
        }
        #endregion

        #region ConnectStatusTest    连接测试
        public bool ConnectStatusTest()
        {
            DbConnection conn = null;
            try
            {
                conn = this.GetConnect();
                DbStatus = conn.State == ConnectionState.Open;
            }
            catch (Exception e)
            {
                LogHelper.Instance.WriteError("检测数据库是否能够连接", e);
                DbStatus = false;
            }
            finally
            {
                conn?.Close();
            }
            return DbStatus;
        }
        #endregion

        #region Transaction    事务
        public void BeginTransaction()
        {
            oracleConnection = GetConnect();
            transaction = oracleConnection.BeginTransaction(); ;
        }

        public void Commit()
        {
            if (transaction != null)
                transaction.Commit();
        }

        public void Rollback()
        {
            if (transaction != null)
                transaction.Rollback();
        }
        #endregion

        #region Init    
        /// <summary> 初始化参数 dbServer 服务名 </summary>
        public void Init(string dbServer, string dbUser, string dbPassword)
        {
            this.DbServer = dbServer;
            this.DbUser = dbUser;
            this.DbPassword = dbPassword;
        }
        #endregion
    }
}
