using System;
using System.IO;
using System.Net;

namespace KallitheaNetApi
{
   /**
    * This class handles the JSON requests and responses 
    * sent to and from the Kallithea server.
    */
   public class Request
   {
      public static bool IgnoreCertificateErrors = false;
      /**
      * This method issues a GET request and returns the response as one long string.
      * 
      * FUTURE IMPLEMENTATION:  Have it marshal a specified object type and return that instead.
      */
      public static string HttpGet(string url)
      {
         HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
         string result = null;

         if (IgnoreCertificateErrors)
            httpWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

         using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
         {
            StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream());
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
         HttpWebRequest httpWebRequest = WebRequest.Create(new Uri(url)) as HttpWebRequest;
         httpWebRequest.Method = "POST";
         httpWebRequest.ContentType = "text/plain";

         if (IgnoreCertificateErrors)
            httpWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
         // Send the request.
         using (var post = new StreamWriter(httpWebRequest.GetRequestStream()))
         {
            post.Write(json);
         }

         string result = null;

         using (HttpWebResponse resp = httpWebRequest.GetResponse() as HttpWebResponse)
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
