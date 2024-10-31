namespace Catalog.API.Products.CreaateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProuctCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProuctCommandValidator()
  {
    RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
    RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
    RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greated than 0");
  }
}

internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator, ILogger<CreateProductCommandHandler> logger)
  : ICommandHandler<CreateProductCommand, CreateProductResult>
{
  public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
  {
    logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);
    // Create Product entity from command object

    var result = await validator.ValidateAsync(command, cancellationToken);
    var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
    if (errors.Any()) throw new ValidationException(errors.FirstOrDefault());

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
