using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KallitheaNetApi;

namespace KallitheaNetApi.Tests
{
   [TestClass()]
   public class KallitheaTests
   {
      [TestMethod()]
      public void GetUserTest()
      {
         Settings setting = Settings.GetSettings();
         if (setting.KallitheaIgnoreCertificateErrors)
            Request.IgnoreCertificateErrors = true;

         Kallithea server = new Kallithea(setting.KallitheaUrl, setting.KallitheaAdminApiKey);
         var something = server.GetIp(setting.KallitheaRegularUsername);

         Assert.AreEqual(something.Result.ServerIpAddr, "127.0.0.1");
      }

      [TestMethod()]
      public void CreateRepoTest()
      {
         string newRepoName = "Test1";
         Settings setting = Settings.GetSettings();
         if (setting.KallitheaIgnoreCertificateErrors)
            Request.IgnoreCertificateErrors = true;

         Kallithea server = new Kallithea(setting.KallitheaUrl, setting.KallitheaAdminApiKey);
         server.delete_repo(newRepoName);

         var results = server.CreateRepo(new Repository() { @private = true, repo_name = "Test1" });

         Assert.IsNull(results.Error);

         server.delete_repo(newRepoName);
      }

      [TestMethod()]
      public void GetReposTest()
      {
         Settings setting = Settings.GetSettings();
         if (setting.KallitheaIgnoreCertificateErrors)
            Request.IgnoreCertificateErrors = true;

         Kallithea server = new Kallithea(setting.KallitheaUrl, setting.KallitheaAdminApiKey);

         var results = server.GetRepos();
         return;
      }
   }
}