using NHibernate;
using NHibernate.Cfg;
using NHibernate.Id.Insert;
using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Data
{
    public class DataLayer
    {
        public List<UDPLog> getItems()
        {
            var configuration = new NHibernate.Cfg.Configuration();
            ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            IList<UDPLog> logList = session.CreateSQLQuery(@"select * from ""public"".""UDPLog""").AddEntity(typeof(UDPLog)).List<UDPLog>();
            return logList.ToList();
        }

        public bool saveItem(Derece derece)
        {
            var configuration = new NHibernate.Cfg.Configuration();
            ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            
            ISQLQuery query = session.CreateSQLQuery(@"insert into ""SIC"".""Derece""(""Id"", ""IsActive"", ""CreatedBy"", ""CreatedAt"", ""Tanim"")VALUES(:Id, false, :CreatedBy, :CreatedAt, :Tanim);").AddEntity(typeof(Derece));
            query.SetParameter("Id", derece.Id);
            query.SetParameter("CreatedBy", derece.CreatedBy);
            query.SetParameter("CreatedAt", derece.CreatedAt);
            query.SetParameter("Tanim", derece.Tanim);

            int a = query.ExecuteUpdate();
            transaction.Commit();
            session.Close();
            transaction.Dispose();
            return a == 1;
        }

        public bool deleteItem(Guid dereceId)
        {
            var configuration = new NHibernate.Cfg.Configuration();
            ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            ISQLQuery query = session.CreateSQLQuery(@"delete from ""SIC"".""Derece"" where ""Id""=:Id").AddEntity(typeof(Derece));
            query.SetParameter("Id", dereceId);
            int a = query.ExecuteUpdate();
            transaction.Commit();
            session.Close();
            transaction.Dispose();
            if(a == 1)
                return true;
            return false;
        }

        public List<Derece> getByDateRange(DateTime startDate, DateTime finishDate)
        {
            var configuration = new NHibernate.Cfg.Configuration();
            ISessionFactory sessionFactory = new Configuration().Configure("hibernate.cfg.xml").BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();
            ISQLQuery query = session.CreateSQLQuery(@"select * from ""SIC"".""Derece"" where ""CreatedAt"">=:StartDate and ""CreatedAt""<=:FinishDate").AddEntity(typeof(Derece));
            query.SetParameter("StartDate", startDate);
            query.SetParameter("FinishDate", finishDate);
            IList<Derece> logList = query.List<Derece>();
            return logList.ToList();
        }
    }
}