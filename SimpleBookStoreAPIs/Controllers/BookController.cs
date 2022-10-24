using Common.DTOs;
using IApplication.IAppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBookStoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookAppService _bookAppService;
        public BookController(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _bookAppService.GetAllBooks());
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookDTO book)
        {
            if(!ModelState.IsValid)
                return BadRequest(book);
            return Ok(await _bookAppService.Create(book));
        }

        [HttpGet("GetBook/{id}")]
        public  async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _bookAppService.GetbyId(id));
        }

        [HttpPost("EditBook")]
        public IActionResult Edit(BookDTO book)
        {
            if (book != null)
                return Ok(_bookAppService.EditBook(book));
            return BadRequest(book);
        }

        [HttpDelete("DeleteBook/{bookId}")]
        public IActionResult Delete(int bookId)
        {
            if (bookId > 0)
                return Ok(_bookAppService.Delete(bookId));
            return BadRequest();
        }
    }
}
