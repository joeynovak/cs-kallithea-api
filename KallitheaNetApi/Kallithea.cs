using System;
using System.Collections.Generic;
using KallitheaNetApi.Responses;
using Newtonsoft.Json;

namespace KallitheaNetApi
{
    public class Kallithea
    {
        private readonly string _url;
        private readonly string _apiKey;

        /// <summary>
        /// Used to connect and execute API calls to a Kallithea server.
        /// </summary>
        ///
        /// <param name="apiKey">An API Key belonging to a user with admin privileges.</param>
        /// <param name="url">The location of the Kallithea server.  Include /_admin/api on the end.</param>
        public Kallithea(string url, string apiKey)
        {
            this._url = url;
            this._apiKey = apiKey;
        }

        /// <summary>
        /// This function is used to send the API_Request and return the API_Response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private ApiResponse<T> Call<T>(ApiRequest request)
      {
            // Create and set Request ID.
            Random rand = new Random();
            request.id = rand.Next(1, 1000);

            // Serialize to JSON.
            string jsonRequest = JsonConvert.SerializeObject(request);

            // Need to clean up the args a bit.
            jsonRequest = jsonRequest.Replace("\\\"", "\"").Replace("\"args\":\"", "\"args\":").Replace("}\"}", "}}");

            // Send the request, store response.
            string jsonResponse = Request.HttpPost(_url + "_admin/api", jsonRequest);

            // Deserialize the response and return it.
            return JsonConvert.DeserializeObject<ApiResponse<T>>(jsonResponse);
        }

        /// <summary>
        /// Pulls given repo from remote location. 
        /// Can be used to automatically keep remote repos up to date. 
        /// This command can be executed only using api_key belonging to user with admin rights
        /// </summary>
        /// 
        /// <param name="repo">reponame or repo_id.</param>
        /// <returns>A string containing results of operation.</returns>
        public ApiResponse<Pull> Pull(string repo)
        {
            // Build the arguments
            PullArgs args;
            args.repoid = repo;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "pull",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<Pull>(request);
        }

        /// <summary>
        /// Dispatch rescan repositories action. If remove_obsolete is set Kallithea will delete 
        /// repos that are in database but not in the filesystem. This command can be executed 
        /// only using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="remove_obselete">Used to delete repos that are in the database but not the filesystem.</param>
        /// <returns>A struct containing an array of added and removed repositories.</returns>
        public ApiResponse<RescanRepos> RescanRepos(bool remove_obselete = false)
        {
            // Build the arguments
            RescanArgs args;
            args.remove_obselete = remove_obselete;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "rescan_repos",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<RescanRepos>(request);
        }

        /// <summary>
        /// Invalidate cache for repository. This command can be executed only using api_key belonging to user 
        /// with admin rights or regular user that have write or admin or write access to repository.
        /// </summary>
        /// 
        /// <param name="repo">reponame or repo_id.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<InvalidateCache> invalidate_cache(string repo)
        {
            // Build the arguments
            InvalidateArgs args;
            args.repoid = repo;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "invalidate_cache",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<InvalidateCache>(request);
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
        public ApiResponse<GenericApiResponse> lock_repo(string repo, bool? locked = null, string user = "_apiuser_")
        {
            // Build arguments.
            LockArgs args;
            args.repoid = repo;
            args.userid = user;
            args.locked = locked;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "lock",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Shows IP address as seen from Kallithea server, together with all defined IP addresses 
        /// for given user. This command can be executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="user">username or user_id.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GetIp> GetIp(string user)
        {
            // Build the arguments
            GetIpArgs args;
            args.userid = user;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_ip",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GetIp>(request);
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
        public ApiResponse<GenericApiResponse> GetUser(string user = "_apiuser_")
        {
            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_user",
              api_key = _apiKey
           };

           if (user != "_apiuser_")
            {
                GetUserArgs args;
                args.userid = user;

                request.args = args;
            }

            // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Lists all existing users. This command can be executed only 
        /// using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> GetUsers()
        {
            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_users",
              api_key = _apiKey
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Creates new user. This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="create">The User_Create object to create a user.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> CreateUser(UserCreate create)
        {
            // Build the arguments
            CreateUserArgs args;
            args.active = create.Active;
            args.admin = create.Admin;
            args.email = create.Email;
            args.firstname = create.Firstname;
            args.lastname = create.Lastname;
            args.ldap_dn = create.LdapDn;
            args.password = create.Password;
            args.username = create.Username;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "create_user",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Updates given user if such user exists. 
        /// This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="update">The User_Create object to modify a user.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> UpdateUser(Requests.UserUpdate update)
        {
            // Build the arguments
           UpdateUserArgs args = new UpdateUserArgs
           {
              active = update.Active,
              admin = update.Admin,
              email = update.Email,
              firstname = update.Firstname,
              lastname = update.Lastname,
              ldap_dn = update.LdapDn,
              password = update.Password,
              username = update.Username,
              userid = update.Userid
           };

           // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "update_user",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Deletes given user if such user exists. 
        /// This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="user">The userid or username of the user to delete.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> DeleteUser(string user)
        {
            // Build the arguments
            DeleteUserArgs args;
            args.userid = user;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "delete_user",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Gets an existing user group. This command can 
        /// be executed only using api_key belonging to 
        /// user with admin rights.
        /// </summary>
        /// <param name="usergroup"></param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> get_user_group(string usergroup)
        {
            // Build the arguments
            GetUserGroupArgs args;
            args.usergroupid = usergroup;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_user_group",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Lists all existing user groups. This command 
        /// can be executed only using api_key belonging 
        /// to user with admin rights.
        /// </summary>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> get_user_groups()
        {
            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_user_groups",
              api_key = _apiKey
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Creates new user group. This command can be 
        /// executed only using api_key belonging to user 
        /// with admin rights.
        /// </summary>
        /// <param name="group_name">The name of the user group to be created.</param>
        /// <param name="active">If the group is active.  Default(True)</param>
        /// <param name="owner">The owner of the group.  Default(=apiuser)</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> create_user_group(string group_name, string owner = "_apiuser_", bool active = true)
        {
            // Build the arguments.
            CreateUserGroupArgs args = new CreateUserGroupArgs();
            args.group_name = group_name;
            args.active = active;
            args.owner = owner;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "create_user_group",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Adds a user to a user group. If user exists in that group 
        /// success will be false. This command can be executed only 
        /// using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="usergroup">user group id or name</param>
        /// <param name="user">user id or username</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> add_user_to_user_group(string usergroup, string user)
        {
            // Build the arguments
            EditUserGroupArgs args;
            args.usersgroupid = usergroup;
            args.userid = user;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "add_user_to_user_group",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Removes a user from a user group. If user is not in given group 
        /// success will be false. This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="usergroup">user group id or name</param>
        /// <param name="user">user id or username</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> remove_user_from_user_group(string usergroup, string user)
        {
            // Build the arguments
            EditUserGroupArgs args;
            args.usersgroupid = usergroup;
            args.userid = user;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "remove_user_from_user_group",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Gets an existing repository by it’s name or repository_id. 
        /// Members will return either users_group or user associated 
        /// to that repository. This command can be executed only using
        /// api_key belonging to user with admin rights or regular user 
        /// that have at least read access to repository.
        /// </summary>
        /// <param name="reponame">reponame or repo_id</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> get_repo(string repo)
        {
            // Build the arguments
            GetRepoArgs args;
            args.repoid = repo;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_repo",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Lists all existing repositories. This command can be 
        /// executed only using api_key belonging to user with 
        /// admin rights or regular user that have admin, write
        /// or read access to repository.
        /// </summary>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<List<GetReposRepository>> GetRepos()
        {
            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_repos",
              api_key = _apiKey
           };

           // Send the request and return response.
            return Call<List<GetReposRepository>>(request);
        }

        /// <summary>
        /// Returns a list of nodes and it’s children in a flat list for a given path 
        /// at a given revision. It’s possible to specify ret_type to show only files 
        /// or dirs. This command can be executed only using api_key belonging to user 
        /// with admin rights.
        /// </summary>
        /// <param name="reponame">The name or id of the repository.</param>
        /// <param name="revision">The revision of the repository.</param>
        /// <param name="root_path">The root path of the repository.</param>
        /// <param name="ret_type">The return type of the call. (Default: all)</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> get_repo_nodes(string repo, int revision, string root_path, string ret_type = "all")
        {
            // Build the arguments
            GetRepoNodesArgs args;
            args.repoid = repo;
            args.revision = revision;
            args.root_path = root_path;
            args.ret_type = ret_type;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "get_repo_nodes",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Creates a repository. If repository name contains “/”, all needed 
        /// repository groups will be created. For example “foo/bar/baz” will 
        /// create groups “foo”, “bar” (with “foo” as parent), and create “baz” 
        /// repository with “bar” as group. This command can be executed only 
        /// using api_key belonging to user with admin rights or regular user 
        /// that have create repository permission. Regular users cannot specify
        /// owner parameter.
        /// </summary>
        /// <param name="create">A Repository object to be created.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> CreateRepo(Repository create)
        {
            // Build arguments.
            CreateRepoArgs args;
            args.repo_name = create.repo_name;
            args.owner = create.owner;
            args.repo_type = create.repo_type;
            args.description = create.description;
            args.@private = create.@private;
            args.clone_uri = create.clone_uri;
            args.landing_rev = create.landing_rev;
            args.enable_downloads = create.enable_downloads;
            args.enable_locking = create.enable_locking;
            args.enable_statistics = create.enable_statistics;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "create_repo",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Creates a fork of given repo. In case of using celery this will immidiatelly return success 
        /// message, while fork is going to be created asynchronous. This command can be executed only 
        /// using api_key belonging to user with admin rights or regular user that have fork permission, 
        /// and at least read access to forking repository. Regular users cannot specify owner parameter.
        /// </summary>
        /// <param name="reponame">The reponame or repo_id of the repository to fork.</param>
        /// <param name="forkname">A name for the fork.</param>
        /// <param name="owner">Owner of the forked repository.  Default(=apiuser)</param>
        /// <param name="description">Description of the forked repository.</param>
        /// <param name="copy_permissions">Copy permissions from original repository.</param>
        /// <param name="private">Private repository visibility.</param>
        /// <param name="landing_rev">Revisions to display on statistics page.  (ex. 'tip')</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> fork_repo(string repo, string fork_name, string description, bool copy_permissions, bool @private, string landing_rev, string owner = "_apiuser_")
        {
            // Build arguments.
            ForkRepoArgs args;
            args.repoid = repo;
            args.fork_name = fork_name;
            args.description = description;
            args.copy_permissions = copy_permissions;
            args.@private = @private;
            args.landing_rev = landing_rev;
            args.owner = owner;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "fork_repo",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Deletes a repository. This command can be executed only using api_key belonging 
        /// to user with admin rights or regular user that have admin access to repository. 
        /// When forks param is set it’s possible to detach or delete forks of deleting repository.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository to delete.</param>
        /// <param name="forks">'delete' or 'detach' all forks.  Default(None)</param>
        /// <returns></returns>
        public ApiResponse<GenericApiResponse> delete_repo(string repo, string forks = null)
        {
            // Build arguments.
            DeleteRepoArgs args;
            args.repoid = repo;
            args.forks = forks;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "delete_repo",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Grant permission for user on given repository, or update existing one if found. 
        /// This command can be executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="user">The username or user_id of the user.</param>
        /// <param name="perm">repository.(none|read|write|admin) permissions to grant.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> grant_user_permission(string repo, string user, string perm)
        {
            // Build arguments.
            GrantUserPermissionArgs args;
            args.repoid = repo;
            args.userid = user;
            args.perm = perm;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "grant_user_permission",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Revoke permission for user on given repository. This command can be 
        /// executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="user">The username or user_id of the user.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> revoke_user_permission(string repo, string user)
        {
            // Build arguments.
            RevokeUserPermissionArgs args;
            args.repoid = repo;
            args.userid = user;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "revoke_user_permission",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Grant permission for user group on given repository, or update existing 
        /// one if found. This command can be executed only using api_key belonging 
        /// to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="usergroup">The user group id or name.</param>
        /// <param name="perm">repository.(none|read|write|admin) permissions to grant.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> grant_user_group_permission(string repo, string usergroup, string perm)
        {
            // Build arguments.
            GrantUserGroupPermissionArgs args;
            args.repoid = repo;
            args.usersgroupid = usergroup;
            args.perm = perm;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "grant_user_group_permission",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /// <summary>
        /// Revoke permission for user group on given repository.  
        /// This command can be executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="usergroup">The user group id or name.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public ApiResponse<GenericApiResponse> revoke_user_group_permission(string repo, string usergroup)
        {
            // Build arguments.
            RevokeUserGroupPermissionArgs args;
            args.repoid = repo;
            args.usersgroupid = usergroup;

            // Create the request.
           ApiRequest request = new ApiRequest
           {
              method = "revoke_user_group_permission",
              api_key = _apiKey,
              args = args
           };

           // Send the request and return response.
            return Call<GenericApiResponse>(request);
        }

        /*
         * Arguments for the API calls.
         */
        public struct PullArgs { public string repoid; }
       
        public struct RescanArgs { public bool remove_obselete; }
        
        public struct InvalidateArgs { public string repoid; }
       
        // Conditional Serialization
        public struct LockArgs 
        { 
            public string repoid; 
            public string userid; 
            public bool? locked;

            public bool ShouldSerializeuserid() { return userid != "_apiuser_"; }
            public bool ShouldSerializelocked() { return locked != null; }
        }
       
        public struct GetIpArgs { public string userid; }
        
        public struct GetUserArgs { public string userid; }
        
        public struct CreateUserArgs
        {
            public string username;
            public string email;
            public string password;
            public string firstname;
            public string lastname;
            public bool active;
            public bool admin;
            public string ldap_dn;
        }
        
        // Conditional Serialization
        public struct UpdateUserArgs
        {
            public int userid;
            public string username;
            public string email;
            public string password;
            public string firstname;
            public string lastname;
            public bool? active;
            public bool? admin;
            public string ldap_dn;

            public bool ShouldSerializeusername() { return username != null; }
            public bool ShouldSerializeemail() { return email != null; }
            public bool ShouldSerializepassword() { return password != null; }
            public bool ShouldSerializefirstname() { return firstname != null; }
            public bool ShouldSerializelastname() { return lastname != null; }
            public bool ShouldSerializeactive() { return active != null; }
            public bool ShouldSerializeadmin() { return admin != null; }
            public bool ShouldSerializeldap_dn() { return ldap_dn != null; }
        }

        public struct DeleteUserArgs { public string userid; }
        
        public struct GetUserGroupArgs { public string usergroupid; }
       
        // Conditional Serialization
        public struct CreateUserGroupArgs 
        { 
            public string group_name; 
            public string owner; 
            public bool? active;

            public bool ShouldSerializeowner() { return owner != "_apiuser_"; }
            public bool ShouldSerializeactive() { return active != null; }
        }
       
        public struct EditUserGroupArgs { public string usersgroupid; public string userid; }

      public struct GetRepoArgs { public string repoid; }

      public struct GetRepoNodesArgs { public string repoid; public int revision; public string root_path; public string ret_type; }

      // Conditional Serialization
      public struct CreateRepoArgs
        {
            public string repo_name;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string owner;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string repo_type;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string description;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public bool ? @private;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string clone_uri;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public string landing_rev;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public bool ? enable_downloads;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public bool ? enable_locking;

         [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
         public bool ? enable_statistics;            
        }

      // Conditional Serialization
      public struct ForkRepoArgs
        {
            public string repoid;
            public string fork_name;
            public string owner;
            public string description;
            public bool copy_permissions;
            public bool @private;
            public string landing_rev;

            public bool ShouldSerializeowner() { return owner != "_apiuser_"; }
        }

      // Conditional Serialization
      public struct DeleteRepoArgs
        {
            public string repoid;
            public string forks;

            public bool ShouldSerializeforks() { return forks != null; }
        }

      public struct GrantUserPermissionArgs { public string repoid; public string userid; public string perm; }

      public struct RevokeUserPermissionArgs { public string repoid; public string userid; }

      public struct GrantUserGroupPermissionArgs { public string repoid; public string usersgroupid; public string perm; }

      public struct RevokeUserGroupPermissionArgs { public string repoid; public string usersgroupid; }
    }

   public class GetReposResponse : RequestSpecificResponseData
   {
      public GetReposRepository[] Repositories;
   }

   public class GetReposRepository
   {
      public string repo_type;
      public string description;
      public bool @private;
      public RepositoryChangeset last_changeset;
      public string created_on;
      public string fork_of;
      public bool enabled_locking;
      public string owner;
      public string locked_date;
      public string[] landing_rev;
      public bool enable_downloads;
      public bool enable_statistics;
      public string locked_by;
      public string clone_uri;
      public int repo_id;
      public string repo_name;
   }

   public class GenericApiResponse : RequestSpecificResponseData
   {
   }

   public class InvalidateCache : RequestSpecificResponseData
   {
   }
}