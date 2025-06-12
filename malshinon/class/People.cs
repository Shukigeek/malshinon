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
        public int NumMentions;

        public People(int id, string firstName,string lastName,
            string secretCode,string type,int numReports,int numMentions)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SecretCode = secretCode;
            Type = type;
            NumReports = numReports;
            NumMentions = numMentions;
        }
    }
}
