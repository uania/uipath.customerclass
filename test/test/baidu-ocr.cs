using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace test
{
    internal class baidu_ocr
    {
        public byte[] LoadImage(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);

                return buffer;
            }
        }

        public string ToBase64(byte[] buffer)
        {
            var str = Convert.ToBase64String(buffer, 0, buffer.Length);
            Console.WriteLine(str);
            return str;
        }

        public string EncodeImage(string base64Str)
        {
            return HttpUtility.UrlEncode(base64Str);
        }

        public void ExecOcr(string path, string url)
        {
            var image = this.LoadImage(path);
            var base64Str = this.ToBase64(image);
            var urlEncodeStr = this.EncodeImage(base64Str);
            var postDataStr = $"image={urlEncodeStr}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            Encoding encoding = Encoding.UTF8;
            byte[] postData = encoding.GetBytes(postDataStr);
            request.ContentLength = postData.Length;
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(postData, 0, postData.Length);
            myRequestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            Console.WriteLine(retString);
        }
    }



    public class Form
    {
        // 表格文字识别(同步接口)
        public static string form()
        {
            string token = "24.00614ce04db9f9f8adc27cb7837b9f0a.2592000.1643873603.282335-24677035";
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/form?access_token=" + token;
            //string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/numbers?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            // 图片的base64编码
            //string base64 = getFileBase64("d:\\u8.png");
            string base64 = getFileBase64("d:\\u8-number.png");
            String str = "image=" + HttpUtility.UrlEncode(base64);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            Console.WriteLine("表格文字识别(同步接口):");
            Console.WriteLine(result);
            return result;
        }

        public static string getFileBase64(String fileName)
        {
            FileStream filestream = new FileStream(fileName, FileMode.Open);
            byte[] arr = new byte[filestream.Length];
            filestream.Read(arr, 0, (int)filestream.Length);
            string baser64 = Convert.ToBase64String(arr);
            filestream.Close();
            return baser64;
        }
    }
}
