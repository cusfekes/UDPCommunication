using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data.Interfaces
{
    public interface IDereceRepository
    {
        OperationResult<List<Derece>> GetAllItems();

        OperationResult<List<Derece>> GetItemsByDateRange(DateTime startDate, DateTime endDate);

        OperationResult<bool> SaveItem(Derece item);

        OperationResult<bool> DeleteItem(Guid itemId);
    }
}
