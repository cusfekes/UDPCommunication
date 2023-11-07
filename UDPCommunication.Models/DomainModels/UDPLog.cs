namespace UDPCommunication.Models.DomainModels
{
    public class UDPLog
    {
        public virtual Guid Id { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime LogDate { get; set; }

        public virtual string Ip { get; set; }

        public virtual string LogDirection { get; set; }

        public UDPLog()
        {
        }

        public UDPLog(string message, DateTime logDate, string ip, string logDirection)
        {
            Id = Guid.NewGuid();
            Message = message;
            LogDate = logDate;
            Ip = ip;
            LogDirection = logDirection;
        }
    }
}
