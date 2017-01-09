using System.Collections.Generic;
using IniParser;
using IniParser.Model;

namespace KallitheaNetApi.Tests
{
   public class Settings
   {
      public string KallitheaUrl { get; set; }
      public string KallitheaAdminApiKey { get; set; }
      public string KallitheaRegularApiKey { get; set; }
      public string KallitheaRegularUsername { get; set; }
      public bool KallitheaIgnoreCertificateErrors { get; set; }

      public static Settings GetSettings()
      {
         var parser = new FileIniDataParser();
         IniData data = parser.ReadFile("test_repository.ini");

         Settings settings = new Settings();
         settings.KallitheaUrl = data["Kallithea"]["Url"];
         settings.KallitheaAdminApiKey = data["Kallithea"]["AdminApiKey"];
         settings.KallitheaRegularApiKey = data["Kallithea"]["RegularApiKey"];
         settings.KallitheaRegularUsername = data["Kallithea"]["RegularUsername"];
         settings.KallitheaIgnoreCertificateErrors = bool.Parse(data["Kallithea"]["IgnoreCertificateErrors"].ToLower());
         return settings;
      }
   }
}