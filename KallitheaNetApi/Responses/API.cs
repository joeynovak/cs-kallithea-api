using Newtonsoft.Json;

namespace KallitheaNetApi.Responses
{
   /**
     * Used for deserialization.
     */
   public class RescanRepos : RequestSpecificResponseData
   {
      [JsonProperty("added")]
      public string[] Added;

      [JsonProperty("removed")]
      public string[] Removed;
   }
   public class LockRepo : RequestSpecificResponseData
   {
      [JsonProperty("repo")]
      public string Repo;

      [JsonProperty("locked")]
      public bool Locked;

      [JsonProperty("locked_since")]
      public float? LockedSince;

      [JsonProperty("locked_by")]
      public string LockedBy;

      [JsonProperty("msg")]
      public string Msg;
   }
   public class UserIp : RequestSpecificResponseData
   {
      [JsonProperty("ip_addr")]
      public string IpAddr;

      [JsonProperty("ip_range")]
      public string[] IpRange;
   }
   public class GetIp : RequestSpecificResponseData
   {
      [JsonProperty("server_ip_addr")]
      public string ServerIpAddr;

      [JsonProperty("user_ips")]
      public UserIp[] UserIps;
   }
   public class UserMessage : RequestSpecificResponseData
   {
      [JsonProperty("msg")]
      public string Msg;

      [JsonProperty("user")]
      public User User;
   }
   public class UserUpdate : RequestSpecificResponseData
   {
      [JsonProperty("msg")]
      public string Msg;

      [JsonProperty("user")]
      public UserExtended User;
   }
   public class UserGroupMessage : RequestSpecificResponseData
   {
      [JsonProperty("msg")]
      public string Msg;

      [JsonProperty("user_group")]
      public UserGroup UserGroup;
   }
   public class Response : RequestSpecificResponseData
   {
      [JsonProperty("success")]
      public bool Success;

      [JsonProperty("msg")]
      public string Msg;
   }
   public class RepositoryMessage : RequestSpecificResponseData
   {
      [JsonProperty("msg")]
      public string Msg;

      [JsonProperty("repo")]
      public RepositoryAll Repo;
   }

   public class Pull : RequestSpecificResponseData
   {

   }
}