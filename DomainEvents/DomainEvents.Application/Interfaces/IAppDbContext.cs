using DomainEvents.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Person> People { get; set; }
    Task SaveAsync(CancellationToken cancellationToken = default);
}