using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;

namespace PipelinePattern.OrderFlow.Handlers;

public class ApplyTenPercentDiscountHandler : IPipelineHandler<decimal, decimal>
{
    public async Task<ErrorOr<decimal>> HandleAsync(decimal total)
        => total > 0 ? total * 0.9m : Error.Validation("Invalid.Total", "Total must be > 0");
}
