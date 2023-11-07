namespace UDPCommunication.Models.DomainModels
{
    public class UDPLog
    {
        public virtual Guid Id { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime LogDate { get; set; }

        public virtual string SourceIp { get; set; }

        public virtual int SourcePort { get; set; }

        public virtual string DestIp { get;set; }

        public virtual int DestPort { get; set; }

        public virtual string LogDirection { get; set; }

        public UDPLog()
        {
        }

        public UDPLog(string message, DateTime logDate, string sourceIp, int sourcePort, string destIp, int destPort, string logDirection)
        {
            Id = Guid.NewGuid();
            Message = message;
            LogDate = logDate;
            SourceIp = sourceIp;
            SourcePort = sourcePort;
            DestIp = destIp;
            DestPort = destPort;
            LogDirection = logDirection;
        }
    }
}
