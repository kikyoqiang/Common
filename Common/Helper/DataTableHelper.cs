using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Common
{
    public class DataTableHelper
    {
        public static DataTable ToDataTable<T>(List<T> entitys)
        {
            if (entitys == null || entitys.Count < 1)
                return new DataTable();

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable("dt");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                string name = entityProperties[i].Name;
                if (name.Contains("_"))
                {
                    name = name.Substring(1);
                }
                dt.Columns.Add(name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
    }
}
