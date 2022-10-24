
using Core.Models;
using Services.INtefaces;

namespace Application.AppServices
{
    public class BookAppService : BaseService, IBookAppService
    {
        private IBookMapper _bookMapper;
        public BookAppService(IUnitOfWork unitOfWork,IBookMapper bookMapper) : base(unitOfWork)
        {
            _bookMapper= bookMapper;
        }

        public async Task<APIResult> Create(BookDTO book)
        {
            APIResult result = new APIResult() { Message="Error",TypeMessage=Common.Enums.typeMessage.Error};
            try
            {
                Book bookobj = _bookMapper.Map(book);
                bookobj.creationDate = DateTime.Now;
                bookobj.isDeleted = (byte)Common.Enums.delete.NotDeleted;
               await _unitOfWork.Books.AddOne(bookobj);
                if (_unitOfWork.complete() > default(int))
                    result.TypeMessage = Common.Enums.typeMessage.Ok; result.Message = "Added Sucessfully";
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public APIResult Delete(int id)
        {
            APIResult result = new APIResult() { Message = "Error", TypeMessage = Common.Enums.typeMessage.Error };
            try
            {
               var obj=  _unitOfWork.Books.GetByID(id).Result;
                if (obj != null)
                {
                    _unitOfWork.Books.Delete(obj);
                    if (_unitOfWork.complete() > default(int))
                            result.TypeMessage = Common.Enums.typeMessage.Ok; result.Message = "Deleted Sucessfully";

                }

            }
            catch (Exception)
            {

                throw;
            }
            return result;   
        }

        public APIResult EditBook(BookDTO book)
        {
            APIResult result = new APIResult() { Message = "Error", TypeMessage = Common.Enums.typeMessage.Error };
            try
            {
                _unitOfWork.Books.Update(_bookMapper.Map(book));
                if (_unitOfWork.complete() > default(int))
                    result.TypeMessage = Common.Enums.typeMessage.Ok; result.Message = "Updated Sucessfully";
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            List<BookDTO> bookDTOs = new List<BookDTO>();
            try
            {
                var BookList = await _unitOfWork.Books.getAll();
                foreach (var item in BookList)
                {
                    bookDTOs.Add(_bookMapper.Map(item));
                }
                return bookDTOs;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<BookDTO> GetbyId(int id)
        {
            BookDTO book=new BookDTO();
            try
            {
              var bookobj=await _unitOfWork.Books.GetByID(id);
                book=_bookMapper.Map(bookobj);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
            return book;
        }

    }
}
