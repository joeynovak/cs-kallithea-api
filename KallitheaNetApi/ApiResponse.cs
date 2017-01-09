using Newtonsoft.Json;

namespace KallitheaNetApi
{
   /// <summary>
   /// This class contains the data structure for the response of an API call in Kallithea.
   /// </summary>
   public class ApiResponse<T>
   {
      public int Id;
      public T Result;
      public string Error;
   }
}