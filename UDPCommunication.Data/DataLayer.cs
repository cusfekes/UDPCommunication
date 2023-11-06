using NHibernate;
using NHibernate.Cfg;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data
{
    public class DataLayer
    {
        public List<Derece> getItems()
        {
            var configuration = new NHibernate.Cfg.Configuration();
            ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            IList<Derece> logList = session.CreateSQLQuery(@"select * from ""SIC"".""Derece""").AddEntity(typeof(Derece)).List<Derece>();
            return logList.ToList();
        }
    }
}