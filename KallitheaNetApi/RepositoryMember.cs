namespace KallitheaNetApi
{
   /// <summary>
   /// Used to store either user or user_group member object.
   /// Data structure used by:
   /// 
   /// get_repo
   /// </summary>
   public class RepositoryMember
   {
      // Used by both types.
      public string type;                    
      public bool active;                    
      public string permission;               

      // Used by user type.
      public int? user_id = null;            
      public string api_key = null;         
      public string username = null;          
      public string firstname = null;
      public string lastname = null;
      public string email = null;
      public string[] emails = null;
      public string ldap_dn = null;
      public string last_login = null;

      // Used by user_group type.
      public int? id = null;
      public string name = null; 
   }
}