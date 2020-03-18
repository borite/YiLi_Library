using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace YiLi_Library.Class
{
    public class AliyunHelper
    {
        public const string AccessKeySecret = "LTAI4Fwvp5vQkst6y6exq9ys";
        public const string AccessKeyID = "HVfostOve4Eg2vRwmkMjQGBFEJbvpy";
        public const string Endpoint = "oss-cn-huhehaote.aliyuncs.com";

        public const string BucketName = "cabiproject";
        public const string Host = "https://" + BucketName + "." + Endpoint;

        static OssClient ossClient = new OssClient(Endpoint, AccessKeyID, AccessKeySecret);



        /// <summary>
        /// OSS签名返回类
        /// </summary>
        public class OssSignature
        {
            public string AccessID { get; set; }

            public string Policy { get; set; }

            public string Signature { get; set; }

            public string Dir { get; set; }

            public string EndPoint { get; set; }

            public long Expire { get; set; }

            public string OSSHost { get; set; }

            /// <summary>
            /// 完整的url
            /// </summary>
            public string FinalUrl { get; set; }

        }

        /// <summary>
        /// Web直传签名方法
        /// </summary>
        /// <param name="dir">上传的目标文件夹</param>
        /// <param name="min">签名有效期限</param>
        /// <returns>签名信息</returns>
        public static OssSignature SignGen(string dir, int min)
        {
            var now = DateTime.Now;
            var ex = now.AddMinutes(min);
            var policyCods = new PolicyConditions();
            policyCods.AddConditionItem("content-length-range", 0L, 1048576000L);
            policyCods.AddConditionItem(MatchMode.StartWith, PolicyConditions.CondKey, dir);
            var postPolicy = ossClient.GeneratePostPolicy(ex, policyCods);
            var binaryData = Encoding.UTF8.GetBytes(postPolicy);
            var encodedPolicy = Convert.ToBase64String(binaryData);
            var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(AccessKeySecret));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(encodedPolicy));
            var signature = Convert.ToBase64String(hashBytes);
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return new OssSignature
            {
                AccessID = AccessKeyID,
                Policy = encodedPolicy,
                Signature = signature,
                Dir = dir,
                EndPoint = Endpoint,
                Expire = (long)(ex - startTime).TotalMilliseconds,
                OSSHost = Host,
            };
        }



    }
}