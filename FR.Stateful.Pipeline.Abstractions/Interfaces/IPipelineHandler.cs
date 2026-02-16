using ErrorOr;

namespace FR.Stateful.Pipeline.Abstractions.Interfaces;

public interface IPipelineHandler<TInput, TOutput>
{
    Task<ErrorOr<TOutput>> HandleAsync(TInput input);
}
