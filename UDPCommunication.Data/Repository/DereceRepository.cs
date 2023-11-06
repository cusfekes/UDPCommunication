using NHibernate;
using NHibernate.Cfg;
using UDPCommunication.Data.Interfaces;
using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data.Repository
{
    public class DereceRepository : IDereceRepository
    {
        public OperationResult<bool> DeleteItem(Guid itemId)
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

        public OperationResult<List<Derece>> GetItemsByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public OperationResult<bool> SaveItem(Derece item)
        {
            throw new NotImplementedException();
        }
    }
}
