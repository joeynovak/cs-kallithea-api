using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RhodeCode_NET_API
{
    /// <summary>
    /// Contains the data structure for using an API call in RhodeCode.
    /// </summary>
    public class API_Request
    {
        public int id;              // Required
        public string api_key;      // Required
        public string method;       // Required
        public object args;         // Required
    }

    /// <summary>
    /// This class contains the data structure for the response of an API call in RhodeCode.
    /// </summary>
    public class API_Response
    {
        public int id;
        public object result;
        public string error;

        /// <summary>
        /// Use this function to return the appropriate data type for the pull API call.
        /// </summary>
        /// <returns>String representation of result.</returns>
        public string deserialize_pull()
        {
            if (result != null)
                return result.ToString();
            else
                return "";
        }

        /// <summary>
        /// Use this function to return the appropriate data type for the rescan_repos API call.
        /// </summary>
        /// <returns>A rescan_repos_result struct containing the results.</returns>
        public rescan_repos deserialize_rescan_repos()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<rescan_repos>(result.ToString());
            else
                return new rescan_repos();
        }

        /// <summary>
        /// Use this function to return the appropriate data type for the invalidate_cache API call.
        /// </summary>
        /// <returns>String representation of result.</returns>
        public string deserialize_invalidate_cache()
        {
            if (result != null)
                return result.ToString();
            else
                return "";
        }

        /// <summary>
        /// Use this function to return the appropriate data type for the lock API call.
        /// </summary>
        /// <returns>String representation of result.</returns>
        public string deserialize_lock()
        {
            if (result != null)
                return result.ToString();
            else
                return "";
        }

        /// <summary>
        /// Use this function to return the appropriate data type for the show_ip API call.
        /// </summary>
        /// <returns>A show_ip data structure containing the result of the API call.</returns>
        public show_ip deserialize_show_ip()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<show_ip>(result.ToString());
            else
                return new show_ip();
        }

        /// <summary>
        /// Use this function to return the appropriate data type for the get_user API call.
        /// </summary>
        /// <returns>A User_Full object containing the result of the get_user API call.</returns>
        public User_Full deserialize_get_user()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<User_Full>(result.ToString());
            else
                return new User_Full();
        }

        /// <summary>
        /// Use this function to return the appropriate data type for the get_users API call.
        /// </summary>
        /// <returns>A User object array containing all users returned by the get_users API call.</returns>
        public User[] deserialize_get_users()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<User[]>(result.ToString());
            else
                return new User[0];
        }
    
    
    }

    /**
     * Arguments for the API calls.
     */
    struct pull_args { public string repoid; }
    struct rescan_args { public bool remove_obselete; }
    struct invalidate_args { public string repoid; }
    struct lock_args { public string repoid; public string userid; public bool locked; }
    struct show_ip_args { public string userid; }
    struct get_user_args { public string userid; }

    /**
     * Used for deserialization.
     */
    public struct rescan_repos
    {
        public string[] added;
        public string[] removed;
    }

    public struct user_ip
    {
        public string ip_addr;
        public string[] ip_range;
    }

    public struct show_ip
    {
        public string ip_addr_server;
        public user_ip[] user_ips;
    }
}
