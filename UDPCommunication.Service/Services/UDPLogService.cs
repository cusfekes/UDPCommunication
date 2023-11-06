using UDPCommunication.Data.Interfaces;
using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Service.Services
{
    public class UDPLogService : IUDPLogService
    {
        private readonly IUDPLogRepository _udpLogRepository;

        public UDPLogService(IUDPLogRepository udpLogRepository)
        {
            _udpLogRepository = udpLogRepository;
        }

        public OperationResult<List<UDPLog>> GetAllItems()
        {
            return _udpLogRepository.GetAllItems();
        }

        public OperationResult<bool> SaveItem(UDPLog item)
        {
            return _udpLogRepository.SaveItem(item);
        }

        public OperationResult<bool> DeleteItem(Guid itemId)
        {
            return _udpLogRepository.DeleteItem(itemId);
        }

        public OperationResult<List<UDPLog>> GetItemsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _udpLogRepository.GetItemsByDateRange(startDate, endDate);
        }
    }
}
