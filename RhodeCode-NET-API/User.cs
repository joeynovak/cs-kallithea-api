using System;
using System.Collections.Generic;
using System.Text;

/**
 * This class contains all the request and response
 * data structures for User related calls. 
 */
namespace RhodeCode_NET_API
{
    /**
     * Holds information returned by a user request (response).
     * 
     * create_user
     * update_user
     */
    public class User
    {
        public int user_id;
        public string username;
        public string firstname;
        public string lastname;
        public string email;
        public string emails;
        public bool active;
        public bool admin;
        public string ldap_dn;
        public string last_login;
    }

    /**
     * Subclass of User.
     * This class also holds IP addresses for a user.
     * 
     * get_user
     */
    public class User_Extended : User
    {
        public string ip_addresses;
    }

    /**
     * Subclass of User_Extended.
     * This class also holds the permissions for a user.
     * 
     * get_users
     */
    public class User_Full : User_Extended
    {
        public User_Permission permissions;
    }

    /**
     * Holds information used to initiate a a create_user request.
     * 
     * create_user
     */
    public class User_Create
    {
        public string username;         // Required
        public string email;            // Required
        public string password;         // Required
        public string firstname = "";   // Optional ("")
        public string lastname = "";    // Optional ("")
        public bool active = true;      // Optional (false)
        public bool admin = false;      // Optional (false)
        public string ldap_dn = "";     // Optional ("")
    }

    /**
     * Holds information used to initiate an update_user request.
     * 
     * update_user
     */
    public class User_Update
    {
        public int userid;              // Required.
        public string username;         // Optional (null)
        public string email;            // Optional (null)
        public string password;         // Optional (null)
        public string firstname;        // Optional (null)
        public string lastname;         // Optional (null)
        public bool active;             // Optional (null)
        public bool admin;              // Optional (null)
        public string ldap_dn;          // Optional (null)
    }

    /**
     * Used to store user permissions.
     * 
     * get_user
     */
    public class User_Permission
    {
        public string global;
        public KeyValuePair<string,string> repositories;
        public KeyValuePair<string,string> repositories_groups;
    }
}
