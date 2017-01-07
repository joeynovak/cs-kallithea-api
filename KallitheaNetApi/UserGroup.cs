
/**
 * This class contains all the request and response
 * data structures for User Group related calls. 
 */
namespace Kallithea_NET_API
{
    /// <summary>
    /// Data structure used by:
    /// 
    /// get_users_groups
    /// </summary>
    public class UserGroup
    {
        public int users_group_id;
        public string group_name;
        public bool active;
    }

    /// <summary>
    /// Data structure used by:
    /// 
    /// get_users_group
    /// </summary>
    public class UserGroup_Full : UserGroup
    {
        public User[] members;
    }
}
