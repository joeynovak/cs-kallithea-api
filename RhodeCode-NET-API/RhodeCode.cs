using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeCode_NET_API
{
    class RhodeCode
    {
        private string host;
        private string apikey;

        public RhodeCode(string host, string apikey)
        {
            this.host = host;
            this.apikey = apikey;
        }

        // All API call methods should exist here.
        //
        /* 
        pull
        rescan_repos
        invalidate_cache
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
}
