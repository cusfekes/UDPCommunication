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
                        ISQLQuery query = session.CreateSQLQuery(QueryConstants.DELETE_STATEMENT).AddEntity(typeof(UDPLog));
                        query.SetParameter("Id", itemId);
                        int exec = query.ExecuteUpdate();
                        transaction.Commit();
                        result.SetSuccessMode(exec == 1);
                    }
                }
                sessionFactory.Close();
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
                        List<UDPLog> source = session.CreateSQLQuery(QueryConstants.GET_ALL_STATEMENT).AddEntity(typeof(UDPLog)).List<UDPLog>().ToList();
                        result.SetSuccessMode(source);
                    }
                }
                sessionFactory.Close();
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
                        ISQLQuery query = session.CreateSQLQuery(QueryConstants.GET_BY_DATE_STATEMENT).AddEntity(typeof(UDPLog));
                        query.SetParameter("StartDate", startDate);
                        query.SetParameter("EndDate", endDate);
                        List<UDPLog> source = query.List<UDPLog>().ToList();
                        result.SetSuccessMode(source);
                    }
                }
                sessionFactory.Close();
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
                        ISQLQuery query = session.CreateSQLQuery(QueryConstants.INSERT_STATEMENT).AddEntity(typeof(UDPLog));
                        query.SetParameter("Id", item.Id);
                        query.SetParameter("Message", item.Message);
                        query.SetParameter("LogDate", item.LogDate);
                        query.SetParameter("SourceIp", item.SourceIp);
                        query.SetParameter("SourcePort", item.SourcePort);
                        query.SetParameter("DestIp", item.DestIp);
                        query.SetParameter("DestPort", item.DestPort);
                        query.SetParameter("LogDirection", item.LogDirection);
                        int exec = query.ExecuteUpdate();
                        transaction.Commit();
                        result.SetSuccessMode(exec == 1);
                    }
                }
                sessionFactory.Close();
            }
            catch (Exception ex)
            {
                result.SetFailureMode(ex);
            }
            return result;
        }
    }
}
