using ErrorOr;

namespace FR.Stateful.Pipeline.Abstractions.Interfaces;

public interface IPipelineStep<TState>
{
    Task<ErrorOr<Success>> ExecuteAsync(TState state);
}
