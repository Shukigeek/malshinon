using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class People
    {
        int Id;
        string FirstName;
        string LastName;
        string SecretCode;
        string Type;
        int NumReports;
        int NumMention;

        public People(string firstName,string lastName,
            string secretCode,string type)
        {
            
            FirstName = firstName;
            LastName = lastName;
            SecretCode = secretCode;
            Type = type;
            //NumReports = numReports;
            //NumMention = numMention;
        }
    }
}
