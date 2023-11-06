using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPCommunication.Models.DomainModels;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Service.Services
{
    public class DereceService : IDereceService
    {
        public bool DeleteItem(Guid dereceId)
        {
            throw new NotImplementedException();
        }

        public List<Derece> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public List<Derece> GetItemsByDateRange(DateTime startDate, DateTime finishDate)
        {
            throw new NotImplementedException();
        }

        public bool SaveItem(Derece derece)
        {
            throw new NotImplementedException();
        }
    }
}
