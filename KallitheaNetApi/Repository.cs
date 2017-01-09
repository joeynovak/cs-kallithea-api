
/**
 * This class contains all the request and response
 * data structures for Repository related calls. 
 */
namespace KallitheaNetApi
{
    /// <summary>
    /// Data structure used by:
    /// 
    /// create_repo.
    /// </summary>
    public class Repository
    {
        public string repo_name;                   // Required.
        public string owner = null;                // Optional. (_apiuser_)
        public string repo_type = null;            // Optional. (hg)
        public string description = null;          // Optional. ("")
        public bool ? @private = null;               // Optional. (false)
        public string clone_uri = null;            // Optional. (null)
        public string landing_rev = null;         // Optional. (tip)
        public bool ? enable_downloads = null;      // Optional. (false)
        public bool ? enable_locking = null;        // Optional. (false)
        public bool ? enable_statistics = null;     // Optional. (false)
    }
}