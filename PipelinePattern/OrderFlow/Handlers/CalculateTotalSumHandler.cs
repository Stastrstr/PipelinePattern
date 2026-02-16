using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;

namespace PipelinePattern.OrderFlow.Handlers;

public class CalculateTotalSumHandler : IPipelineHandler<(decimal Price, int Qty), decimal>
{
    public async Task<ErrorOr<decimal>> HandleAsync((decimal Price, int Qty) input)
        => input.Price * input.Qty;
}
