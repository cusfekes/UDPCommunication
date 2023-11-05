using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDPCommunication.UI
{
    public class UDPLogItem
    {
        public virtual Guid Id { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime LogDate { get; set; }

        public virtual string Ip { get; set; }

        public virtual int LogType { get; set; }
    }
}
