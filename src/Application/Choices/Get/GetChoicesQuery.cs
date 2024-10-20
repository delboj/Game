using Application.Abstractions.Messaging;

namespace Application.Choices.Get;
public sealed record GetChoicesQuery() : IQuery<List<ChoiceResponse>>;
