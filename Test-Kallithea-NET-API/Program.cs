using System;
using System.Collections;
using System.Collections.Generic;
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
            Kallithea local = new Kallithea("http://192.168.1.7:5005/_admin/api", "d7652a661bbfd3129aaa7cdb1c7f6ae5207efecd");
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
                newUser.ldap_dn = "OU=Unit,DC=Test,DC=com";

                response = local.create_user(newUser);
                user_message userCreateResult = response.deserialize_create_user();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                updateUser.active = false;
                updateUser.admin = true;
                //updateUser.ldap_dn = "";
                updateUser.email = "test_unit@example.com";
                updateUser.firstname = "Test";
                updateUser.lastname = "Unit";

                response = local.update_user(updateUser);
                user_update updateUserResult = response.deserialize_update_user();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.get_user("UnitTest");
                User_Full getUserResult = response.deserialize_get_user();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.get_ip("UnitTest");
                get_ip getIPResult = response.deserialize_get_ip();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                User_Extended[] getUsersResult = response.deserialize_get_users();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                user_group_message userGroupCreateResult = response.deserialize_create_user_group();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.add_user_to_user_group("UnitTest Group", "UnitTest");
                response.deserialize_add_user_to_user_group();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                UserGroup_Full getUserGroupResult = response.deserialize_get_user_group();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                UserGroup[] getUserGroupsResult = response.deserialize_get_user_groups();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                newRepo.owner = "UnitTest";
                newRepo.@private = false;
                newRepo.repo_name = "UnitTest Repo";
                newRepo.repo_type = "hg";
                newRepo.landing_rev = "tip";

                response = local.create_repo(newRepo);
                repository_message repoCreateResult = response.deserialize_create_repo();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.fork_repo("UnitTest Repo", "UnitTest Repo_FORK", "UnitTest Repository Fork - Also Delete Me", true, false, "tip", "UnitTest");
                response.deserialize_fork_repo();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                Repository_Full getRepoResult = response.deserialize_get_repo();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                Console.WriteLine(response.result.ToString());

                Repository_All[] getReposResult = response.deserialize_get_repos();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.get_repo_nodes("UnitTest Repo", 0, "");
                Repository_Node[] getReposResult = response.deserialize_get_repo_nodes();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                string pullResult = response.deserialize_pull();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                rescan_repos rescanReposResult = response.deserialize_rescan_repos();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                string invalidateCacheResult = response.deserialize_invalidate_cache();

                currentError = response.error != "" ? "" : response.error.ToString();
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
                lock_repo lockResult = response.deserialize_lock();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.grant_user_permission("UnitTest Repo", "UnitTest", "repository.read");
                response.deserialize_grant_user_permission();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.grant_user_group_permission("UnitTest Repo", "UnitTest Group", "repository.read");
                response.deserialize_grant_user_group_permission();

                currentError = response.error != "" ? "" : response.error.ToString();
                unitTestResults.Add(currentTest, currentError);
            }
            catch (Exception e)
            {
                unitTestResults.Add(currentTest, e.Message.ToString());
            }


            // This is when everything is done and not undone.  Break here if needed to verify.
            try { } catch { }

            
            // Unit Test: Revoke User Group Permission
            //
            try
            {
                currentTest = "Revoke User Group Permission";

                response = local.revoke_user_group_permission("UnitTest Repo", "UnitTest Group");
                response.deserialize_revoke_user_group_permission();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.revoke_user_permission("UnitTest Repo", "UnitTest");
                response.deserialize_revoke_user_permission();

                currentError = response.error != "" ? "" : response.error.ToString();
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


            // Unit Test: Delete Repo
            //
            try
            {
                currentTest = "Delete Repo";

                response = local.delete_repo("delete", "delete");
                response.deserialize_delete_repo();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.remove_user_from_user_group("UnitTest Group", "UnitTest");
                response.deserialize_remove_user_from_user_group();

                currentError = response.error != "" ? "" : response.error.ToString();
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

                response = local.delete_user("UnitTest");
                user_message deleteUserResult = response.deserialize_delete_user();

                currentError = response.error != "" ? "" : response.error.ToString();
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

        }
    }
}
