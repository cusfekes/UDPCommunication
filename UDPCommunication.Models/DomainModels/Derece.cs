using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommunication.Models.DomainModels
{
    public class Derece
    {
        public virtual Guid Id { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual string Tanim { get; set; }

        public virtual Guid CreatedBy { get; set; }

        public virtual DateTime CreatedAt { get; set; }
    }
}
