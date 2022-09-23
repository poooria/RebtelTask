using Book.Contracts;
using Grpc.Core;
using ProtoBuf.Grpc;

namespace Book.API.Grpc;

public class GrpcBookService : IGrpcBookService
{
    private IBookRepository _bookRepository;

    public GrpcBookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<GetBooksByIdsResponse>> GetBooksByIdsAsync(GetBooksByIdsRequest request, CallContext context = default)
    {
        if (string.IsNullOrEmpty(request.Ids))
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Ids is null"));
        }
        var numIds = request.Ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

        if (!numIds.All(nid => nid.Ok))
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Ids must be comma-separated list of numbers"));
        }
        var idsToSelect = numIds
            .Select(id => id.Value);
        var books = await _bookRepository.GetBooksByIdsAsync(idsToSelect);
        var retBooks = books.Select(x => new GetBooksByIdsResponse
        {
            BookAuthor = x.BookAuthor.FirstName + " " + x.BookAuthor.LastName,
            BookLanguage = x.BookLanguage.Title,
            BookPublisher = x.BookPublisher.Title,
            ISBN = x.ISBN,
            Title = x.Title,
            Description = x.Description,
            Id = x.Id,
            Pages = x.Pages,
            Weight = x.Weight,
            BorrowedCopies = x.BorrowedCopies,
            TotalCopies = x.TotalCopies

        }).ToList();
        return retBooks;
    }

    public async Task<GetBookAvailableBorrowResponse> GetBookAvailableBorrowAsync(GetBookAvailableBorrowRequest request, CallContext context = default)
    {
        var result = await _bookRepository.GetBookAsync(request.BookId);
        if (result == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Book not found with given id"));
        }
        return new GetBookAvailableBorrowResponse
            { Available = result.TotalCopies - result.BorrowedCopies, Borrowed = result.BorrowedCopies };
    }
}