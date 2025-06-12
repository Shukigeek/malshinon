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
        public int Id {  get; set; }
        public int ReporterId {  get; set; }
        public int TargetId {  get; set; }
        public string Text {  get; set; }
        public DateTime Timestamp {  get; set; }

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
