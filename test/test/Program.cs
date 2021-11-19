using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                int? a = null;
                int? b = 3;
                if (a.HasValue)
                {
                    Console.WriteLine(a);
                }
                if (b.HasValue)
                {
                    Console.WriteLine(b);
                }
            }
            {
                //var subTitle = "1235t43854395666666";
                //subTitle = subTitle.Remove(subTitle.Length - 6, 6) + "*";

                //var a = new List<string>();
                //a.AddRange(new string[9]);

                ////var timestr = "30/09/2021 17:37";
                ////var date = Convert.ToDateTime(timestr);
                ////Console.WriteLine(date.ToString());
                //Console.WriteLine("Hello World!");
                //var departments = new List<ProjectInfo>();
                //departments.Add(new ProjectInfo
                //{
                //    CompanyName = "rpa",
                //    DepartName = "abc"
                //});
                //var departNames = departments.GroupBy(r => new { r.CompanyName, r.DepartName }).Select(r => new KeyValuePair<string, string>(r.Key.CompanyName.ToString(), r.Key.DepartName.ToString())).ToList();
                //foreach (var item in departNames)
                //{
                //    Console.WriteLine(item.Key);
                //    Console.WriteLine(item.Value);
                //}

                //var da = DateTime.Now.ToShortDateString();
            }
            {

                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
                //using (StreamWriter writer = new StreamWriter("PrivateKey.xml"))  //这个文件要保密...
                //{

                //    writer.WriteLine(rsa.ToXmlString(true));

                //}
                //using (StreamWriter writer = new StreamWriter("PublicKey.xml"))
                //{

                //    writer.WriteLine(rsa.ToXmlString(false));

                //}
                //var privateKey = rsa.ExportParameters(true);
                //var publicKey = rsa.ExportParameters(false);

                ////存储
                //File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "key.json"), JsonConvert.SerializeObject(privateKey));
                //File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "key.public.json"), JsonConvert.SerializeObject(publicKey));
            }
            {
                //Console.WriteLine(AbcType.b.ToString()); 
                //Console.WriteLine(AbcType.b.ToString("g")); 
                //Console.WriteLine(AbcType.b.ToString("G")); 
                //Console.WriteLine(AbcType.b.ToString("d")); 
            }
            {
                var callbackUrl = "http://www.baidu.com/index.html";
                var en_callbackUrl = System.Web.HttpUtility.UrlEncode(callbackUrl);
                var url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state=123#wechat_redirect";
                var uuuu = string.Format(url, "wx6e78fb2a79a3afff", en_callbackUrl, "snsapi_userinfo");
                string newFileName = "d:\\qrcode.jpg";
                var a = ImageManage.BitmapByte(ImageManage.QRCodeBimapForString(uuuu));
                using (FileStream file = new FileStream(newFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    BinaryWriter bw = new BinaryWriter(file);

                    for (int i = 0; i < a.ToArray().Length; i++)

                        bw.Write(a[i]);
                }

            }
            Console.ReadKey();
        }
    }
}
