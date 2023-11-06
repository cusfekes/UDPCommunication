using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data.Interfaces
{
    public interface IUDPRepository
    {
        OperationResult<List<Derece>> GetAllItems();

        List<Derece> GetItemsByDateRange(DateTime startDate, DateTime endDate);

        bool SaveItem(Derece item);

        bool DeleteItem(Guid itemId);
    }
}
