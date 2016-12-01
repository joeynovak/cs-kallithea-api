
/**
 * This class contains all the request and response
 * data structures for Repository related calls. 
 */
namespace Kallithea_NET_API
{
    /// <summary>
    /// Data structure used by:
    /// 
    /// create_repo.
    /// </summary>
    public class Repository
    {
        public string repo_name;                   // Required.
        public string owner = "apiuser";           // Optional. (apiuser)
        public string repo_type = "hg";            // Optional. (hg)
        public string description = "";            // Optional. ("")
        public bool @private = false;              // Optional. (false)
        public string clone_uri = null;            // Optional. (null)
        public string landing_rev = "rev:tip";     // Optional. (rev:tip)
        public bool enable_downloads = false;      // Optional. (false)
        public bool enable_locking = false;        // Optional. (false)
        public bool enable_statistics = false;     // Optional. (false)
        public bool copy_permissions = false;      // Optional. (false)
    }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_repos
    /// </summary>
    public class Repository_All
    {
        public string repo_name;                                // Required.
        public string owner = "apiuser";                        // Optional. (apiuser)
        public string repo_type = "hg";                         // Optional. (hg)
        public string description = "";                         // Optional. ("")
        public bool @private = false;                           // Optional. (false)
        public string clone_uri = null;                         // Optional. (null)
        public string[] landing_rev = { "rev", "tip" };         // Optional. (tip)
        public bool enable_downloads = false;                   // Optional. (false)
        public bool enable_locking = false;                     // Optional. (false)
        public bool enable_statistics = false;                  // Optional. (false)
        public int repo_id;
        public string created_on;
        public string fork_of;
    }

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
        public User[] followers;        
    }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_repo
    /// </summary>
    public class Repository_Changeset
    {
        public string author;
        public string date;
        public string message;
        public int raw_id;
        public int revision;
        public int short_id;
    }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_repo_nodes
    /// </summary>
    public class Repository_Node
    {
        public string name;
        public string type;
    }

    /// <summary>
    /// Used to store either user or user_group member object.
    /// Data structure used by:
    /// 
    /// get_repo
    /// </summary>
    public class Repository_Member
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

        // Used by repo_group type.
        public string origin;
    }


}