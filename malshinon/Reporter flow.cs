using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace malshinon
{
    internal class Reporter_flow
    {
        DAL dal = new DAL();
        Thresholds thresholds = new Thresholds();
        Burst_Based Burst_Based = new Burst_Based();
        public void Report(string name, string report)
        {
            //first question to reporter
            Console.WriteLine("Hi, please enter your full name.");
            //string[] fullName = Console.ReadLine().ToLower().Split(' ');
            string[] fullName = name.Split(' '); 
            while (fullName.Length < 2)
            {
                Console.WriteLine("please enter valid full name..");
                fullName = Console.ReadLine().ToLower().Split(' ');
            }
            
            People person = dal.SearchInPeopleTable(string.Join(" ", fullName.Take(fullName.Length - 1)), fullName.Last(), "reporter"); //
            //int IdReporter = dal.FindId(string.Join(" ", fullName.Take(fullName.Length - 1)), fullName.Last()); // 
            int IdReporter = person.Id;
            Console.WriteLine(IdReporter);
            if (dal.IsType(IdReporter, "target"))
                {
                    dal.UpdateType(IdReporter, "both");
                }

            //seconed question to reporter
            Console.WriteLine("enter your report...");
            //string fullReport = Console.ReadLine();
            //string[] fullReport1 = fullReport.Split();
            string fullReport = report;
            string[] fullReport1 = report.Split(' ');

            //Assume names are always in Capitalized First and Last Name format (from instructions)
            string targetFirstName = "";
            string targetLastName = "";
            for (int i = 0; i < fullReport.Length; i++) 
            {
                if (char.IsUpper(fullReport1[i][0]))
                {
                    targetFirstName = fullReport1[i].ToLower();
                    targetLastName = fullReport1[i + 1].ToLower();
                    break;
                }
            }
            People person2 = dal.SearchInPeopleTable(targetFirstName,targetLastName, "target");
            int IdTarget = person2.Id;
            if (dal.IsType(IdTarget, "reporter"))
            {
                dal.UpdateType(IdTarget, "both");
            }
            dal.InsertReport(IdReporter, IdTarget, fullReport);
            dal.IncrementNumReports(IdReporter);
            dal.IncrementNumMentions(IdTarget);
            if (thresholds.IsPotentialAgent(IdReporter))
            {
                dal.UpdateType(IdReporter, "potential_agent");
                Console.WriteLine($"{string.Join(" ",fullName)} is potiantial agent");
            }
            if (thresholds.ThreatAlert(IdTarget))
            {
                Console.WriteLine($"NOTICE: {targetFirstName} {targetLastName} is a potential thret!\n~~~~~~~~~~ dangerous ~~~~~~~~~~~");
            }
            Burst_Based.BurstAlerts(IdTarget);
        }
    }
}
