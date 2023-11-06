using NHibernate;
using NHibernate.Cfg;
using UDPCommunication.Data.Interfaces;
using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data.Repository
{
    public class UDPLogRepository : IUDPLogRepository
    {
        public OperationResult<bool> DeleteItem(Guid itemId)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            try
            {
                ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ISQLQuery query = session.CreateSQLQuery(@"delete from ""public"".""UDPLog"" where ""Id""=:Id").AddEntity(typeof(UDPLog));
                        query.SetParameter("Id", itemId);
                        int exec = query.ExecuteUpdate();
                        transaction.Commit();
                        result.SetSuccessMode(exec == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetFailureMode(ex);
            }
            return result;
        }

        public OperationResult<List<UDPLog>> GetAllItems()
        {
            OperationResult<List<UDPLog>> result = new OperationResult<List<UDPLog>>();
            try
            {
                ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        List<UDPLog> source = session.CreateSQLQuery(@"select * from ""public"".""UDPLog""").AddEntity(typeof(UDPLog)).List<UDPLog>().ToList();
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

        public OperationResult<List<UDPLog>> GetItemsByDateRange(DateTime startDate, DateTime endDate)
        {
            OperationResult<List<UDPLog>> result = new OperationResult<List<UDPLog>>();
            try
            {
                ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ISQLQuery query = session.CreateSQLQuery(@"select * from ""public"".""UDPLog"" where ""LogDate"">=:StartDate and ""LogDate""<=:EndDate").AddEntity(typeof(UDPLog));
                        query.SetParameter("StartDate", startDate);
                        query.SetParameter("EndDate", endDate);
                        List<UDPLog> source = query.List<UDPLog>().ToList();
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

        public OperationResult<bool> SaveItem(UDPLog item)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            try
            {
                ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        ISQLQuery query = session.CreateSQLQuery(@"insert into ""public"".""UDPLog""(""Id"", ""Message"", ""LogDate"", ""Ip"", ""LogDirection"")VALUES(:Id, :Message, :LogDate, :Ip, :LogDirection);").AddEntity(typeof(UDPLog));
                        query.SetParameter("Id", item.Id);
                        query.SetParameter("Message", item.Message);
                        query.SetParameter("LogDate", item.LogDate);
                        query.SetParameter("Ip", item.Ip);
                        query.SetParameter("LogDirection", item.LogDirection);
                        int exec = query.ExecuteUpdate();
                        transaction.Commit();
                        result.SetSuccessMode(exec == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetFailureMode(ex);
            }
            return result;
        }
    }
}
