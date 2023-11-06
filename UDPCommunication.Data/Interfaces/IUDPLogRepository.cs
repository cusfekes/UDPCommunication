using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data.Interfaces
{
    public interface IUDPLogRepository
    {
        OperationResult<List<UDPLog>> GetAllItems();

        OperationResult<List<UDPLog>> GetItemsByDateRange(DateTime startDate, DateTime endDate);

        OperationResult<bool> SaveItem(UDPLog item);

        OperationResult<bool> DeleteItem(Guid itemId);
    }
}
