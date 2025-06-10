using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Reporter_flow report = new Reporter_flow();
            for (int i = 0; i < 20; i++)
            {
                report.Report("dor perz","Achmed Yasiin hkjlhgjftdrspoiuyer,mlknbjhvgcfxm,nbvcxkojlihugyftdr,mnbcvxjlkfhdm.,nbvcxjkldfnkbjvhcgxdknjbvhcgxfmnbcvxjklgdfkjbvhcgxfjkhfgtdrsoiutru98y7t6re54nkjbvhcgxdmnb vchkgjfdsu98y7t6r5e4kjbvhcgxdjihugfydtr");
            }
            AnalysisMenu menu = new AnalysisMenu();
            menu.GetPotnetailAgents();
            menu.GetDangerousTargets();
        }
    }
}
