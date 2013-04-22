using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;

namespace RhodeCode_NET_API
{
    class Request
    {
        /**
        * This method issues a GET request and returns the response as one long string.
        * 
        * FUTURE IMPLEMENTATION:  Have it marshal a specified object type and return that instead.
        */
        static string HttpGet(string url)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            string result = null;

            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }

        /**
         * This method issues a POST request with the associated JSON encoded string.  Returns the response.
         * 
         * FUTURE IMPLEMENTATION:  Have it marshal a specified object type and return that instead.
         */
        static void HttpPost(string url, string json, object result)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "text/plain";

            // Send the request.
            using (var post = new StreamWriter(req.GetRequestStream()))
            {
                post.Write(json);
            }

            string tmp = null;

            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                tmp = reader.ReadToEnd();
            }

            //return JsonConvert.DeserializeObject(result);
        }

        /**
         * Convert string to byte array.
         */
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
