using NHibernate;
using NHibernate.Cfg;
using System.Text;
using UDPCommunication.Data.Interfaces;
using UDPCommunication.Models;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data.Repository
{
    /// <summary>
    /// Database operations using hibernate framework
    /// </summary>
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
                        ISQLQuery query = session.CreateSQLQuery(Constants.DELETE_STATEMENT_QUERY).AddEntity(typeof(UDPLog));
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
                // Create user friendly exception message with technical information
                string message = CreateExceptionString(Constants.DB_ERROR, Constants.DB_DELETE_ERROR, ex.Message);
                result.SetFailureMode(message);
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
                        List<UDPLog> source = session.CreateSQLQuery(Constants.GET_ALL_STATEMENT_QUERY).AddEntity(typeof(UDPLog)).List<UDPLog>().ToList();
                        result.SetSuccessMode(source);
                    }
                }
                sessionFactory.Close();
            }
            catch (Exception ex)
            {
                // Create user friendly exception message with technical information
                string message = CreateExceptionString(Constants.DB_ERROR, Constants.DB_GET_ALL_ERROR, ex.Message);
                result.SetFailureMode(message);
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
                        ISQLQuery query = session.CreateSQLQuery(Constants.GET_BY_DATE_STATEMENT_QUERY).AddEntity(typeof(UDPLog));
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
                // Create user friendly exception message with technical information
                string message = CreateExceptionString(Constants.DB_ERROR, Constants.DB_GET_BY_DATE_RANGE_ERROR, ex.Message);
                result.SetFailureMode(message);
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
                        ISQLQuery query = session.CreateSQLQuery(Constants.INSERT_STATEMENT_QUERY).AddEntity(typeof(UDPLog));
                        query.SetParameter("Id", item.Id);
                        query.SetParameter("Message", item.Message);
                        query.SetParameter("LogDate", item.LogDate);
                        query.SetParameter("IpAddress", item.IpAddress);
                        query.SetParameter("PortNumber", item.PortNumber);
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
                // Create user friendly exception message with technical information
                string message = CreateExceptionString(Constants.DB_ERROR, Constants.DB_SAVE_ERROR, ex.Message);
                result.SetFailureMode(message);
            }
            return result;
        }

        private string CreateExceptionString(string header, string title, string body)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(header).Append(title).Append(body);
            return stringBuilder.ToString();
        }
    }
}
