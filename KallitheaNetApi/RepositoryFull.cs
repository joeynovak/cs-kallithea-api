using Newtonsoft.Json;

namespace KallitheaNetApi
{
   /// <summary>
   /// The contents of 'members' are either a 
   /// Data structure used by:
   /// 
   /// get_repo
   /// </summary>
   public class RepositoryFull
   {
      [JsonProperty("repo_id")]
      public int RepoId;

      [JsonProperty("created_on")]
      public string CreatedOn;

      [JsonProperty("fork_of")]
      public string ForkOf;

      [JsonProperty("last_changeset")]
      public RepositoryChangeset LastChangeset;

      [JsonProperty("members")]
      public RepositoryMember[] Members;

      [JsonProperty("followers")]
      public UserExtended[] Followers;        
   }
}