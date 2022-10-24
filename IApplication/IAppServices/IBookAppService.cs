

namespace IApplication.IAppServices
{
    public interface IBookAppService
    {
        public  Task<List<BookDTO>> GetAllBooks();
        public  Task<BookDTO> GetbyId(int id);
        public APIResult EditBook(BookDTO book);
        public Task<APIResult> Create(BookDTO book);
        public APIResult Delete(int id);
    }
}
