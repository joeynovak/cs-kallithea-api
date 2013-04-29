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
        /// This function is used to send teh API_Request and return the API_Response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private API_Response API_Call(API_Request request)
        {
            // Create and set Request ID.
            Random rand = new Random();
            request.id = rand.Next(1, 1000);

            // Serialize to JSON.
            string json_request = JsonConvert.SerializeObject(request);

            // Need to clean up the args a bit.
            json_request = json_request.Replace("\\\"", "\"").Replace("\"args\":\"", "\"args\":").Replace("}\"}", "}}");

            // Send the request, store response.
            string json_response = Request.HttpPost(host, json_request);

            // Deserialize the response and return it.
            return JsonConvert.DeserializeObject<API_Response>(json_response);
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
            // Build the arguments
            pull_args args;
            args.repoid = repo;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "pull";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
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
            // Build the arguments
            rescan_args args;
            args.remove_obselete = remove_obselete;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "rescan_repos";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
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
            // Build the arguments
            invalidate_args args;
            args.repoid = repo;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "invalidate_cache";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
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
            // Build the arguments
            lock_args args;
            args.repoid = repo;
            args.userid = user;
            args.locked = locked;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "lock";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Shows IP address as seen from RhodeCode server, together with all defined IP addresses 
        /// for given user. This command can be executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="user">username or user_id.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response show_ip(string user)
        {
            // Build the arguments
            show_ip_args args;
            args.userid = user;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "show_ip";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Get’s an user by username or user_id, Returns empty result if user is not found. 
        /// If userid param is skipped it is set to id of user who is calling this method. 
        /// This command can be executed only using api_key belonging to user with admin rights, 
        /// or regular users that cannot specify different userid than theirs.
        /// </summary>
        /// 
        /// <param name="user">username or user_id.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_user(string user)
        {
            // Build the arguments
            get_user_args args;
            args.userid = user;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_user";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Lists all existing users. This command can be executed only 
        /// using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_users()
        {
            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_users";
            request.api_key = api_key;

            // Send the request and return response.
            return API_Call(request);
        }


        /*
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
}
