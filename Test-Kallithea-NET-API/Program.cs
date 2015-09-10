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
