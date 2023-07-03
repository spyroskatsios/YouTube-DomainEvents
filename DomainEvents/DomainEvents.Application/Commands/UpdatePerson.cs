using DomainEvents.Application.Interfaces;
using DomainEvents.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Application.Commands;

public record UpdatePerson(int Id, string FullName, int Age) : IRequest<Unit>;

public class UpdatePersonHandler : IRequestHandler<UpdatePerson, Unit>
{
    private readonly IAppDbContext _context;

    public UpdatePersonHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePerson request, CancellationToken cancellationToken)
    {
        var person = await _context.People.FirstAsync(x=>x.Id == request.Id, cancellationToken);
        person.Update(request.FullName, request.Age);
        await _context.SaveAsync(cancellationToken);
        return Unit.Value;
    }
}