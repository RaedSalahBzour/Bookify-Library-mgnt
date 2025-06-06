using MediatR;

namespace Application.Books.Commands
{
    public record CreateBookCommand(
    string Author,
    string? Description,
    string Title,
    string? CoverUrl,
    string ISBN,
    DateTime PublishDate,
    List<string> CategoryIds
) : IRequest<Guid>;

}
