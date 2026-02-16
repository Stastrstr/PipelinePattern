using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;

namespace PipelinePattern.OrderFlow.Handlers;

public class SaveToAzureStorageHandler : IPipelineHandler<decimal, Success>
{
    public async Task<ErrorOr<Success>> HandleAsync(decimal amount)
    {
        // Azure SDK logic here...
        return Result.Success;
    }
}
