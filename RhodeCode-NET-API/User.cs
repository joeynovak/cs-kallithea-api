using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

/**
 * This class contains all the request and response
 * data structures for User related calls. 
 */
namespace RhodeCode_NET_API
{
    /// <summary>
    /// Data structure used by:
    /// 
    /// create_user
    /// update_user
    /// </summary>
    public class User
    {
        public int user_id;
        public string api_key;
        public string username;
        public string firstname;
        public string lastname;
        public string email;
        public string[] emails;
        public bool active;
        public bool admin;
        public string ldap_dn;
        public string last_login;
    }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_user
    /// </summary>
    public class User_Extended : User
    {
        public string[] ip_addresses;
    }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_users
    /// </summary>
    public class User_Full : User_Extended
    {
        public User_Permission permissions;
    }

    /// <summary>
    /// Data structure used by:
    ///
    /// create_user
    /// </summary>
    public class User_Create
    {
        public string username;         // Required
        public string email;            // Required
        public string password = "";    // Optional ("")
        public string firstname = "";   // Optional ("")
        public string lastname = "";    // Optional ("")
        public bool active = true;      // Optional (false)
        public bool admin = false;      // Optional (false)
        public string ldap_dn = "";     // Optional ("")
    }

    /// <summary>
    /// Data structure used by:
    ///
    /// update_user
    /// </summary>
    public class User_Update
    {
        public int userid;              // Required.
        public string username = null;  // Optional (null)
        public string email = null;     // Optional (null)
        public string password = null;  // Optional (null)
        public string firstname = null; // Optional (null)
        public string lastname = null;  // Optional (null)
        public bool? active = null;     // Optional (null)
        public bool? admin = null;      // Optional (null)
        public string ldap_dn = null;   // Optional (null)
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
