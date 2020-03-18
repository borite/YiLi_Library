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
            return Content(HttpStatusCode.OK, CodeNew.Successful("图书新加目录成功", ""));


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

            YL.BookList.Add(res);
            YL.SaveChanges();
            return Content(HttpStatusCode.OK, CodeNew.Successful("更新目录成功", res));

        }

        //加一个书


        //加一本书的内容
        //OSS直传
        /// <summary>
        /// 获取直传图片签名,签名有效期60分钟,filename:前端上传的文件名（带后缀名），mtype:1-新闻、2-评测、3-体验、4-社区、5.头像
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
        [HttpPost,Route("CreatBookInfo")]
        public IHttpActionResult CreatBookInfo(CreatBooKinfoDTO obj)
        {
            BookSection res = new BookSection();
            res.BookID = obj.BooklD;
            res.SectionOrder = obj.SectionOrder;
        
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

       [HttpGet,Route("GetBookSection")]
        public IHttpActionResult GetBookInfo(int BookID,int pageSize,int pageIndex)
        {

            var count = YL.BookSection.Where(a => a.BookID == BookID).Count();
            var val=YL.BookSection.Where(a=>a.BookID==BookID).Select(a=> new {
                a.SectionlD,
                a.AddTime,
             
                a.SectionOrder,
                a.SectionContent,
                a.Remark,
                a.SubBookSection.Count
            }).OrderBy(a=>a.SectionOrder).Skip(pageSize * (pageIndex - 1))
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
            var val = YL.SubBookSection.Where(a => a.SectionID == SectionID).Select(a => new {
       
                a.SubSectionID, //二级章节ID
                a.SubSectionOrder, //排序
                a.AddTime,//录入时间
                a.SubSectionContent, //二级章节名字
                a.Article, //正文

                



            }).OrderBy(a => a.SubSectionOrder).Skip(pageSize * (pageIndex - 1))
                                    .Take(pageSize);
            return Content(HttpStatusCode.OK, new resultInfo { Code = 200, Message = count, Data = val });

        }


       




    }
}
