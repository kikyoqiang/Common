using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    #region PermissionInfo
    /// <summary>
    /// 权限类
    /// </summary>
    public class PermissionInfo : IPermission
    {
        public string FunctionName { get; set; }

        public int PermissionID { get; set; }

        public PermissionInfo() : this("", 0)
        {

        }

        public PermissionInfo(string pFunctionName, int pPermissionID)
        {
            this.FunctionName = pFunctionName;
            this.PermissionID = pPermissionID;
        }

        public bool CanOutPut()
        {
            return (this.PermissionID & (int)PermissionTypes.OutPut) == (int)PermissionTypes.OutPut;
        }

        public bool CanQuery()
        {
            return (this.PermissionID & (int)PermissionTypes.Query) == (int)PermissionTypes.Query;
        }

        public bool CanCreate()
        {
            return (this.PermissionID & (int)PermissionTypes.Create) == (int)PermissionTypes.Create;
        }

        public bool CanModify()
        {
            return (this.PermissionID & (int)PermissionTypes.Modify) == (int)PermissionTypes.Modify;
        }

        public bool CanDelete()
        {
            return (this.PermissionID & (int)PermissionTypes.Delete) == (int)PermissionTypes.Delete;
        }

        public bool CanSave()
        {
            return CanCreate() || CanModify();
        }

        public bool HasPermission()
        {
            return CanQuery() || CanCreate() || CanModify() || CanDelete();
        }
    }
    #endregion

    #region 权限接口
    /// <summary>
    /// 权限接口
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// 是否有导出权限
        /// </summary>
        /// <returns></returns>
        bool CanOutPut();
        /// <summary>
        /// 是否有查询权限
        /// </summary>
        /// <returns></returns>
        bool CanQuery();
        /// <summary>
        /// 是否有添加权限
        /// </summary>
        /// <returns></returns>
        bool CanCreate();
        /// <summary>
        /// 是否有修改权限
        /// </summary>
        /// <returns></returns>
        bool CanModify();
        /// <summary>
        /// 是否有删除权限
        /// </summary>
        /// <returns></returns>
        bool CanDelete();
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <returns></returns>
        bool CanSave();
        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <returns></returns>
        bool HasPermission();
    }
    #endregion

    #region 用户权限枚举
    /// <summary>
    /// 用户权限枚举
    /// </summary>
    public enum PermissionTypes : sbyte
    {
        /// <summary>
        /// 空权限
        /// </summary>
        None = 0,
        /// <summary>
        /// 查询
        /// </summary>
        Query = 1,
        /// <summary>
        /// 添加
        /// </summary>
        Create = 2,
        /// <summary>
        /// 修改
        /// </summary>
        Modify = 4,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 8,
        /// <summary>
        /// 输出权限，打印和导出都用这个
        /// </summary>
        OutPut = 16,
        /// <summary>
        /// 所有权限
        /// </summary>
        All = Query | Create | Modify | Delete | OutPut,
        /// <summary>
        /// 保存
        /// </summary>
        Save = Create | Modify
    }
    #endregion
}
