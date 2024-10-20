using Application.Abstractions.Messaging;

namespace Application.Choices.GetRandom;
public sealed record GetRandomChoiceQuery() : IQuery<RandomChoiceResponse>;
