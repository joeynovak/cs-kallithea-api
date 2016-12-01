using System;
using Newtonsoft.Json;

namespace Kallithea_NET_API
{
    public class Kallithea
    {
        private string host;
        private string api_key;

        /// <summary>
        /// Used to connect and execute API calls to a Kallithea server.
        /// </summary>
        ///
        /// <param name="api_key">An API Key belonging to a user with admin privileges.</param>
        /// <param name="host">The location of the Kallithea server.  Include /_admin/api on the end.</param>
        public Kallithea(string host, string api_key)
        {
            this.host = host;
            this.api_key = api_key;
        }

        /// <summary>
        /// This function is used to send the API_Request and return the API_Response.
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
        /// <returns>A string containing results of operation.</returns>
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
        /// Dispatch rescan repositories action. If remove_obsolete is set Kallithea will delete 
        /// repos that are in database but not in the filesystem. This command can be executed 
        /// only using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="remove_obselete">Used to delete repos that are in the database but not the filesystem.</param>
        /// <returns>A struct containing an array of added and removed repositories.</returns>
        public API_Response rescan_repos(bool remove_obselete = false)
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
        public API_Response lock_repo(string repo, bool? locked = null, string user = "_apiuser_")
        {
            // Build arguments.
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
        /// Shows IP address as seen from Kallithea server, together with all defined IP addresses 
        /// for given user. This command can be executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="user">username or user_id.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_ip(string user)
        {
            // Build the arguments
            get_ip_args args;
            args.userid = user;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_ip";
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
        public API_Response get_user(string user = "_apiuser_")
        {
            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_user";
            request.api_key = api_key;

            if (user != "_apiuser_")
            {
                get_user_args args;
                args.userid = user;

                request.args = args;
            }

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

        /// <summary>
        /// Creates new user. This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="create">The User_Create object to create a user.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response create_user(User_Create create)
        {
            // Build the arguments
            create_user_args args;
            args.active = create.active;
            args.admin = create.admin;
            args.email = create.email;
            args.firstname = create.firstname;
            args.lastname = create.lastname;
            args.extern_type = create.extern_type;
            args.extern_name = create.extern_name;
            args.password = create.password;
            args.username = create.username;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "create_user";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Updates given user if such user exists. 
        /// This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// 
        /// <param name="update">The User_Create object to modify a user.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response update_user(User_Update update)
        {
            // Build the arguments
            update_user_args args = new update_user_args();
            args.active = update.active;
            args.admin = update.admin;
            args.email = update.email;
            args.firstname = update.firstname;
            args.lastname = update.lastname;
            args.extern_type = update.extern_type;
            args.extern_name = update.extern_name;
            args.password = update.password;
            args.username = update.username;
            args.userid = update.userid;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "update_user";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Deletes given user if such user exists. 
        /// This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="user">The userid or username of the user to delete.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response delete_user(string user)
        {
            // Build the arguments
            delete_user_args args;
            args.userid = user;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "delete_user";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Gets an existing user group. This command can 
        /// be executed only using api_key belonging to 
        /// user with admin rights.
        /// </summary>
        /// <param name="usergroup"></param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_user_group(string usergroup)
        {
            // Build the arguments
            get_user_group_args args;
            args.usergroupid = usergroup;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_user_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Lists all existing user groups. This command 
        /// can be executed only using api_key belonging 
        /// to user with admin rights.
        /// </summary>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_user_groups()
        {
            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_user_groups";
            request.api_key = api_key;

            // Send the request and return response.
            return API_Call(request);
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
        public API_Response create_user_group(string group_name, string owner = "_apiuser_", bool active = true)
        {
            // Build the arguments.
            create_user_group_args args = new create_user_group_args();
            args.group_name = group_name;
            args.active = active;
            args.owner = owner;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "create_user_group";
            request.api_key = api_key;
            request.args = args;
         
            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Deletes a user group. This command can be 
        /// executed only using api_key belonging to user 
        /// with admin rights.
        /// </summary>
        /// <param name="usergroupid">The name of the user group to be deleted.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response delete_user_group(int usergroupid)
        {
            // Build the arguments.
            delete_user_group_args args = new delete_user_group_args();
            args.usergroupid = usergroupid;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "delete_user_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Adds a user to a user group. If user exists in that group 
        /// success will be false. This command can be executed only 
        /// using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="usergroup">user group id or name</param>
        /// <param name="user">user id or username</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response add_user_to_user_group(string usergroup, string user)
        {
            // Build the arguments
            edit_user_group_args args;
            args.usergroupid = usergroup;
            args.userid = user;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "add_user_to_user_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Removes a user from a user group. If user is not in given group 
        /// success will be false. This command can be executed only using 
        /// api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="usergroup">user group id or name</param>
        /// <param name="user">user id or username</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response remove_user_from_user_group(string usergroup, string user)
        {
            // Build the arguments
            edit_user_group_args args;
            args.usergroupid = usergroup;
            args.userid = user;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "remove_user_from_user_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
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
        public API_Response get_repo(string repo)
        {
            // Build the arguments
            get_repo_args args;
            args.repoid = repo;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_repo";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Lists all existing repositories. This command can be 
        /// executed only using api_key belonging to user with 
        /// admin rights or regular user that have admin, write
        /// or read access to repository.
        /// </summary>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_repos()
        {
            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_repos";
            request.api_key = api_key;

            // Send the request and return response.
            return API_Call(request);
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
        public API_Response get_repo_nodes(string repo, string revision, string root_path, string ret_type = "all")
        {
            // Build the arguments
            get_repo_nodes_args args;
            args.repoid = repo;
            args.revision = revision;
            args.root_path = root_path;
            args.ret_type = ret_type;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_repo_nodes";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
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
        public API_Response create_repo(Repository create)
        {
            // Build arguments.
            create_repo_args args;
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
            args.copy_permissions = create.copy_permissions;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "create_repo";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
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
        public API_Response fork_repo(string repo, string fork_name, string description, bool copy_permissions, bool @private, string landing_rev, string owner = "apiuser")
        {
            // Build arguments.
            fork_repo_args args;
            args.repoid = repo;
            args.fork_name = fork_name;
            args.description = description;
            args.copy_permissions = copy_permissions;
            args.@private = @private;
            args.landing_rev = landing_rev;
            args.owner = owner;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "fork_repo";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Deletes a repository. This command can be executed only using api_key belonging 
        /// to user with admin rights or regular user that have admin access to repository. 
        /// When forks param is set it’s possible to detach or delete forks of deleting repository.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository to delete.</param>
        /// <param name="forks">'delete' or 'detach' all forks.  Default(None)</param>
        /// <returns></returns>
        public API_Response delete_repo(string repo, string forks = null)
        {
            // Build arguments.
            delete_repo_args args;
            args.repoid = repo;
            args.forks = forks;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "delete_repo";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Grant permission for user on given repository, or update existing one if found. 
        /// This command can be executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="user">The username or user_id of the user.</param>
        /// <param name="perm">repository.(none|read|write|admin) permissions to grant.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response grant_user_permission(string repo, string user, string perm)
        {
            // Build arguments.
            grant_user_permission_args args;
            args.repoid = repo;
            args.userid = user;
            args.perm = perm;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "grant_user_permission";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Revoke permission for user on given repository. This command can be 
        /// executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="user">The username or user_id of the user.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response revoke_user_permission(string repo, string user)
        {
            // Build arguments.
            revoke_user_permission_args args;
            args.repoid = repo;
            args.userid = user;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "revoke_user_permission";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Grant permission for user group on given repository, or update existing 
        /// one if found. This command can be executed only using api_key belonging 
        /// to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="usergroupid">The user group id or name.</param>
        /// <param name="perm">repository.(none|read|write|admin) permissions to grant.</param>
        /// <param name="apply_to_children">Apply permissions to children of option 'none', 'repos', 'groups', 'all'.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response grant_user_group_permission(string repo, string usergroup, string perm, string apply_to_children = "none")
        {
            // Build arguments.
            grant_user_group_permission_args args;
            args.repoid = repo;
            args.usergroupid = usergroup;
            args.perm = perm;
            args.apply_to_children = apply_to_children;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "grant_user_group_permission";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Revoke permission for user group on given repository.  
        /// This command can be executed only using api_key belonging to user with admin rights.
        /// </summary>
        /// <param name="repo">The reponame or repo_id of the repository.</param>
        /// <param name="usergroup">The user group id or name.</param>
        /// <param name="apply_to_children">Apply permissions to children of option 'none', 'repos', 'groups', 'all'.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response revoke_user_group_permission(string repo, string usergroup, string apply_to_children = "none")
        {
            // Build arguments.
            revoke_user_group_permission_args args;
            args.repoid = repo;
            args.usergroupid = usergroup;
            args.apply_to_children = apply_to_children;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "revoke_user_group_permission";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Creates a repository group. Group name can have / in group_name. For example “foo/bar/baz” 
        /// will create group baz, with foo/bar as parent This command can be executed only using 
        /// api_key belonging to user with admin rights or user who has create repogroup permission.
        /// </summary>
        /// <param name="group_name">The name of the repo group to be created.</param>
        /// <param name="description">A description for the repo group being created. (Optional - Default is group_name)</param>
        /// <param name="owner">The owner of the newly created repo group.  (Optional - Default is _apiuser_)</param>
        /// <param name="parent">The name of the parent repo group.  (Optional - Default is null)</param>
        /// <param name="copy_permissions">Whether to copy permissions from parent repo group.  (Optional - Default is false)</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response create_repo_group(string group_name, string description = null, string owner = null, string parent = null, bool? copy_permissions = null)
        {
            // Build arguments.
            create_repo_group_args args;
            args.group_name = group_name;
            args.description = description;
            args.owner = owner;
            args.parent = parent;
            args.copy_permissions = copy_permissions;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "create_repo_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Updates a repository group.  This command can be executed only using 
        /// api_key belonging to user with admin rights or user who has create 
        /// repogroup permission.
        /// </summary>
        /// <param name="repogroupid">The name or id of the repo group to be updated.</param>
        /// <param name="group_name">The name of the repo group to be updated.  (Optional)</param>
        /// <param name="description">A description for the repo group being updated.  (Optional)</param>
        /// <param name="owner">The owner of the repo group.  (Optional)</param>
        /// <param name="parent">The name of the parent repo group.  (Optional)</param>
        /// <param name="copy_permissions">Whether to copy permissions from parent repo group.  (Optional)</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response update_repo_group(string repogroupid, string group_name = null, string description = null, string owner = null, string parent = null, bool? copy_permissions = null, bool? enable_locking = null)
        {
            // Build arguments.
            update_repo_group_args args;
            args.repogroupid = repogroupid;
            args.group_name = group_name;
            args.description = description;
            args.owner = owner;
            args.parent = parent;
            args.copy_permissions = copy_permissions;
            args.enable_locking = enable_locking;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "update_repo_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Deletes a repository group.  This command can be executed only using 
        /// api_key belonging to user with admin rights or user who has create 
        /// repogroup permission.
        /// </summary>
        /// <param name="repogroupid">The name or id of the repo group to be updated.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response delete_repo_group(string repogroupid)
        {
            // Build arguments.
            delete_repo_group_args args;
            args.repogroupid = repogroupid;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "delete_repo_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Returns a repository group.  This command can be executed only using 
        /// api_key belonging to user with admin rights or user who has create 
        /// repogroup permission.
        /// </summary>
        /// <param name="repogroupid">The name or id of the repo group to be returned.</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_repo_group(string repogroupid)
        {
            // Build arguments.
            get_repo_group_args args;
            args.repogroupid = repogroupid;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_repo_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Returns all repository groups.  This command can be executed only using 
        /// api_key belonging to user with admin rights or user who has create 
        /// repogroup permission.
        /// </summary>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response get_repo_groups()
        {
            // Create the request.
            API_Request request = new API_Request();
            request.method = "get_repo_groups";
            request.api_key = api_key;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Grants a user permission to a repo group. This command can be executed only using 
        /// api_key belonging to user with admin rights or user who has create 
        /// repogroup permission.
        /// </summary>
        /// <param name="repogroupid">The name or id of the repo group to grant permissions to.</param>
        /// <param name="userid">The name or id of the user to grant permissions to.</param>
        /// <param name="perm">The level of permissions to grant.  (group.(none|read|write|admin))</param>
        /// <param name="apply_to_children">Whether to apply these updates to children of specified type. (none|repos|groups|all)</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response grant_user_permission_to_repo_group(string repogroupid, string userid, string perm, string apply_to_children = "none")
        {
            // Build arguments.
            grant_user_permission_to_repo_group_args args;
            args.repogroupid = repogroupid;
            args.userid = userid;
            args.perm = perm;
            args.apply_to_children = apply_to_children;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "grant_user_permission_to_repo_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }

        /// <summary>
        /// Revokes all user permissions to a repo group. This command can be executed only using 
        /// api_key belonging to user with admin rights or user who has create 
        /// repogroup permission.
        /// </summary>
        /// <param name="repogroupid">The name or id of the repo group to grant permissions to.</param>
        /// <param name="userid">The name or id of the user to grant permissions to.</param>
        /// <param name="apply_to_children">Whether to apply these updates to children of specified type. (none|repos|groups|all)</param>
        /// <returns>API_Response containing results of operation.</returns>
        public API_Response revoke_user_permission_from_repo_group(string repogroupid, string userid, string apply_to_children = "none")
        {
            // Build arguments.
            revoke_user_permission_from_repo_group_args args;
            args.repogroupid = repogroupid;
            args.userid = userid;
            args.apply_to_children = apply_to_children;

            // Create the request.
            API_Request request = new API_Request();
            request.method = "revoke_user_permission_from_repo_group";
            request.api_key = api_key;
            request.args = args;

            // Send the request and return response.
            return API_Call(request);
        }


        /*
         * Arguments for the API calls.
         */
        private struct pull_args { public string repoid; }
       
        private struct rescan_args { public bool remove_obselete; }
        
        private struct invalidate_args { public string repoid; }
       
        // Conditional Serialization
        private struct lock_args 
        { 
            public string repoid; 
            public string userid; 
            public bool? locked;

            public bool ShouldSerializeuserid() { return userid != "_apiuser_"; }
            public bool ShouldSerializelocked() { return locked != null; }
        }
       
        private struct get_ip_args { public string userid; }
        
        private struct get_user_args { public string userid; }
        
        private struct create_user_args
        {
            public string username;
            public string email;
            public string password;
            public string firstname;
            public string lastname;
            public bool? active;
            public bool? admin;
            public string extern_name;
            public string extern_type;

            public bool ShouldSerializepassword() { return password != ""; }
            public bool ShouldSerializefirstname() { return firstname != ""; }
            public bool ShouldSerializelastname() { return lastname != ""; }
            public bool ShouldSerializeactive() { return active != true; }
            public bool ShouldSerializeadmin() { return admin != false; }
            public bool ShouldSerializeextern_name() { return extern_name != "internal"; }
            public bool ShouldSerializeextern_type() { return extern_type != "internal"; }

        }
        
        // Conditional Serialization
        private struct update_user_args
        {
            public int userid;
            public string username;
            public string email;
            public string password;
            public string firstname;
            public string lastname;
            public bool? active;
            public bool? admin;
            public string extern_name;
            public string extern_type;

            public bool ShouldSerializeusername() { return username != null; }
            public bool ShouldSerializeemail() { return email != null; }
            public bool ShouldSerializepassword() { return password != null; }
            public bool ShouldSerializefirstname() { return firstname != null; }
            public bool ShouldSerializelastname() { return lastname != null; }
            public bool ShouldSerializeactive() { return active != null; }
            public bool ShouldSerializeadmin() { return admin != null; }
            public bool ShouldSerializeextern_name() { return extern_name != null; }
            public bool ShouldSerializeextern_type() { return extern_type != null; }
        }

        private struct delete_user_args { public string userid; }
        
        private struct get_user_group_args { public string usergroupid; }
       
        // Conditional Serialization
        private struct create_user_group_args 
        { 
            public string group_name; 
            public string owner; 
            public bool? active;

            public bool ShouldSerializeowner() { return owner != "_apiuser_"; }
            public bool ShouldSerializeactive() { return active != null; }
        }

        private struct delete_user_group_args
        {
            public int usergroupid;

        }

        private struct edit_user_group_args { public string usergroupid; public string userid; }
       
        private struct get_repo_args { public string repoid; }
       
        private struct get_repo_nodes_args { public string repoid; public string revision; public string root_path; public string ret_type; }
        
        // Conditional Serialization
        private struct create_repo_args
        {
            public string repo_name;
            public string owner;
            public string repo_type;
            public string description;
            public bool @private;
            public string clone_uri;
            public string landing_rev;
            public bool enable_downloads;
            public bool enable_locking;
            public bool enable_statistics;
            public bool copy_permissions;

            public bool ShouldSerializeowner() { return owner != "apiuser"; }
            public bool ShouldSerializeclone_uri() { return clone_uri != null; }
        }
       
        // Conditional Serialization
        private struct fork_repo_args
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
        private struct delete_repo_args
        {
            public string repoid;
            public string forks;

            public bool ShouldSerializeforks() { return forks != null; }
        }

        private struct grant_user_permission_args { public string repoid; public string userid; public string perm; }

        private struct revoke_user_permission_args { public string repoid; public string userid; }

        private struct grant_user_group_permission_args
        {
            public string repoid;
            public string usergroupid;
            public string perm;
            public string apply_to_children;
        }

        private struct revoke_user_group_permission_args
        {
            public string repoid;
            public string usergroupid;
            public string apply_to_children;
        }

        // Conditional Serialization
        private struct create_repo_group_args
        {
            public string group_name;
            public string description;
            public string owner;
            public string parent;
            public bool? copy_permissions;

            public bool ShouldSerializedescription() { return description != null; }
            public bool ShouldSerializeowner() { return owner != null; }
            public bool ShouldSerializeparent() { return parent != null; }
            public bool ShouldSerializecopy_permissions() { return copy_permissions != null; }
        }

        // Conditional Serialization
        private struct update_repo_group_args
        {
            public string repogroupid;
            public string group_name;
            public string description;
            public string owner;
            public string parent;
            public bool? copy_permissions;
            public bool? enable_locking;

            public bool ShouldSerializegroup_name() { return group_name != null; }
            public bool ShouldSerializedescription() { return description != null; }
            public bool ShouldSerializeowner() { return owner != null; }
            public bool ShouldSerializeparent() { return parent != null; }
            public bool ShouldSerializecopy_permissions() { return copy_permissions != null; }
            public bool ShouldSerializeenable_locking() { return enable_locking != null; }
        }

        private struct delete_repo_group_args
        {
            public string repogroupid;
        }

        private struct get_repo_group_args
        {
            public string repogroupid;
        }

        private struct grant_user_permission_to_repo_group_args
        {
            public string repogroupid;
            public string userid;
            public string perm;
            public string apply_to_children;
        }

        private struct revoke_user_permission_from_repo_group_args
        {
            public string repogroupid;
            public string userid;
            public string apply_to_children;
        }
    }
}