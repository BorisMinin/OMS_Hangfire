namespace OMS.Data.Access.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        //todo: реализовать Query с помощью Task<IEnumerable>
        IQueryable<T>Query<T>(/*T obj/*, CancellationToken token*/) where T : class;
        Task Add<T>(T obj, CancellationToken token) where T : class;
        void Update<T>(T obj, CancellationToken token) where T : class;
        void Delete<T>(T obj, CancellationToken token) where T : class;
        Task CommitAsync(CancellationToken token);
    }
}