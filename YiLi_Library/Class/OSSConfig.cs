using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YiLi_Library.Class
{
    public class OSSConfig
    {

        public class CoverIMG : AliyunHelper
        {
            //文件OSS存储路径
            public static string objectPath = "YL/LibaryProject/ListIMG";
            //图片名字前缀
            public static string ImgFirstName = "Cover";
        }
    }
}