using Newtonsoft.Json;

namespace Kallithea_NET_API
{
    /// <summary>
    /// Contains the data structure for using an API call in Kallithea.
    /// </summary>
    public class API_Request
    {
        public int id;              // Required
        public string api_key;      // Required
        public string method;       // Required
        public object args;         // Required
    }

    /// <summary>
    /// This class contains the data structure for the response of an API call in Kallithea.
    /// </summary>
    public class API_Response
    {
        public int id;
        public object result;
        public string error;

        public string deserialize_pull()
        {
            if (result != null)
                return result.ToString();
            else
                return "";
        }
      
        public rescan_repos deserialize_rescan_repos()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<rescan_repos>(result.ToString());
            else
                return new rescan_repos();
        }
        
        public string deserialize_invalidate_cache()
        {
            if (result != null)
                return result.ToString();
            else
                return "";
        }
       
        public lock_repo deserialize_lock()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<lock_repo>(result.ToString());
            else
                return new lock_repo();
        }
       
        public get_ip deserialize_get_ip()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<get_ip>(result.ToString());
            else
                return new get_ip();
        }
       
        public User_Full deserialize_get_user()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<User_Full>(result.ToString());
            else
                return new User_Full();
        }
       
        public User_Extended[] deserialize_get_users()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<User_Extended[]>(result.ToString());
            else
                return new User_Extended[0];
        }
       
        public user_message deserialize_create_user()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<user_message>(result.ToString());
            else
                return new user_message();
        }

        public user_update deserialize_update_user()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<user_update>(result.ToString());
            else
                return new user_update();
        }

        public user_message deserialize_delete_user()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<user_message>(result.ToString());
            else
                return new user_message();
        }

        public UserGroup_Full deserialize_get_user_group()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<UserGroup_Full>(result.ToString());
            else
                return new UserGroup_Full();
        }

        public UserGroup[] deserialize_get_user_groups()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<UserGroup[]>(result.ToString());
            else
                return new UserGroup[0];
        }

        public user_group_message deserialize_create_user_group()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<user_group_message>(result.ToString());
            else
                return new user_group_message();
        }

        public response deserialize_add_user_to_user_group()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }

        public response deserialize_remove_user_from_user_group()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }

        public Repository_Full deserialize_get_repo()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<Repository_Full>(result.ToString());
            else
                return new Repository_Full();
        }

        public Repository_All[] deserialize_get_repos()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<Repository_All[]>(result.ToString());
            else
                return new Repository_All[0];
        }

        public Repository_Node[] deserialize_get_repo_nodes()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<Repository_Node[]>(result.ToString());
            else
                return new Repository_Node[0];
        }

        public repository_message deserialize_create_repo()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<repository_message>(result.ToString());
            else
                return new repository_message();
        }

        public response deserialize_fork_repo()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }

        public response deserialize_delete_repo()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }

        public response deserialize_grant_user_permission()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }

        public response deserialize_revoke_user_permission()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }

        public response deserialize_grant_user_group_permission()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }

        public response deserialize_revoke_user_group_permission()
        {
            if (result != null)
                return JsonConvert.DeserializeObject<response>(result.ToString());
            else
                return new response();
        }
    }

    /**
     * Used for deserialization.
     */
    public struct rescan_repos
    {
        public string[] added;
        public string[] removed;
    }
    public struct lock_repo
    {
        public string repo;
        public bool locked;
        public float? locked_since;
        public string locked_by;
        public string msg;
    }
    public struct user_ip
    {
        public string ip_addr;
        public string[] ip_range;
    }
    public struct get_ip
    {
        public string ip_addr_server;
        public user_ip[] user_ips;
    }
    public struct user_message
    {
        public string msg;
        public User user;
    }
    public struct user_update
    {
        public string msg;
        public User_Extended user;
    }
    public struct user_group_message
    {
        public string msg;
        public UserGroup user_group;
    }
    public struct response
    {
        public bool success;
        public string msg;
    }
    public struct repository_message 
    {
        public string msg;
        public Repository_All repo;
    }

}