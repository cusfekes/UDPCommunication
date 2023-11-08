using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;
using UDPCommunication.Models.Enums;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Test.UnitTests
{
    /// <summary>
    /// Defines unit test methods for UDP database transactions
    /// </summary>
    [TestClass]
    public class UDPLogServiceUnitTest : BaseUnitTest
    {
        private Guid udpId = Guid.NewGuid();

        [TestMethod]
        public void GetAllItemsTest()
        {
            IUDPLogService udpLogService = serviceProvider.GetRequiredService<IUDPLogService>();
            OperationResult<List<UDPLog>> result = udpLogService.GetAllItems();
            if (result.Success)
            {
                List<UDPLog> list = result.Result;
                Assert.IsNotNull(list);
            }
            Assert.Fail(result.Message);
        }

        [TestMethod]
        public void GetItemsByDateRangeTest()
        {
            IUDPLogService udpLogService = serviceProvider.GetRequiredService<IUDPLogService>();
            OperationResult<List<UDPLog>> result = udpLogService.GetItemsByDateRange(DateTime.Now.AddDays(-7), DateTime.Now);
            if (result.Success)
            {
                List<UDPLog> list = result.Result;
                Assert.IsNotNull(list);
            }
            Assert.Fail(result.Message);
        }

        [TestMethod]
        public void SaveItemTest()
        {
            IUDPLogService udpLogService = serviceProvider.GetRequiredService<IUDPLogService>();
            UDPLog item = new UDPLog();
            Guid id = udpId;
            item.Id = id;
            item.Message = "Unit Test Item";
            item.LogDate = DateTime.Now;
            item.IpAddress = "127.0.0.1";
            item.PortNumber = 9090;
            item.LogDirection = UDPOperationTypeEnum.Sent.ToString();
            OperationResult<bool> result = udpLogService.SaveItem(item);
            if (result.Success)
            {
                bool execution = result.Result;
                Assert.IsTrue(execution);
            }
            Assert.Fail(result.Message);
        }

        [TestMethod]
        public void DeleteItemTest()
        {
            IUDPLogService udpLogService = serviceProvider.GetRequiredService<IUDPLogService>();
            OperationResult<bool> result = udpLogService.DeleteItem(udpId);
            if (result.Success)
            {
                bool execution = result.Result;
                Assert.IsTrue(execution);
            }
            Assert.Fail(result.Message);
        }
    }
}
