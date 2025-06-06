using Application.Books.Commands;
using MediatR;

namespace Application.Books.Handlers
{
    internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        public Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
