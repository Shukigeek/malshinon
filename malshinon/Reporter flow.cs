using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class Reporter_flow
    {
        public void Report()
        {
            Console.WriteLine("Hi, please enter your full name.");
            string[] fullName = Console.ReadLine().Split(' ');
        }
    }
}
