using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YiLi_Library.DTO
{
    public class BookDTO
    {
        /// <summary>
        /// 增加一本书的列表
        /// </summary>
        public class CreaTBookListDTO
        {
           /// <summary>
           /// 标题
           /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 副标题
            /// </summary>
            public string Abstract { get; set; }
            /// <summary>
            /// 作者
            /// </summary>
            public string Author { get; set; }
            /// <summary>
            /// 封面图
            /// </summary>
            public string CoverIMG { get; set; }
            /// <summary>
            /// 发布时间
            /// </summary>
            public System.DateTime AddTime { get; set; }
            /// <summary>
            /// 图书类型
            /// </summary>
            public int BookType { get; set; }
            /// <summary>
            /// 状态 1.正常2.下架3.删除
            /// </summary>
            public byte State { get; set; }
            /// <summary>
            /// 置顶时间
            /// </summary>
            public Nullable<System.DateTime> TopTime { get; set; }
            /// <summary>
            /// 是否推荐
            /// </summary>
            public Nullable<bool> Recommend { get; set; }
            ///// <summary>
            ///// 阅读量
            ///// </summary>
            //public Nullable<int> ClickNum { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get; set; }
           
        }
        /// <summary>
        /// 更新一本书的列表
        /// </summary>
        public class UpdateBookListDTO
        {
            /// <summary>
            /// bookID
            /// </summary>
            public int BookID { get; set; }
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 副标题
            /// </summary>
            public string Abstract { get; set; }
            /// <summary>
            /// 作者
            /// </summary>
            public string Author { get; set; }
            /// <summary>
            /// 封面图
            /// </summary>
            public string CoverIMG { get; set; }
            /// <summary>
            /// 发布时间
            /// </summary>
            public System.DateTime AddTime { get; set; }
            /// <summary>
            /// 图书类型
            /// </summary>
            public int BookType { get; set; }
            /// <summary>
            /// 状态 1.正常2.下架3.删除
            /// </summary>
            public byte State { get; set; }
            /// <summary>
            /// 置顶时间
            /// </summary>
            public Nullable<System.DateTime> TopTime { get; set; }
            /// <summary>
            /// 是否推荐
            /// </summary>
            public Nullable<bool> Recommend { get; set; }
            ///// <summary>
            ///// 阅读量
            ///// </summary>
            //public Nullable<int> ClickNum { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get; set; }

        }

    }
}