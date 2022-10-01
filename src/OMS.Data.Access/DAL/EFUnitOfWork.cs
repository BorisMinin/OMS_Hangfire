namespace OMS.Data.Access.DAL
{
    /// <summary>
    /// Реализация шаблона UnitOfWork
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private OMSDbContext _context;
        // инъекция DbContext
        public EFUnitOfWork(OMSDbContext context)
        {
            _context = context;
        }
        // todo: сформулирвать комментарии и дописать
        /// <summary>
        /// Метод Query принимает объект Т и возвращает 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable<T> Query<T>(/*T obj, CancellationToken token*/)
            where T : class => _context.Set<T>();
        // todo: сформулирвать комментарии
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        public async Task Add<T>(T obj, CancellationToken token)
            where T : class
        {
            await this._context.Set<T>().AddAsync(obj, token);
            await this._context.SaveChangesAsync(); 
        }
        // todo: сформулирвать комментарии
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        public void Update<T>(T obj, CancellationToken token) 
            where T : class
        {
            this._context.Set<T>()
            .Update(obj);
        }
        // todo: реализовать Soft Delete
        // todo: сформулирвать комментарии
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        public void Delete<T>(T obj, CancellationToken token) 
            where T : class
        {
            this._context.Set<T>().Remove(obj);
        }
        public async Task CommitAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            _context = null;
        }
    }
}