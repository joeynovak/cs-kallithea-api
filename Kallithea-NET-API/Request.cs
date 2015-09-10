using System;
using System.IO;
using System.Net;

namespace Kallithea_NET_API
{
    /**
     * This class handles the JSON requests and responses 
     * sent to and from the Kallithea server.
     */
    public class Request
    {
        /**
        * This method issues a GET request and returns the response as one long string.
        * 
        * FUTURE IMPLEMENTATION:  Have it marshal a specified object type and return that instead.
        */
        public static string HttpGet(string url)
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
         */
        public static string HttpPost(string url, string json)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "text/plain";

            // Send the request.
            using (var post = new StreamWriter(req.GetRequestStream()))
            {
                post.Write(json);
            }

            string result = null;

            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }

            return result;
        }

        /**
         * Convert string to byte array.
         */
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
