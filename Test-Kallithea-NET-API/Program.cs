using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kallithea_NET_API;

namespace Test_Kallithea_NET_API
{
    class Program
    {
        /// <summary>
        /// This is the main run of the application.  It will perform all of the unit tests.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create a connection to the Kallithea server (in this case, HERMES).  API Key of KTAdmin.
            Kallithea local = new Kallithea("https://kallithea.winemantech.com/_admin/api", "d7652a661bbfd3129aaa7cdb1c7f6ae5207efecd");
            API_Response response = new API_Response();
            string currentTest = "", currentError = "";

            /*
                This application will run a unit test on all the API commands to Kallithea.
                It will report all the commands that were run and which ones passed/failed.

                This application can also be used as a reference for how to implement
                each API command and read data back from Kallithea.
            */

            // Used to store the Unit Test name and pass/fail metric.
            Dictionary<string, string> unitTestResults = new Dictionary<string, string>();

            // Variable used for cross-test communication.
            int uid = 0;
            int ugid = 0;
            int rgid = 0;



            // Unit Test: Create User
            //
            try
            {
                currentTest = "Create User";

                User_Create newUser = new User_Create();
                newUser.active = true;
                newUser.admin = false;
                newUser.email = "unit_test@example.com";
                newUser.firstname = "Unit";
                newUser.lastname = "Test";
                newUser.password = "MyPassword123";
                newUser.username = "UnitTest";
                newUser.extern_type = "ldap";
                newUser.extern_name = "DC=COM=COM";

                response = local.create_user(newUser);

                if (response.result == null)
                {
                    Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());
                }
                else
                {
                    Console.WriteLine(response.result.ToString());

                    user_message userCreateResult = response.deserialize_create_user();

                    // If command completed successfully, assert that returned values are as expected.
                    Debug.Assert(userCreateResult.user.active == newUser.active, "UserCreate.active assertion failed! Expected: '" + newUser.active.ToString() + "'  Actual'" + userCreateResult.user.active.ToString() + "'");
                    Debug.Assert(userCreateResult.user.admin == newUser.admin, "UserCreate.admin assertion failed! Expected: '" + newUser.admin.ToString() + "'  Actual'" + userCreateResult.user.admin.ToString() + "'");
                    Debug.Assert(userCreateResult.user.email == newUser.email, "UserCreate.email assertion failed! Expected: '" + newUser.email + "'  Actual'" + userCreateResult.user.email + "'");
                    Debug.Assert(userCreateResult.user.firstname == newUser.firstname, "UserCreate.firstname assertion failed! Expected: '" + newUser.firstname + "'  Actual'" + userCreateResult.user.firstname + "'");
                    Debug.Assert(userCreateResult.user.lastname == newUser.lastname, "UserCreate.lastname assertion failed! Expected: '" + newUser.lastname + "'  Actual'" + userCreateResult.user.lastname + "'");
                    Debug.Assert(userCreateResult.user.username == newUser.username, "UserCreate.username assertion failed! Expected: '" + newUser.username + "'  Actual'" + userCreateResult.user.username + "'");
                   
                    // Used for update_user command.
                    uid = userCreateResult.user.user_id;
                }

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Update User
            //
            try
            {
                currentTest = "Update User";

                User_Update updateUser = new User_Update();
                updateUser.userid = uid;
                updateUser.active = false;
                updateUser.admin = true;
                updateUser.email = "test_unit@example.com";
                updateUser.firstname = "Test";
                updateUser.lastname = "Unit";
                updateUser.extern_name = "DC=com";
                updateUser.extern_type = "ldap";
                updateUser.username = "TestUnit";
                updateUser.password = "qwertyasdf";

                response = local.update_user(updateUser);

                if (response.result == null)
                {
                    Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());
                }
                else
                {
                    Console.WriteLine(response.result.ToString());

                    user_update updateUserResult = response.deserialize_update_user();

                    // If command completed successfully, assert that returned values are as expected.
                    Debug.Assert(updateUserResult.user.active == updateUser.active, "UserUpdate.active assertion failed! Expected: '" + updateUser.active.ToString() + "'  Actual'" + updateUserResult.user.active.ToString() + "'");
                    Debug.Assert(updateUserResult.user.admin == updateUser.admin, "UserUpdate.admin assertion failed! Expected: '" + updateUser.admin.ToString() + "'  Actual'" + updateUserResult.user.admin.ToString() + "'");
                    Debug.Assert(updateUserResult.user.email == updateUser.email, "UserUpdate.email assertion failed! Expected: '" + updateUser.email + "'  Actual'" + updateUserResult.user.email + "'");
                    Debug.Assert(updateUserResult.user.firstname == updateUser.firstname, "UserUpdate.firstname assertion failed! Expected: '" + updateUser.firstname + "'  Actual'" + updateUserResult.user.firstname + "'");
                    Debug.Assert(updateUserResult.user.lastname == updateUser.lastname, "UserUpdate.lastname assertion failed! Expected: '" + updateUser.lastname + "'  Actual'" + updateUserResult.user.lastname + "'");
                    Debug.Assert(updateUserResult.user.username == updateUser.username, "UserUpdate.username assertion failed! Expected: '" + updateUser.username + "'  Actual'" + updateUserResult.user.username + "'");
                }

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get User
            //
            try
            {
                currentTest = "Get User";

                response = local.get_user(uid.ToString());

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                User_Full getUserResult = response.deserialize_get_user();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get IP
            //
            try
            {
                currentTest = "Get IP";

                response = local.get_ip(uid.ToString());

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                get_ip getIPResult = response.deserialize_get_ip();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get Users
            //
            try
            {
                currentTest = "Get Users";

                response = local.get_users();

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                User_Extended[] getUsersResult = response.deserialize_get_users();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Create User Group
            //
            try
            {
                currentTest = "Create User Group";

                response = local.create_user_group("UnitTest Group");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                user_group_message userGroupCreateResult = response.deserialize_create_user_group();

                ugid = userGroupCreateResult.user_group.users_group_id;

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Add User to User Group
            //
            try
            {
                currentTest = "Add User to User Group";

                response = local.add_user_to_user_group("UnitTest Group", "TestUnit");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_add_user_to_user_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }

            
            // Unit Test: Get User Group
            //
            try
            {
                currentTest = "Get User Group";

                response = local.get_user_group("UnitTest Group");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                UserGroup_Full getUserGroupResult = response.deserialize_get_user_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get User Groups
            //
            try
            {
                currentTest = "Get User Groups";

                response = local.get_user_groups();

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                UserGroup[] getUserGroupsResult = response.deserialize_get_user_groups();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Create Repo
            //
            try
            {
                currentTest = "Create Repo";

                Repository newRepo = new Repository();
                newRepo.description = "UnitTest Repository - Delete Me";
                newRepo.enable_downloads = true;
                newRepo.enable_locking = false;
                newRepo.enable_statistics = false;
                newRepo.owner = "TestUnit";
                newRepo.@private = false;
                newRepo.repo_name = "UnitTest Repo";
                newRepo.repo_type = "hg";
                newRepo.landing_rev = "rev:tip";
                newRepo.copy_permissions = false;

                response = local.create_repo(newRepo);

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                repository_message repoCreateResult = response.deserialize_create_repo();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Fork Repo
            //
            try
            {
                currentTest = "Fork Repo";

                response = local.fork_repo("UnitTest Repo", "UnitTest Repo_FORK", "UnitTest Repository Fork - Also Delete Me", true, false, "rev:tip", "TestUnit");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_fork_repo();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get Repo
            //
            try
            {
                currentTest = "Get Repo";

                response = local.get_repo("UnitTest Repo");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                Repository_Full getRepoResult = response.deserialize_get_repo();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get Repos
            //
            try
            {
                currentTest = "Get Repos";

                response = local.get_repos();

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString()); 

                Repository_All[] getReposResult = response.deserialize_get_repos();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get Repo Nodes
            //
            try
            {
                currentTest = "Get Repo Nodes";

                response = local.get_repo_nodes("UnitTest Repo", "rev:tip", "");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                Repository_Node[] getReposResult = response.deserialize_get_repo_nodes();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Pull
            //
            try
            {
                currentTest = "Pull";

                response = local.pull("UnitTest Repo");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                pull_message pullResult = response.deserialize_pull();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Rescan Repos
            //
            try
            {
                currentTest = "Rescan Repos";

                response = local.rescan_repos();

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                rescan_repos rescanReposResult = response.deserialize_rescan_repos();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Invalidate Cache
            //
            try
            {
                currentTest = "Invalidate Cache";

                response = local.invalidate_cache("UnitTest Repo");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                string invalidateCacheResult = response.deserialize_invalidate_cache();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Lock
            //
            try
            {
                currentTest = "Lock";

                response = local.lock_repo("UnitTest Repo");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                lock_repo lockResult = response.deserialize_lock();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Grant User Permission
            //
            try
            {
                currentTest = "Grant User Permission";

                response = local.grant_user_permission("UnitTest Repo", "TestUnit", "repository.read");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_grant_user_permission();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Grant User Group Permission
            //
            try
            {
                currentTest = "Grant User Group Permission";

                response = local.grant_user_group_permission("UnitTest Repo", "UnitTest Group", "repository.read", "all");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_grant_user_group_permission();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Create Repo Group
            //
            try
            {
                currentTest = "Create Repo Group";

                response = local.create_repo_group("UnitTest Repository Group", "UnitTest Repository Group - Delete Me");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                repository_group_message repogroupCreateResult = response.deserialize_create_repo_group();


                rgid = repogroupCreateResult.repo_group.group_id;

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }

            //
            // This still isn't working quite yet - will fix later.
            //
            // Unit Test: Update Repo Group
            //
            try
            {
                currentTest = "Update Repo Group";
           
                response = local.update_repo_group(rgid.ToString(), "TestUnit Repository Group", "TestUnit Repository Group - Delete Me", "TestUnit");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                repository_group_message repogroupUpdateResult = response.deserialize_update_repo_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get Repo Group
            //
            try
            {
                currentTest = "Get Repo Group";

                response = local.get_repo_group(rgid.ToString());

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                RepositoryGroup repogroupGetResult = response.deserialize_get_repo_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Get Repo Groups
            //
            try
            {
                currentTest = "Get Repo Groups";

                response = local.get_repo_groups();

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                RepositoryGroup[] repogroupsGetResult = response.deserialize_get_repo_groups();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Grant User Permissions To Repo Group
            //
            try
            {
                currentTest = "Grant User Permissions To Repo Group";

                response = local.grant_user_permission_to_repo_group(rgid.ToString(), "TestUnit", "group.admin");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response grantUserPermToRepoGroupResults = response.deserialize_grant_user_permissions_to_repo_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }




            // This is when everything is done and not undone.  Break here if needed to verify.
            while (false) ;




            // Unit Test: Revoke User Permissions To Repo Group
            //
            try
            {
                currentTest = "Revoke User Permissions From Repo Group";

                response = local.revoke_user_permission_from_repo_group(rgid.ToString(), "TestUnit");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response revokeUserPermFromRepoGroupResults = response.deserialize_revoke_user_permissions_from_repo_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Revoke User Group Permission
            //
            try
            {
                currentTest = "Revoke User Group Permission";

                response = local.revoke_user_group_permission("UnitTest Repo", "UnitTest Group", "none");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_revoke_user_group_permission();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Revoke User Permission
            //
            try
            {
                currentTest = "Revoke User Permission";

                response = local.revoke_user_permission("UnitTest Repo", "TestUnit");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_revoke_user_permission();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }

            //
            //
            //
            //
            //
            //
            // UPDATE REPO HERE


            // Unit Test: Delete Repo Group
            //
            try
            {
                currentTest = "Delete Repo Group";

                response = local.delete_repo_group(rgid.ToString());

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                repository_group_message repogroupDeleteResult = response.deserialize_delete_repo_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Delete Repo
            //
            try
            {
                currentTest = "Delete Repo";

                response = local.delete_repo("UnitTest Repo", "delete");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_delete_repo();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // Unit Test: Remove User from User Group
            //
            try
            {
                currentTest = "Remove User from User Group";

                response = local.remove_user_from_user_group("UnitTest Group", "TestUnit");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_remove_user_from_user_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }

            // Unit Test: Delete User Group
            //
            try
            {
                currentTest = "Delete User Group";

                response = local.delete_user_group(ugid);

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                response.deserialize_delete_user_group();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }

            // Unit Test: Delete User
            //
            try
            {
                currentTest = "Delete User";

                response = local.delete_user("TestUnit");

                if (response.result != null) Console.WriteLine(response.result.ToString()); else Console.WriteLine("Error running " + currentTest + " test: " + response.error.ToString());

                user_message deleteUserResult = response.deserialize_delete_user();

                if (response.error != null) currentError = response.error.ToString(); else currentError = "";
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            Console.WriteLine("Unit tests have completed!  Here are the results:" + Environment.NewLine + Environment.NewLine);

            foreach (KeyValuePair<string,string> testResult in unitTestResults)
            {
                Console.WriteLine("Test Name: " + testResult.Key.ToString()
                                                + Environment.NewLine +
                                  "Pass: " + (testResult.Value == "").ToString()
                                                + Environment.NewLine +
                                  "Error:  " + Environment.NewLine
                                                + testResult.Value.ToString()
                                                + Environment.NewLine);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
