

namespace Common.IMappers
{
    public interface IBookMapper
    {
        public BookDTO Map(Book book);
        public Book Map(BookDTO book);
    }
}
