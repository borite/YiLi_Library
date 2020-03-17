using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ChinaAudio.Classes.CodeHeper
{
    public class CodeNew
    {
        /// <summary>
        /// 返回规定的一些字段
        /// </summary>
        public class resultInfo
        {

            /// <summary>
            /// 状态码
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 信息提示
            /// </summary>
            public dynamic Message { get; set; }
            /// <summary>
            /// 返回数据
            /// </summary>
            public dynamic Data { get; set; }
        }

        /// <summary>
        /// 成功后引用的方法
        /// </summary>
        /// <param name="message">返回message数据</param>
        /// <param name="data">返回data数据</param>
        /// <returns></returns>
        public static dynamic Successful (dynamic message,dynamic data)
        {
          return  new resultInfo { Code = (int)HttpStatusCode.OK, Message = message, Data = data };

        }

        /// <summary>
        /// 成功新方法
        /// </summary>
        /// <param name="Code">状态传入</param>
        /// <param name="message">信息传入</param>
        /// <param name="data">数据传入</param>
        /// <returns></returns>

        public static dynamic Success(dynamic Code, dynamic message, dynamic data)
        {
            return new resultInfo { Code =Convert.ToInt32(Code) , Message = message, Data = data };

        }

        /// <summary>
        ///  请求失败
        /// </summary>
        /// <param name="message">返回message数据</param>
        /// <param name="data">返回data数据</param>
        /// <returns></returns>
        public static dynamic Bad(dynamic message, dynamic data)
        {
            return new resultInfo { Code =Convert.ToInt32(HttpStatusCode.BadRequest), Message = message, Data = data };

        }





    }
}