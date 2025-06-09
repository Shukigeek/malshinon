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
        string NumReports;
        string NumMention;

        public People(int id,string firstName,string lastName,
            string secretCode,string type,string numReports,string numMention)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SecretCode = secretCode;
            Type = type;
            NumReports = numReports;
            NumMention = numMention;
        }
    }
}
