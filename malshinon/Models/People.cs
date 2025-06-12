using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class People
    {
        public int Id {  get; set; }
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public string SecretCode {  get; set; }
        public string Type {  get; set; }
        public int NumReports {  get; set; }
        public int NumMentions {  get; set; }

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
