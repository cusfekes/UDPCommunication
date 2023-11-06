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

        public bool save(Derece derece)
        {
            return new DataLayer().saveItem(derece);
        }

        public bool deleteItem(Guid dereceId)
        {
            return new DataLayer().deleteItem(dereceId);
        }

        public List<Derece> getByDateRange(DateTime startDate, DateTime finishDate)
        {
            return new DataLayer().getByDateRange(startDate, finishDate);
        }
    }
}
