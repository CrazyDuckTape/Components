using System.Linq;
using System.Threading.Tasks;
using NHibernate;

namespace Domain
{
    public class NHibernateSession<T>
    {
        private ISession _session;
        private ITransaction _transaction;

        protected NHibernateSession(ISession session)
        {
            _session = session;
        }

        protected IQueryable<T> Query => _session.Query<T>();

        protected void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        protected void Commit()
        {
            _transaction.Commit();
        }

        protected async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        protected void Rollback()
        {
            _transaction.Rollback();
        }

        protected async Task RollbackAsync()
        {
            await _transaction.RollbackAsync().ConfigureAwait(false);
        }

        protected void CloseTransaction()
        {
            if(_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        protected void Save(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        protected async Task SaveAsync(T entity)
        {
            await _session.SaveOrUpdateAsync(entity).ConfigureAwait(false);
        }

        protected void Update(T entity)
        {
            Save(entity);
        }

        protected async Task UpdateAsync(T entity)
        {
            await SaveAsync(entity).ConfigureAwait(false);
        }

        protected void Delete(T entity)
        {
            _session.Delete(entity);
        }

        protected async Task DeleteAsync(T entity)
        {
            await _session.DeleteAsync(entity).ConfigureAwait(false);
        }
    }
}
