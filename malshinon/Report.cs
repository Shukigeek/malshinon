using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class Report
    {
        public int Id;
        public int ReporterId;
        public int TargetId;
        public string Text;
        public DateTime Timestamp;

        public Report(int id ,int reporterId ,int targetId ,string text ,DateTime timeStamp) 
        {
            this.Id = id;
            this.ReporterId = reporterId;
            this.TargetId = targetId;
            this.Text = text;
            this.Timestamp = timeStamp;
        }
    }
}
