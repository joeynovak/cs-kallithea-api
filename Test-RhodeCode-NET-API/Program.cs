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

            API_Response test = wti.lock_repo("369", "bjones", false);

            Console.WriteLine("ID: " + test.id);
            Console.WriteLine("Result: " + test.result);
            Console.WriteLine("Error: " + test.error);
            Console.WriteLine();
        }
    }
}
