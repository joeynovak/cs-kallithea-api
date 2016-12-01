
/**
 * This class contains all the request and response
 * data structures for Repository Group related calls. 
 */
namespace Kallithea_NET_API
{
    /// <summary>`
    /// Data structure used by:
    /// 
    /// get_repo_group, get_repo_groups.
    /// </summary>
    public class RepositoryGroup
    {
        public int group_id;
        public string group_name;                  // Required. 
        public string group_description = "";      // Optional. (group name)
        public Repository_Member[] members;
        public string owner = "_apiuser_";         // Optional. (_apiuser_)
        public string parent_group = null;         // Optional. (null)
        public string[] repositories;
    }
}