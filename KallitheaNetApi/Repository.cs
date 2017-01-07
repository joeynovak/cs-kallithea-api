
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
        public string owner = "_apiuser_";         // Optional. (_apiuser_)
        public string repo_type = "hg";            // Optional. (hg)
        public string description = "";            // Optional. ("")
        public bool @private = false;              // Optional. (false)
        public string clone_uri = null;            // Optional. (null)
        public string landing_rev = "tip";         // Optional. (tip)
        public bool enable_downloads = false;      // Optional. (false)
        public bool enable_locking = false;        // Optional. (false)
        public bool enable_statistics = false;     // Optional. (false)
    }
}