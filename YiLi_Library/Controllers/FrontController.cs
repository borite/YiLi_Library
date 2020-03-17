using ChinaAudio.Classes.CodeHeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YiLi_Library.Entity;
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


    }
}
