
namespace Common.Mappers
{
    public class BookMapper : IBookMapper
    {
        public BookDTO Map(Book book)
        {
            return new BookDTO() { genreId = book.genreId ,bookDescription=book.bookDescription,bookTitle=book.bookTitle};
        }

        public Book Map(BookDTO book)
        {
            return new Book() { genreId=book.genreId,bookDescription=book.bookDescription,bookTitle=book.bookTitle};
        }
    }
}
