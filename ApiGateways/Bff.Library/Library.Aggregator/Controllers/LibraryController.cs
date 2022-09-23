using System.Net;
using Borrow.Contracts.DTO;
using Library.Aggregator.Authentication;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Aggregator.Controllers;
[Route("api")]
[ApiController]
[Produces("application/json")]
public class LibraryController : ControllerBase
{
    private IMediator _mediator;
    public LibraryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Authenticate user and returns JWT token
    /// </summary>
    /// <remarks>for administrator privilege set user and password to 'admin'</remarks>
    [HttpPost]
    [Route("authenticate")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AuthenticateResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Authenticate([FromForm] AuthenticateRequest request)
    {
        return new JsonResult(await _mediator.Send(request));
    }

    /// <summary>
    /// Get 10 most borrowed books
    /// </summary>
    [Route("get-most-borrowed-books")]
    [RebtelAuthorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MostBorrowedBooksResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetMostBorrowedBooks()
    {
        return new JsonResult(await _mediator.Send(new MostBorrowedBooksRequest()));
    }


    /// <summary>
    /// Get the number borrowed/available book
    /// </summary>
    [Route("get-book-available")]
    [RebtelAuthorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookAvailableBorrowResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBookAvailable(int bookId)
    {
        return new JsonResult(await _mediator.Send(new BookAvailableBorrowRequest { BookId = bookId }));
    }


    /// <summary>
    /// Get the most top 10 borrowers in a given time frame 
    /// </summary>
    [Route("get-most-borrowers")]
    [RebtelAuthorize]
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<MostBorrowersResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetMostBorrowers([FromForm] MostBorrowersRequest request)
    {
        return new JsonResult(await _mediator.Send(request));
    }


    /// <summary>
    /// Get other books has were borrowed by particular person that borrowed a particular book
    /// </summary>
    [Route("get-other-books-also-borrowed")]
    [RebtelAuthorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OtherBooksAlsoBorrowedResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOtherBooksAlsoBorrowed(int bookId)
    {
        return new JsonResult(await _mediator.Send(new OtherBooksAlsoBorrowedRequest { BookId = bookId }));
    }


    /// <summary>
    /// Get particular book's read rate(pages per day)
    /// </summary>
    [Route("get-book-read-rate")]
    [RebtelAuthorize]
    [HttpGet]
    [ProducesResponseType(typeof(BookReadRateResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBookReadRate(int bookId)
    {
        return new JsonResult(await _mediator.Send(new BookReadRateRequest { BookId = bookId }));
    }


    /// <summary>
    /// Get particular user's borrowed books in each time frame
    /// </summary>
    [Route("get-user-borrowed-books")]
    [RebtelAuthorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserBorrowedBooksResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUserBorrowedBooks(int userId)
    {
        return new JsonResult(await _mediator.Send(new UserBorrowedBooksRequest { UserId = userId }));
    }
}


