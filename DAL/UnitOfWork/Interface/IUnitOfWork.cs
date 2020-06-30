namespace DAL.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
        int Save();
    }
}
