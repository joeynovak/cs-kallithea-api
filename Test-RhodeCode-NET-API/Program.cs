using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RhodeCode_NET_API;

namespace Test_RhodeCode_NET_API
{
    class Program
    {
        static void Main(string[] args)
        {
            RhodeCode wti = new RhodeCode("http://68.188.255.44:5000/_admin/api", "ff23a96518c15032ff7495eda7f5b149c0baf19b");
            RhodeCode local = new RhodeCode("http://192.168.1.126:5000/_admin/api", "ef3cee4198668321c372572e0f30d8df2c2e8a6f");

            API_Response test = wti.get_users();

            User[] temp = test.deserialize_get_users();

            Console.WriteLine("ID: " + test.id);
            //Console.WriteLine("Result: " + temp.permissions.repositories.Count);
            Console.WriteLine("Error: " + test.error);
            Console.WriteLine();
        }
    }
}
