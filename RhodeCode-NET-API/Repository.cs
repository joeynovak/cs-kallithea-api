using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeCode_NET_API
{
    public class Repository
    {
        public string repo_name;                   // Required.
        public string owner;                       // Required.
        public string repo_type = "hg";            // Optional. Default: hg
        public string description = "";            // Optional. Default: ""
        public bool @private = false;              // Optional. Default: false
        public string clone_uri = null;            // Optional. Default: null
        public string landing_rev = "tip";         // Optional. Default: tip
        public bool enable_downloads = false;      // Optional. Default: false
        public bool enable_locking = false;        // Optional. Default: false
        public bool enable_statistics = false;     // Optional. Default: false
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