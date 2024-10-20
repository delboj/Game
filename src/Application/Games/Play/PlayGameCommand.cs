using Application.Abstractions.Messaging;

namespace Application.Games.Play;
public sealed record PlayGameCommand(int Player) : ICommand<PlayGameCommandResponse>;
