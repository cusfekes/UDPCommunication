using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Service.Interfaces
{
    /// <summary>
    /// Interface for database operations of UDP transactions
    /// </summary>
    public interface IUDPLogService
    {
        OperationResult<List<UDPLog>> GetAllItems();

        OperationResult<bool> SaveItem(UDPLog item);

        OperationResult<bool> DeleteItem(Guid itemId);

        OperationResult<List<UDPLog>> GetItemsByDateRange(DateTime startDate, DateTime endDate);
    }
}
