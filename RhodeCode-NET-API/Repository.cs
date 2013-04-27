using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeCode_NET_API
{
    /**
     * This class contains all the request and response
     * data structures for Repository related calls. 
     */

    /**
     * Used in: create_repo
     */
    public class Repository
    {
        public string repo_name;                   // Required.
        public string owner;                       // Required.
        public string repo_type = "hg";            // Optional. (hg)
        public string description = "";            // Optional. ("")
        public bool @private = false;              // Optional. (false)
        public string clone_uri = null;            // Optional. (null)
        public string landing_rev = "tip";         // Optional. (tip)
        public bool enable_downloads = false;      // Optional. (false)
        public bool enable_locking = false;        // Optional. (false)
        public bool enable_statistics = false;     // Optional. (false)
    }

    /**
     * Used to store information for create_repo response.  Marshalling.
     */
    class Repository_Response
    {
        public int repo_id;
        public string repo_name;
        public string repo_type;
        public string clone_uri;
        public bool @private;
        public string created_on;
        public string description;
        public string landing_rev;
        public string owner;
        public string fork_of;
        public bool enable_downloads;
        public bool enable_locking;
        public bool enable_statistics;
    }

    /**
     * Used for repository responses.
     * 
     * List
     * All
     * Calls
     * Here
     */
    class Repository_Result
    {
        public string msg;
        public Repository_Response repo;
    }
}