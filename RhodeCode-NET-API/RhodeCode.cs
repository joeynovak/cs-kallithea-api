using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RhodeCode_NET_API
{
    public class RhodeCode
    {
        private string host;
        private string api_key;

        /// <summary>
        /// Used to connect and execute API calls to a RhodeCode server.
        /// </summary>
        ///
        /// <param name="api_key">An API Key belonging to a user with admin privileges.</param>
        /// <param name="host">The location of the RhodeCode server.  Include /_admin/api on the end.</param>
        public RhodeCode(string host, string api_key)
        {
            this.host = host;
            this.api_key = api_key;
        }

        /// <summary>
        /// Pulls given repo from remote location. 
        /// Can be used to automatically keep remote repos up to date. 
        /// This command can be executed only using api_key belonging to user with admin rights
        /// </summary>
        /// 
        /// <param name="repo">reponame or repo_id.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response pull(string repo)
        {
            // Create a random ID.
            Random rand = new Random();
            int id = rand.Next(1,1000);

            // Build the arguments
            pull_args args;
            args.repoid = repo;

            // Create the request.
            API_Request request = new API_Request();
            request.id = id;
            request.method = "pull";
            request.api_key = api_key;
            request.args = args;

            // Serialize to JSON.
            string json_request = JsonConvert.SerializeObject(request);

            // Need to clean up the args a bit.
            json_request = json_request.Replace("\\\"", "\"").Replace("\"args\":\"", "\"args\":").Replace("}\"}", "}}");

            // Send the request, store response.
            string json_response = Request.HttpPost(host, json_request);

            // Marshal the response and return it.
            return JsonConvert.DeserializeObject<API_Response>(json_response);
        }

        /// <summary>
        /// Dispatch rescan repositories action. If remove_obsolete is set RhodeCode will delete 
        /// repos that are in database but not in the filesystem. This command can be executed 
        /// only using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="remove_obselete">Used to delete repos that are in the database but not the filesystem.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response rescan_repos(bool remove_obselete)
        {
            // Create a random ID.
            Random rand = new Random();
            int id = rand.Next(1,1000);

            // Build the arguments
            rescan_args args;
            args.remove_obselete = remove_obselete;

            // Create the request.
            API_Request request = new API_Request();
            request.id = id;
            request.method = "rescan_repos";
            request.api_key = api_key;
            request.args = args;

            // Serialize to JSON.
            string json_request = JsonConvert.SerializeObject(request);

            // Need to clean up the args a bit.
            json_request = json_request.Replace("\\\"", "\"").Replace("\"args\":\"", "\"args\":").Replace("}\"}", "}}");

            // Send the request, store response.
            string json_response = Request.HttpPost(host, json_request);

            // Marshal the response and return it.
            return JsonConvert.DeserializeObject<API_Response>(json_response);
        }

        /// <summary>
        /// Invalidate cache for repository. This command can be executed only using api_key belonging to user 
        /// with admin rights or regular user that have write or admin or write access to repository.
        /// </summary>
        /// 
        /// <param name="repo">reponame or repo_id.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response invalidate_cache(string repo)
        {
            // Create a random ID.
            Random rand = new Random();
            int id = rand.Next(1, 1000);

            // Build the arguments
            invalidate_args args;
            args.repoid = repo;

            // Create the request.
            API_Request request = new API_Request();
            request.id = id;
            request.method = "invalidate_cache";
            request.api_key = api_key;
            request.args = args;

            // Serialize to JSON.
            string json_request = JsonConvert.SerializeObject(request);

            // Need to clean up the args a bit.
            json_request = json_request.Replace("\\\"", "\"").Replace("\"args\":\"", "\"args\":").Replace("}\"}", "}}");

            // Send the request, store response.
            string json_response = Request.HttpPost(host, json_request);

            // Marshal the response and return it.
            return JsonConvert.DeserializeObject<API_Response>(json_response);
        }

        /// <summary>
        /// Set locking state on given repository by given user. If userid param is skipped , then it is set to id of user 
        /// whos calling this method. If locked param is skipped then function shows current lock state of given repo. This 
        /// command can be executed only using api_key belonging to user with admin rights or regular user that have admin 
        /// or write access to repository.
        /// </summary>
        /// 
        /// <param name="repo">reponame or repo_id.</param>
        /// <param name="user">username or user_id.</param>
        /// <param name="locked">Locked (true) | Unlocked (false)</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response lock_repo(string repo, string user, bool locked)
        {
            // Create a random ID.
            Random rand = new Random();
            int id = rand.Next(1, 1000);

            // Build the arguments
            lock_args args;
            args.repoid = repo;
            args.userid = user;
            args.locked = locked;

            // Create the request.
            API_Request request = new API_Request();
            request.id = id;
            request.method = "lock";
            request.api_key = api_key;
            request.args = args;

            // Serialize to JSON.
            string json_request = JsonConvert.SerializeObject(request);

            // Need to clean up the args a bit.
            json_request = json_request.Replace("\\\"", "\"").Replace("\"args\":\"", "\"args\":").Replace("}\"}", "}}");

            // Send the request, store response.
            string json_response = Request.HttpPost(host, json_request);

            // Marshal the response and return it.
            return JsonConvert.DeserializeObject<API_Response>(json_response);
        }




        //
        // All API call methods should exist here.

        /*
        lock
        show_ip
        get_user
        get_users
        create_user
        update_user
        delete_user
        get_users_group
        get_users_groups
        create_users_group
        add_user_to_users_group
        remove_user_from_users_group
        get_repo
        get_repos
        get_repo_nodes
        create_repo
        fork_repo
        delete_repo
        grant_user_permission
        revoke_user_permission
        grant_users_group_permission
        revoke_users_group_permission
        */
    }

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
    }

    /**
     * Arguments for the API calls.
     */
    struct pull_args { public string repoid; }
    struct rescan_args { public bool remove_obselete; }
    struct invalidate_args { public string repoid; }
    struct lock_args { public string repoid; public string userid; public bool locked; }

}
