using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Clients.Commands;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Clients.Handlers;

internal class CustomerCommandsHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IClientMapper clientMapper):
    IRequestHandler<CreateCustomerCommand, CreatedOrUpdatedEntityViewModel<Guid>>, IRequestHandler<UpdateCustomerCommand>,
    IRequestHandler<ArchiveCustomersCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var customerWithSamePhoneNumber = await dbContext.Customers
            .Where(x => x.PhoneNumber == request.Body.PhoneNumber.ToLower())
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .SingleOrDefaultAsync(cancellationToken);
        if (customerWithSamePhoneNumber != null)
        {
            throw new BusinessLogicException(
                $"Клиент с номером телефона \"{customerWithSamePhoneNumber.PhoneNumber}\" уже существует!");
        }

        var customerToCreate = clientMapper.MapToEntity((request.Body, externalSystemId));
        var createdCustomer = await dbContext.AddAsync(customerToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreatedOrUpdatedEntityViewModel(createdCustomer.Entity.Id);
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var customerToUpdate = await dbContext.Customers
            .Where(x => x.Id == request.CustomerId)
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Клиент с идентификатором \"{request.CustomerId}\" не найден!");

        var customerWithSamePhoneNumber = await dbContext.Customers
            .Where(x => x.PhoneNumber == request.Body.PhoneNumber.ToLower())
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken);
        if (customerWithSamePhoneNumber != null && customerWithSamePhoneNumber.Id != customerToUpdate.Id)
        {
            throw new BusinessLogicException(
                $"Клиент с номером телефона \"{customerWithSamePhoneNumber.PhoneNumber}\" уже существует!");
        }

        var updatedCustomer = clientMapper.MapExisted(request.Body, customerToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(ArchiveCustomersCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var customersToArchiveIds = request.CustomerIds.ToList();
        var existingCustomersToArchive = await dbContext.Customers
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .Where(x => customersToArchiveIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var existingCustomersToArchiveIds = existingCustomersToArchive.Select(x => x.Id).ToList();
        var notExistingCustomers = customersToArchiveIds.Where(x => !existingCustomersToArchiveIds.Contains(x)).ToList();
        if (notExistingCustomers.Any())
        {
            throw new ObjectNotFoundException(
                $"Клиенты с идентификаторами {string.Join(", ", notExistingCustomers)} не найдены!");
        }

        foreach (var customer in existingCustomersToArchive)
        {
            customer.IsArchive = true;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}