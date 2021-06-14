using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 商品分类添加
    /// </summary>
    public class CategoryAdd
    {
        /// <summary>
        /// 商品分类名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 商品分类更新参数
    /// </summary>
    public class CategoryUpdate
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态0正常 1禁用
        /// </summary>
        public int State { get; set; }
    }
}
