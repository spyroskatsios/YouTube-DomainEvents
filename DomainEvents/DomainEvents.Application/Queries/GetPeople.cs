using DomainEvents.Application.Interfaces;
using DomainEvents.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Application.Queries;

public record GetPeople : IRequest<IEnumerable<Person>>;

public class GetPeopleHandler : IRequestHandler<GetPeople, IEnumerable<Person>>
{
    private readonly IAppDbContext _context;

    public GetPeopleHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> Handle(GetPeople request, CancellationToken cancellationToken)
        => await _context.People.AsNoTracking().ToListAsync(cancellationToken);
}