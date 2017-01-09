using Newtonsoft.Json;

namespace KallitheaNetApi.Requests
{
   /// <summary>
   /// Data structure used by:
   ///
   /// update_user
   /// </summary>
   public class UserUpdate
   {
      [JsonProperty("userid")]
      public int Userid;              // Required.

      [JsonProperty("username")]
      public string Username = null;  // Optional (null)

      [JsonProperty("email")]
      public string Email = null;     // Optional (null)

      [JsonProperty("password")]
      public string Password = null;  // Optional (null)

      [JsonProperty("firstname")]
      public string Firstname = null; // Optional (null)

      [JsonProperty("lastname")]
      public string Lastname = null;  // Optional (null)

      [JsonProperty("active")]
      public bool? Active = null;     // Optional (null)

      [JsonProperty("admin")]
      public bool? Admin = null;      // Optional (null)

      [JsonProperty("ldap_dn")]
      public string LdapDn = null;   // Optional (null)
   }
}