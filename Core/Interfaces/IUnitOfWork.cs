using Core.Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBase<Book> Books { get; }
        IBase<User> Users { get; }
        int complete();
    }
}
