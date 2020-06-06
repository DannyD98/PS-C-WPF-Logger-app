using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLogger
{
    public class LoggerModel
    {
        public LoggerModel(string Message, DateTime Date)
        {
            this.Message = Message;
            this.Date = Date;
        }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
