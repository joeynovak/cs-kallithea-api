using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kallithea_NET_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KallitheaNetApi;

namespace Kallithea_NET_API.Tests
{
   [TestClass()]
   public class KallitheaTests
   {
      [TestMethod()]
      public void get_userTest()
      {
         Settings setting = Settings.GetSettings();
         if (setting.KallitheaIgnoreCertificateErrors)
            Request.IgnoreCertificateErrors = true;

         Kallithea server = new Kallithea(setting.KallitheaUrl, setting.KallitheaAdminApiKey);         
         var something = server.get_ip(setting.KallitheaRegularUsername);
         
         return;
      }
   }
}