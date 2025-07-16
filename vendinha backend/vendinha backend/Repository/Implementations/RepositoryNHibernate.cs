using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vendinha_backend.Repository;

namespace vendinha_backend.Repository.Implementations
{
    public class RepositoryNHibernate : IRepository
    {
        private readonly ISessionFactory sessionFactory;
        private readonly NHibernate.ISession session;

        public RepositoryNHibernate(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            this.session = sessionFactory.OpenSession();
        }


        public IQueryable<T> Consultar<T>()
        {
            return session.Query<T>();
        }

        public T ConsultarPorId<T>(long id)
        {
            return session.Get<T>(id);
        }

        public void Excluir(object model)
        {
            session.Delete(model);
        }

        public IDisposable IniciarTransacao()
        {
            var transaction = session.BeginTransaction();
            return transaction;
        }

        public void Rollback()
        {
            session.GetCurrentTransaction()?.Rollback();
        }

        public void Commit()
        {
            session.GetCurrentTransaction().Commit();
        }

        public void Salvar(object model)
        {
            session.Merge(model);
        }
        public void Incluir(object model)
        {
            session.Save(model);
        }
    }
}
