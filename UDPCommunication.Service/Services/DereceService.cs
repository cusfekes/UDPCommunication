using UDPCommunication.Models.DomainModels;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Service.Services
{
    public class DereceService : IDereceService
    {
        private readonly IDereceService _dereceService;

        public DereceService(IDereceService dereceService)
        {
            _dereceService = dereceService;
        }

        public bool DeleteItem(Guid dereceId)
        {
            return _dereceService.DeleteItem(dereceId);
        }

        public List<Derece> GetAllItems()
        {
            return _dereceService.GetAllItems();
        }

        public List<Derece> GetItemsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dereceService.GetItemsByDateRange(startDate, endDate);
        }

        public bool SaveItem(Derece derece)
        {
            return _dereceService.SaveItem(derece);
        }
    }
}
