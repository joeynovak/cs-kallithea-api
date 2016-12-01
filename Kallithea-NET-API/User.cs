using System.Collections.Generic;

/**
 * This class contains all the request and response
 * data structures for User related calls. 
 */
namespace Kallithea_NET_API
{
    /// <summary>
    /// Data structure used by:
    /// 
    /// get_user
    /// get_users
    /// </summary>
    public class User
    {
        public int user_id;
        public string api_key;
        public string[] api_keys;
        public string username;
        public string firstname;
        public string lastname;
        public string email;
        public string[] emails;
        public string[] ip_addresses;
        public bool active;
        public bool admin;
        public string extern_name;
        public string extern_type;
        public string last_login;
        public User_Permission permissions;
    }

    /// <summary>
    /// Data structure used by:
    ///
    /// create_user
    /// </summary>
    public class User_Create
    {
        public string username;                     // Required
        public string email;                        // Required
        public string password = "";                // Optional ("")
        public string firstname = "";               // Optional ("")
        public string lastname = "";                // Optional ("")
        public bool active = true;                  // Optional (false)
        public bool admin = false;                  // Optional (false)
        public string extern_name = "internal";     // Optional ("internal")
        public string extern_type = "internal";     // Optional ("internal")
    }

    /// <summary>
    /// Data structure used by:
    ///
    /// update_user
    /// </summary>
    public class User_Update
    {
        public int userid;                  // Required.
        public string username = null;      // Optional (null)
        public string email = null;         // Optional (null)
        public string password = null;      // Optional (null)
        public string firstname = null;     // Optional (null)
        public string lastname = null;      // Optional (null)
        public bool? active = null;         // Optional (null)
        public bool? admin = null;          // Optional (null)
        public string extern_type = null;   // Optional (null)
        public string extern_name = null;   // Optional (null)
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
