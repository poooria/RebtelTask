using Borrow.Contracts.DTO;
using Borrow.Contracts.Services;
using MediatR;
using ProtoBuf.Grpc;

namespace Borrow.API.Grpc;

public class GrpcBorrowService : IGrpcBorrowService
{
    private readonly IMediator _mediator;

    public GrpcBorrowService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<List<GetMostBorrowedBooksResponse>> GetMostBorrowedBooksAsync(GetMostBorrowedBooksRequest request, CallContext context = default)
    {
        var result = await _mediator.Send(request);
        var items = result.Select(x => new GetMostBorrowedBooksResponse
        { BookId = x.BookId, BorrowedCount = x.BorrowedCount }).ToList();
        return items;
    }
    public async Task<List<GetMostBorrowersResponse>> GetMostBorrowersAsync(GetMostBorrowersRequest request, CallContext context = default)
    {
        var result = await _mediator.Send(request);
        var items = result.Select(x => new GetMostBorrowersResponse
        { UserId = x.UserId, BorrowedCount = x.BorrowedCount }).ToList();
        return items;
    }

    public async Task<List<GetOtherBooksBorrowedResponse>> GetOtherBooksAlsoBorrowedAsync(GetOtherBooksBorrowedRequest request, CallContext context = default)
    {
        var result = await _mediator.Send(request);
        var items = result.Select(x => new GetOtherBooksBorrowedResponse
        { BookId = x.BookId, BorrowedCount = x.BorrowedCount }).ToList();
        return items;
    }

    public async Task<List<GetBooksWithBorrowedDaysResponse>> GetBookWithBorrowedDaysAsync(GetBooksWithBorrowedDaysRequest request, CallContext context = default)
    {
        var result = await _mediator.Send(request);
        var items = result.Select(x => new GetBooksWithBorrowedDaysResponse
        { BookId = x.BookId, UserId = x.UserId, BorrowedDaysCount = x.BorrowedDaysCount }).ToList();
        return items;
    }

    public async Task<List<GetBorrowedBooksByUserResponse>> GetBorrowedBooksByUserIdAsync(GetBorrowedBooksByUserRequest request, CallContext context = default)
    {
        var result = await _mediator.Send(request);
        var items = result.Select(x => new GetBorrowedBooksByUserResponse
        {
            BookId = x.BookId,
            UserId = x.UserId,
            BorrowedDate = x.BorrowedDate,
            ReturnDate = x.ReturnDate
        }).ToList();
        return items;
    }
}