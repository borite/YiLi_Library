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
        /// <summary>
        /// 增加一级章节信息
        /// </summary>
        public class CreatBooKinfoDTO
        {

            /// <summary>
            /// 书籍ID 
            /// </summary>
            public int BooklD { get; set; }
            /// <summary>
            ///一级章节排序
            /// </summary>
            public int SectionOrder { get; set; }
            /// <summary>
            /// 一级章节名字
            /// </summary>
            public string SectionContent { get; set; }

           

    
         
            /// <summary>
            /// 后台备注
            /// </summary>
            public string Remark { get; set; }
        }
        /// <summary>
        /// 更新一级章节信息
        /// </summary>
        public class UpdateSection
        {


            //public int ID { get; set; }
            ///// <summary>
            ///// 书籍ID 
            ///// </summary>
            //public int BooklD { get; set; }
            /// <summary>
            ///一级章节排序
            /// </summary>
            public int SectionOrder { get; set; }

            /// <summary>
            /// 章节ID
            /// </summary>
            public int SectionID { get; set; }


            /// <summary>
            /// 一级章节内容
            /// </summary>
            public string SectionContent { get; set; }
     


            /// <summary>
            /// 后台备注
            /// </summary>
            public string Remark { get; set; }
        }
        /// <summary>
        /// 增加二级章节及文章
        /// </summary>

        public class CreatSubTitleDTO
        {
            /// <summary>
            /// 一章节ID
            /// </summary>
            public int SectionID { get; set; }
            /// <summary>
            /// 二级章节排序
            /// </summary>
            public int SubSectionOrder { get; set; }
            /// <summary>
            /// 二级章节名字
            /// </summary>
            public string SubSectionContent { get; set; }
            /// <summary>
            /// 文章正文
            /// </summary>
            public string Article { get; set; }
        
        }

        /// <summary>
        /// 更新二级章节信息
        /// </summary>
        public class UpdateSubSection
        {

           
            /// <summary>
            /// 二级章节ID
            /// </summary>
            public int SubSectionID { get; set; }
            /// <summary>
            /// 二级章节排序
            /// </summary>
            public int SubSectionOrder { get; set; }
            /// <summary>
            /// 二级章节内容
            /// </summary>
            public string SubSectionContent { get; set; }
            /// <summary>
            /// 文章正文
            /// </summary>
            public string Article { get; set; }



            /// <summary>
            /// 后台备注
            /// </summary>
            public string Remark { get; set; }
        }
        /// <summary>
        /// 种类
        /// </summary>

        public class CreatBookTypeDTO
        {
            /// <summary>
            /// 种类名字
            /// </summary>
            public string TypeName { get; set; }
          


        }
        /// <summary>
        /// 更新书类型种类
        /// </summary>

        public class UpdateBookTypeDTO
        {
            /// <summary>
            /// 种类ID
            /// </summary>
            public int ID { get; set; }
            /// <summary>
            /// 种类名字
            /// </summary>
            public string TypeName { get; set; }



        }


        /// <summary>
        /// 更新某本书的历史信息
        /// </summary>
        public class UpdateUserHistory
        {


            public string OpenID { get; set; }
            /// <summary>
            /// 书籍的ID
            /// </summary>
            public int BookID { get; set; }
            /// <summary>
            /// 章节ID
            /// </summary>
            public  int ReadSection { get; set; }
            /// <summary>
            /// 阅读的章节的时间记录 
            /// </summary>
            public byte[] ReadTime { get; set; }
       

        }

    }
}