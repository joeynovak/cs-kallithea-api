namespace Kallithea_NET_API
{
   /// <summary>
   /// The contents of 'members' are either a 
   /// Data structure used by:
   /// 
   /// get_repo
   /// </summary>
   public class Repository_Full
   {
      public int repo_id;
      public string created_on;
      public string fork_of;
      public Repository_Changeset last_changeset;
      public Repository_Member[] members;
      public User_Extended[] followers;        
   }
}