using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Service.Interfaces
{
    public interface IDereceService
    {
        List<Derece> GetAllItems();

        bool SaveItem(Derece item);

        bool DeleteItem(Guid itemId);

        List<Derece> GetItemsByDateRange(DateTime startDate, DateTime finishDate);
    }
}
