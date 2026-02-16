using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;

namespace PipelinePattern.OrderFlow.Handlers;

public class CalculateSavingsHandler : IPipelineHandler<(decimal Original, decimal Discounted), decimal>
{
    public async Task<ErrorOr<decimal>> HandleAsync((decimal Original, decimal Discounted) input)
        => input.Original - input.Discounted;
}
