﻿namespace Catalog.API.Products.CreaateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) 
  : ICommandHandler<CreateProductCommand, CreateProductResult>
{
  public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
  {
    logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);
    // Create Product entity from command object

    var product = new Product
    {
      Name = command.Name,
      Category = command.Category,
      Description = command.Description,
      ImageFile = command.ImageFile,
      Price = command.Price,
    };

    // TODO Save to database

    session.Store(product);
    await session.SaveChangesAsync(cancellationToken);

    // Return CreateProductResult result

    return new CreateProductResult(product.Id);
  }
}
