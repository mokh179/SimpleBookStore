

using Core.Interfaces;
using Core.Models;

namespace Services.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        AppDBContext _db;
        public UnitOfWork(AppDBContext db)
        {
            _db = db;
        }
        public IBase<Book> Books =>  new BaseRepo<Book>(_db);

        public IBase<User> Users =>  new BaseRepo<User>(_db);

        public int complete()
        {
           return _db.SaveChanges();
        }
    }
}
