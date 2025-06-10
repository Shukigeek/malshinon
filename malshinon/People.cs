using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class People
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public string SecretCode;
        public string Type;
        public int NumReports;
        public int NumMention;

        public People(int id, string firstName,string lastName,
            string secretCode,string type,int numReports,int numMention)
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
