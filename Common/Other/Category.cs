using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    //分类模型
    public class Category
    {
        public string CategoeyID { get; set; }
        public string CategoeyName { get; set; }
    }
    public static class CategoryManager
    {
        //这里只显示创建了三个分类作为示例，实际中AllCategories可以从数据源读取。
        public static readonly List<Category> AllCategories = new List<Category>
        {
            new Category(){ CategoeyID="001", CategoeyName="Nokia"},
            new Category(){ CategoeyID="002", CategoeyName="iPhone"},
            new Category(){ CategoeyID="003", CategoeyName="Anycall"}
        };
    }
}
