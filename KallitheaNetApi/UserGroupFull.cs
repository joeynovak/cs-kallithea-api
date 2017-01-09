using Newtonsoft.Json;

namespace KallitheaNetApi
{
   /// <summary>
   /// Data structure used by:
   /// 
   /// get_users_group
   /// </summary>
   public class UserGroupFull : UserGroup
   {
      [JsonProperty("members")]
      public User[] Members;
   }
}