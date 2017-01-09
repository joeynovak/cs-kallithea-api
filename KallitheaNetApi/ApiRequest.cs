// ReSharper disable InconsistentNaming
namespace KallitheaNetApi
{
   /// <summary>
   /// Contains the data structure for using an API call in Kallithea.
   /// </summary>
   public class ApiRequest
   {
      public int id;              // Required
      public string api_key;      // Required
      public string method;       // Required
      public object args;         // Required
   }
}