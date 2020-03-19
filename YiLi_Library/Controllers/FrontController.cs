using ChinaAudio.Classes.CodeHeper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult CreatSubBookInfo(CreatSubTitleDTO obj)
        {
            SubBookSection res = new SubBookSection();

            res.SectionID = obj.SectionID;
            res.SubSectionOrder = obj.SubSectionOrder;
            res.SubSectionContent = obj.SubSectionContent;
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
        public IHttpActionResult GetBookList()
        {
            var cc = YL.BookList.Where(a => a.State == 1).OrderByDescending(a => a.TopTime).ThenByDescending(b => b.BookID).ToList();
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = "查看书籍成功", Data = cc });


        }





        //更新 阅读时间 阅读的章节 （当用户退出时使用）

        public IHttpActionResult UpdateHistory()
        {




        }




        // 插入一条历史记录（有记录不操作 没记录输入  数据  ）





    }
}
