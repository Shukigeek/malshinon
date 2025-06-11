using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
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
        AlertTable Burst_Based = new AlertTable();
        PeopleTable peopleTable = new PeopleTable();
        ReportTable reportTable = new ReportTable();
        string allReport;

        public People GetName(string name)
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
            string firstName = string.Join(" ", fullName.Take(fullName.Length - 1));
            string lastName = fullName.Last();

            People reporter = peopleTable.SearchInPeopleTable(firstName,lastName, "reporter");
            int IdReporter = reporter.Id;
            Console.WriteLine(IdReporter);
            if (dal.IsType(IdReporter, "target"))
            {
                peopleTable.UpdateType(IdReporter, "both", reporter);
            }
            return reporter;
        }
        public People GetReport(string report)
        {
            //seconed question to reporter
            Console.WriteLine("enter your report...");
            //string fullReport = Console.ReadLine();
            //string[] fullReport2 = fullReport.Split();
            string fullReport = report;
            allReport = fullReport;
            string[] fullReport2 = report.Split(' ');

            //Assume names are always in Capitalized First and Last Name format (from instructions)
            string targetFirstName = "";
            string targetLastName = "";
            for (int i = 0; i < fullReport.Length; i++) 
            {
                if (char.IsUpper(fullReport2[i][0]))
                {
                    targetFirstName = fullReport2[i].ToLower();
                    targetLastName = fullReport2[i + 1].ToLower();
                    break;
                }
            }
            People target = peopleTable.SearchInPeopleTable(targetFirstName,targetLastName, "target");
            int IdTarget = target.Id;
            if (dal.IsType(IdTarget, "reporter"))
            {
                peopleTable.UpdateType(IdTarget, "both",target);
            }
            return target;
            
        }
        public void PogramFlow(string name,string report)
        {
            People reporter = GetName(name);
            People target = GetReport(report);
            reportTable.InsertReport(reporter.Id,target.Id, allReport);
            peopleTable.IncrementNumReports(reporter.Id, reporter);
            peopleTable.IncrementNumMentions(target.Id, target);
            if (thresholds.IsPotentialAgent(reporter.Id))
            {
                peopleTable.UpdateType(reporter.Id, "potential_agent", reporter);
                Console.WriteLine($"{reporter.FirstName} {reporter.LastName} is potiantial agent");
            }
            if (thresholds.ThreatAlert(target.Id))
            {
                Console.WriteLine($"NOTICE: {target.FirstName} {target.LastName} is a potential thret!\n~~~~~~~~~~ dangerous ~~~~~~~~~~~");
            }
            Burst_Based.BurstAlerts(target.Id);
        }
    }
}
