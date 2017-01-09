using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * This class contains all the request and response
 * data structures for User related calls. 
 */
namespace KallitheaNetApi
{
    /// <summary>
    /// Data structure used by:
    /// 
    /// create_user
    /// update_user
    /// </summary>
    public class User
    {
      [JsonProperty("user_id")]
      public int UserId;

      [JsonProperty("api_key")]
      public string ApiKey;

      [JsonProperty("username")]
      public string Username;

      [JsonProperty("firstname")]
      public string Firstname;

      [JsonProperty("lastname")]
      public string Lastname;

      [JsonProperty("email")]
      public string Email;

      [JsonProperty("emails")]
      public string[] Emails;

      [JsonProperty("active")]
      public bool Active;

      [JsonProperty("admin")]
      public bool Admin;

      [JsonProperty("ldap_dn")]
      public string LdapDn;

      [JsonProperty("last_login")]
      public string LastLogin;
      }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_user
    /// </summary>
    public class UserExtended : User
    {
      [JsonProperty("ip_addresses")]
      public string[] IpAddresses;
    }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_users
    /// </summary>
    public class UserFull : UserExtended
    {
      [JsonProperty("permissions")]
      public User_Permission UserPermissions;
    }

    /// <summary>
    /// Data structure used by:
    ///
    /// create_user
    /// </summary>
    public class UserCreate
    {
      [JsonProperty("username")]
      public string Username;         // Required

      [JsonProperty("email")]
      public string Email;            // Required

      [JsonProperty("password")]
      public string Password = "";    // Optional ("")

      [JsonProperty("firstname")]
      public string Firstname = "";   // Optional ("")

      [JsonProperty("lastname")]
      public string Lastname = "";    // Optional ("")

      [JsonProperty("active")]
      public bool Active = true;      // Optional (false)

      [JsonProperty("admin")]
      public bool Admin = false;      // Optional (false)

      [JsonProperty("ldap_dn")]
      public string LdapDn = "";     // Optional ("")
    }

   /// <summary>
    /// Data structure used by:
    ///
    /// get_user
    /// </summary>
    public class User_Permission
    {
        public string[] global;
        public Dictionary<string, string> repositories;
        public Dictionary<string, string> repositories_groups;
    }
}
