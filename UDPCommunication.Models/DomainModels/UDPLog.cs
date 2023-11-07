namespace UDPCommunication.Models.DomainModels
{
    public class UDPLog
    {
        public virtual Guid Id { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime LogDate { get; set; }

        public virtual string IpAddress { get; set; }

        public virtual int PortNumber { get; set; }

        public virtual string LogDirection { get; set; }

        public UDPLog()
        {
        }

        public UDPLog(string message, DateTime logDate, string ipAddress, int portNumber, string logDirection)
        {
            Id = Guid.NewGuid();
            Message = message;
            LogDate = logDate;
            IpAddress = ipAddress;
            PortNumber = portNumber;
            LogDirection = logDirection;
        }
    }
}
