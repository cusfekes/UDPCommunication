using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPCommunication.Data.Interfaces;
using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data.Repository
{
    public class UDPRepository : IUDPRepository
    {
        public bool DeleteItem(Guid itemId)
        {
            throw new NotImplementedException();
        }

        public OperationResult<List<Derece>> GetAllItems()
        {
            OperationResult<List<Derece>> result = new OperationResult<List<Derece>>();
            try
            {
                var configuration = new Configuration();
                ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        List<Derece> source = session.CreateSQLQuery(@"select * from ""SIC"".""Derece""").AddEntity(typeof(Derece)).List<Derece>().ToList();
                        result.SetSuccessMode(source);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetFailureMode(ex);
            }
            return result;
        }

        public List<Derece> GetItemsByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public bool SaveItem(Derece item)
        {
            throw new NotImplementedException();
        }
    }
}
