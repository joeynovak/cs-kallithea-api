
/**
 * This class contains all the request and response
 * data structures for User Group related calls. 
 */

using Newtonsoft.Json;

namespace KallitheaNetApi
{
    /// <summary>
    /// Data structure used by:
    /// 
    /// get_users_groups
    /// </summary>
    public class UserGroup
    {
      [JsonProperty("users_group_id")]
      public int UsersGroupId;

      [JsonProperty("group_name")]
      public string GroupName;

      [JsonProperty("active")]
      public bool Active;
    }
}
