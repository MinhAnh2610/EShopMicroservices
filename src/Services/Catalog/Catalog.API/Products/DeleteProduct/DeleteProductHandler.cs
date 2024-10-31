namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommnadValidator : AbstractValidator<DeleteProductCommand>
{
  public DeleteProductCommnadValidator()
  {
    RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
  }
}

internal class DeleteProducCommandtHandler(IDocumentSession session, ILogger<DeleteProducCommandtHandler> logger)
  : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
  public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
  {
    logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

    session.Delete<Product>(command.Id);
    await session.SaveChangesAsync();

    return new DeleteProductResult(true);
  }
}
