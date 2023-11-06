using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPCommunication.Data;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Service.Services
{
    public class DataService
    {
        public List<Derece> getItems()
        {
            return new DataLayer().getItems();
        }
    }
}
