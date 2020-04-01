using ChinaAudio.Classes.CodeHeper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YiLi_Library.Entity;
using static ChinaAudio.Classes.CodeHeper.CodeNew;
using static YiLi_Library.Class.AliyunHelper;
using static YiLi_Library.Class.OSSConfig;
using static YiLi_Library.DTO.BookDTO;

namespace YiLi_Library.Controllers
{
    [RoutePrefix("api/Front")]
    public class FrontController : ApiController
    {
        YL_LibaryEntities YL = new YL_LibaryEntities();

        /// <summary>
        /// 增加书的类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost, Route("CreatBookType")]
        public IHttpActionResult CreatBookType(CreatBookTypeDTO obj)
        {
            //   var cc= YL.BookList.Where(a=>a.)

            BookType res = new BookType();
            res.TypeName = obj.TypeName;
            res.AddTime = DateTime.Now;
            YL.BookType.Add(res);
            YL.SaveChanges();
            return Content(HttpStatusCode.OK, CodeNew.Successful("新加图书类型成功", res));

        }

        /// <summary>
        /// 修改书的类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateBookType")]
        public IHttpActionResult UpdateBookType(UpdateBookTypeDTO obj)
        {
            var cc = YL.BookType.Where(a => a.ID == obj.ID).FirstOrDefault();
            cc.TypeName = obj.TypeName;

            YL.SaveChanges();
            return Content(HttpStatusCode.OK, CodeNew.Successful("更新图书类型成功", cc));
        }


        /// <summary>
        /// 查看书的类型
        /// </summary>
        /// <returns></returns>

        [HttpGet, Route("GetBookType")]
        public IHttpActionResult GetBookType()
        {

            var cc = YL.BookType.ToList();
            return Content(HttpStatusCode.OK, CodeNew.Successful("查看图书类型成功", cc));

        }

        /// <summary>
        /// 查看书的信息
        /// </summary>
        /// <param name="BookID">书籍ID</param>
        /// <returns></returns>
        [HttpGet, Route("GetBookList")]

        public IHttpActionResult GetBookList(int BookID)
        {
            var cc = YL.BookList.Include("SubBookSection").Where(a => a.BookID == BookID).Select(a => new
            {
                a.Title,
                a.Abstract,
                a.Author,
                a.AddTime,
                a.BookType,
                a.State,
                a.Recommend,
                a.ClickNum,
                a.Remark,
                BookSectionCount = a.BookSection.Count
            }).ToList();
            return Content(HttpStatusCode.OK, CodeNew.Successful("查看图书类型成功", cc));


        }


        /// <summary>
        /// 新加一本书
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost, Route("CreatBookList")]
        public IHttpActionResult CreatBookList(CreaTBookListDTO obj)
        {

            BookList res = new BookList();
            res.Title = obj.Title;
            res.Abstract = obj.Abstract;
            res.Author = obj.Author;
            res.AddTime = DateTime.Now;
            res.BookType = obj.BookType;
            res.State = obj.State;
            res.TopTime = obj.TopTime;
            res.Recommend = obj.Recommend;
            res.Remark = obj.Remark;
            res.ClickNum = 0;
            res.CoverIMG = obj.CoverIMG;

            YL.BookList.Add(res);
            YL.SaveChanges();
            return Content(HttpStatusCode.OK, CodeNew.Successful("图书新加目录成功", res));


        }

        /// <summary>
        /// 更新一本书
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateBookList")]
        public IHttpActionResult UpdateBookList(UpdateBookListDTO obj)
        {
            var res = YL.BookList.Where(a => a.BookID == obj.BookID).FirstOrDefault();
            //  BookList res = new BookList();
            res.Title = obj.Title;
            res.Abstract = obj.Abstract;
            res.Author = obj.Author;
            res.AddTime = DateTime.Now;
            res.BookType = obj.BookType;
            res.State = obj.State;
            res.TopTime = obj.TopTime;
            res.Recommend = obj.Recommend;
            res.Remark = obj.Remark;
            // res.ClickNum = 0;
            res.CoverIMG = obj.CoverIMG;


            YL.SaveChanges();
            return Content(HttpStatusCode.OK, CodeNew.Successful("更新目录成功", res));

        }

        /// <summary>
        /// 获取直传图片签名,签名有效期60分钟,filename:前端上传的文件名（带后缀名），mtype:1.加封面图
        /// </summary>
        /// <param name="obj">JSON格式{filename:'string',mtype:'int'}</param>
        /// <returns></returns>
        [HttpPost, Route("GetDirectUploadSign")]
        public IHttpActionResult GetDirectUploadSign([FromBody]JObject obj)
        {
            if (obj == null)
            {
                return Content(HttpStatusCode.OK, new resultInfo { Code = 400, Message = "参数错误" });
            }
            OssSignature a = new OssSignature();
            int i = int.Parse(obj["mtype"].ToString());
            string filename = obj["filename"].ToString();
            switch (i)
            {
                case 1:
                    a = SignGen(CoverIMG.objectPath, 60);

                    break;
                default:
                    break;
            }
            // a.FinalUrl = "https://cnaudio.oss-cn-beijing.aliyuncs.com/" + a.Dir + filename;
            a.FinalUrl = CoverIMG.Host + a.Dir + filename;
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "OK", Data = a });
        }

        /// <summary>
        /// 加一个一级章节
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost, Route("CreatBookInfo")]
        public IHttpActionResult CreatBookInfo(CreatBooKinfoDTO obj)
        {
            BookSection res = new BookSection();
            res.BookID = obj.BooklD;
            res.SectionOrder = obj.SectionOrder;
            res.SectionContent = obj.SectionContent;

            res.AddTime = DateTime.Now;
            res.Remark = obj.Remark;
            YL.BookSection.Add(res);
            YL.SaveChanges();
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "OK", Data = res });
        }

        /// <summary>
        /// 增加二级章节及文章
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        [HttpPost, Route("CreatSubBookInfo")]
        public IHttpActionResult CreatSubBookInfo(CreatSubTitleDTO obj)
        {
            SubBookSection res = new SubBookSection();

            res.SectionID = obj.SectionID;
            res.SubSectionOrder = obj.SubSectionOrder;
            res.SubSectionContent = obj.SubSectionContent;
            res.Article = obj.Article;
            res.AddTime = DateTime.Now;
            YL.SubBookSection.Add(res);
            YL.SaveChanges();
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "OK", Data = res });



        }


        /// <summary>
        /// 获取一级章节信息
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>

        [HttpGet, Route("GetBookSection")]
        public IHttpActionResult GetBookInfo(int BookID, int pageSize, int pageIndex)
        {

            var count = YL.BookSection.Where(a => a.BookID == BookID).Count();
            var val = YL.BookSection.Where(a => a.BookID == BookID).Select(a => new
            {
                a.SectionlD,
                a.AddTime,

                a.SectionOrder,
                a.SectionContent,
                a.Remark,
                a.SubBookSection.Count
            }).OrderBy(a => a.SectionOrder).Skip(pageSize * (pageIndex - 1))
                                    .Take(pageSize);
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = count, Data = val });


        }

        /// <summary>
        /// 获取二级章节信息
        /// </summary>
        /// <param name="SectionID">一级章节ID</param>
        /// <param name="pageSize">每页多少张</param>
        /// <param name="pageIndex">页数索引</param>
        /// <returns></returns>

        [HttpGet, Route("GetSubBookSection")]
        public IHttpActionResult GetSubBookInfo(int SectionID, int pageSize, int pageIndex)
        {

            var count = YL.SubBookSection.Where(a => a.SectionID == SectionID).Count();
            var val = YL.SubBookSection.Where(a => a.SectionID == SectionID).Select(a => new
            {

                a.SubSectionID, //二级章节ID
                a.SubSectionOrder, //排序
                a.AddTime,//录入时间
                a.SubSectionContent, //二级章节名字
                a.Article, //正文





            }).OrderBy(a => a.SubSectionOrder).Skip(pageSize * (pageIndex - 1))
                                    .Take(pageSize);
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = count, Data = val });

        }

        /// <summary>
        /// 通过二级章节ID获取某本书的所有信息
        /// </summary>
        /// <param name="SubSectionID">二级标题的ID</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>

        [HttpGet, Route("GetBookAllInfo")]
        public IHttpActionResult GetBookAllInfo(int SubSectionID, int pageSize, int pageIndex)
        {

            var count = YL.SubBookSection.Where(a => a.SubSectionID == SubSectionID).Count();
            var val = YL.SubBookSection.Where(a => a.SubSectionID == SubSectionID).Select(a => new
            {

                a.SubSectionID, //二级章节ID
                a.SectionID, //一级章节ID
                a.SubSectionOrder, //排序
                a.AddTime,//录入时间
                a.SubSectionContent, //二级章节名字
                a.Article, //正文
                a.BookSection.BookID,//书籍ID
                a.BookSection.SectionOrder,//一级标章节排序
                a.BookSection.SectionContent,//一级章节的名字
                a.BookSection.BookList.Title,//书籍名称          
                a.BookSection.BookList.Abstract,
                a.BookSection.BookList.Author,
                a.BookSection.BookList.BookType,
                a.BookSection.BookList.State,
                a.BookSection.BookList.Recommend,
                a.BookSection.BookList.ClickNum, //阅读人数
                a.BookSection.BookList.Remark





            }).OrderBy(a => a.SubSectionOrder).Skip(pageSize * (pageIndex - 1))
                                    .Take(pageSize);
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = count, Data = val });

        }

        /// <summary>
        /// 一级章节更新
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateSection")]
        public IHttpActionResult UpdateSection(UpdateSection obj)
        {

            var cc = YL.BookSection.Where(a => a.SectionlD == obj.SectionID).FirstOrDefault();
            cc.SectionOrder = obj.SectionOrder;
            cc.SectionContent = obj.SectionContent;
            //    cc.AddTime = DateTime.Now;
            cc.Remark = obj.Remark;
            YL.SaveChanges();

            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "更新成功", Data = cc });


        }

        /// <summary>
        /// 二级章节更新
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdateSubSection")]
        public IHttpActionResult UpdateSubSection(UpdateSubSection obj)
        {

            var cc = YL.SubBookSection.Where(a => a.SubSectionID == obj.SubSectionID).FirstOrDefault();
            cc.SubSectionOrder = obj.SubSectionOrder;
            cc.SubSectionContent = obj.SubSectionContent;
            cc.AddTime = DateTime.Now;
            cc.Article = obj.Article;
            YL.SaveChanges();

            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "更新成功", Data = cc });


        }

        /// <summary>
        /// 前端查看书的列表
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("GetBookList")]
        public IHttpActionResult GetBookList()
        {
            var cc = YL.BookList.Where(a => a.State == 1).OrderByDescending(a => a.TopTime).ThenByDescending(b => b.BookID).ToList();
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "查看书籍成功", Data = cc });


        }


        /// <summary>
        /// 增加或者更新记录（如果bookID没有记录 新加一条记录,并且阅读量+1，如果有记录就更新记录）
        /// </summary>
        /// <param name="obj">更新需要传入：ReadTime ReadSection  插入需要传入：BookID ReadSection（这个传入的是BookSectionID）</param>
        /// <returns></returns>

        [HttpPut,Route("UpdateHistory")]
        public IHttpActionResult UpdateHistory(UpdateUserHistory obj)
        {
            var cc=YL.UserReadHistory.Where(a=>a.OpenID== obj.OpenID&&a.BookListID==obj.BookID).FirstOrDefault();
            if (cc==null) //没有记录增加一条记录
            {
                //个人用户增加记录
                UserReadHistory res = new UserReadHistory();
                res.BookListID = obj.BookID; //书的ID
                res.ReadTime =obj.ReadTime;//时间输入
               
                res.ReadSection = obj.ReadSection; //章节ID
                res.OpenID = obj.OpenID;
              
                //在图书列表加一个用户的阅读量
                YL.UserReadHistory.Add(res);
                var c = YL.BookList.Where(a => a.BookID == obj.BookID).First();
                c.ClickNum += 1;

                
                YL.SaveChanges();
                return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "增加书籍成功", Data = cc });

            }
            else //更细一条记录
            {

               cc.ReadTime = obj.ReadTime;//更新阅读章节时间
              

                cc.ReadSection = obj.ReadSection; //更新章节ID
                YL.SaveChanges();
                return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "更新书籍成功", Data = cc });
            }





        }


        /// <summary>
        /// 查询某个书籍历史记录
        /// </summary>
        /// <param name="OpenID"></param>
        /// <param name="BookID">书籍ID</param>
        /// <returns></returns>
         [HttpGet,Route("GetBookHistory")]
        public IHttpActionResult GetBookHistory(string OpenID ,int BookID)
        {

            var cc = YL.UserReadHistory.Where(a => a.OpenID == OpenID && a.BookListID == BookID).Select(a=> new 
            { 
                a.HisID, //历史记录Id
              
                a.BookSection.SectionOrder,
                a.BookSection.SectionContent ,//章节名字
                a.ReadTime ,//已经阅读时间
                a.ReadSection //一级章节ID

            
            }
            ).FirstOrDefault();

            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "查询书籍历史记录成功", Data = cc });
        }

        /// <summary>
        /// 书阅读量+1
        /// </summary>
        /// <param name="BookID">书籍ID 返回0没有找到数据，返回1执行成功</param>
        /// <returns></returns>

        [HttpPut,Route ("BookClick")]
        public IHttpActionResult BookClick(int BookID)
        {
            
            int cc = YL.Database.ExecuteSqlCommand("update BookList set ClickNum=ClickNum+1 where BookID=@BookID", new SqlParameter("@BookID", BookID));

            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "增加读书+1", Data = cc });


        }


    }
}
