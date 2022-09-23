﻿using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.Abstractions.Queries;

public interface IGetOtherBooksAlsoBorrowedHandler : IRequestHandler<OtherBooksAlsoBorrowedRequest, IList<OtherBooksAlsoBorrowedResponse>>
{
    
}