
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) 
  : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator
  : AbstractValidator<CheckoutBasketCommand>
{
  public CheckoutBasketCommandValidator()
  {
    RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
    RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
  }
}

public class CheckoutBasketCommandHandler
  (IBasketRepository repository, IPublishEndpoint publishEndpoint)
  : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
  public Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
