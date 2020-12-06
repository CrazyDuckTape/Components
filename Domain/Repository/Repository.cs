using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace Domain
{
    public abstract class Repository<T> : NHibernateSession<T>
    {
        protected Repository(ISession session) : base(session) { }
            
        public virtual IEnumerable<T> GetAll()
        {
            return Query.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Query.ToListAsync().ConfigureAwait(false);
        }

        public virtual T GetOne(Expression<Func<T, bool>> criteria)
        {
            return Query.Where(criteria).FirstOrDefault();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> criteria)
        {
            return Query.Where(criteria).ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> criteria)
        {
            return await Query.Where(criteria).ToListAsync().ConfigureAwait(false);
        }

        public new virtual async Task SaveAsync(T entity)
        {
            try
            {
                BeginTransaction();

                await SaveAsync(entity).ConfigureAwait(false);

                await CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await RollbackAsync().ConfigureAwait(false);

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }

        public virtual async Task SaveAsync(IEnumerable<T> entities)
        {
            try
            {
                BeginTransaction();

                foreach (var entity in entities)
                {
                    await SaveAsync(entity).ConfigureAwait(false);
                }

                await CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await RollbackAsync().ConfigureAwait(false);

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }
        
        public new virtual void Save(T entity)
        {
            try
            {
                BeginTransaction();

                Save(entity);

                Commit();
            }
            catch (Exception)
            {
                Rollback();

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }

        public virtual void Save(IEnumerable<T> entities)
        {
            try
            {
                BeginTransaction();

                foreach (var entity in entities)
                {
                    Save(entity);
                }

                Commit();
            }
            catch (Exception)
            {
                Rollback();

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }

        public new virtual async Task DeleteAsync(T entity)
        {
            try
            {
                BeginTransaction();

                await DeleteAsync(entity).ConfigureAwait(false);

                await CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await RollbackAsync().ConfigureAwait(false);

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }

        public virtual async Task DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                BeginTransaction();

                foreach (var entity in entities)
                {
                    await DeleteAsync(entity).ConfigureAwait(false);
                }

                await CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await RollbackAsync().ConfigureAwait(false);

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }

        public new virtual void Delete(T entity)
        {
            try
            {
                BeginTransaction();

                Delete(entity);

                Commit();
            }
            catch (Exception)
            {
                Rollback();

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                BeginTransaction();

                foreach (var entity in entities)
                {
                    Delete(entity);
                }

                Commit();
            }
            catch (Exception)
            {
                Rollback();

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }
    }
}
