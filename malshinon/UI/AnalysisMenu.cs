using malshinon.UI;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class AnalysisMenu
    {
        DALconnction dal = new DALconnction();
        Reporter_flow reporter_Flow = new Reporter_flow();
        ManuFunctions functions = new ManuFunctions();
        
        public void Manu()
        {
            bool online = true;
            int num = 0;
            while (online) {
                Console.WriteLine("Analysis manu:\nchoose your option from 1-5");
                Console.WriteLine("1: add new report\n" +
                    "2: get potnetail agents\n" +
                    "3: get dangerous target\n" +
                    "4: get acvit alerts\n" +
                    "5: get person by name\n" +
                    "6: get person by secret code\n" +
                    "7: exit program");
                bool check = int.TryParse(Console.ReadLine(), out num);
                if (num < 1 || num > 7) Console.WriteLine("enter number between 1 - 7");
                if (check && num > 0 && num < 8) 
                {
                    Console.Clear();
                    switch (num)
                    {
                        case 1:
                            reporter_Flow.PogramFlow();
                            Console.ReadKey();
                            break;
                        case 2:
                            functions.GetPotnetailAgents();
                            Console.ReadKey();
                            break;
                        case 3:
                            functions.GetDangerousTargets();
                            Console.ReadKey();
                            break;
                        case 4:
                            functions.GetActiveAlert();
                            Console.ReadKey();
                            break;
                        case 5:
                            functions.GetPersonByName();
                            Console.ReadKey();
                            break;
                        case 6:
                            functions.GetPersonBySecretCode();
                            Console.ReadKey();
                            break;
                        case 7:
                            online = false;
                            Console.WriteLine("by!");
                            break;
                    }
                }
             }
        }


        
    }
}
