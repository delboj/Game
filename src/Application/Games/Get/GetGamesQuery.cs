using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Games.Get;
public sealed record GetGamesQuery(Pagination Pagination) : IQuery<PagedList<GameResponse>>;
