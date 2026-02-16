using ErrorOr;

namespace FR.Stateful.Pipeline.Abstractions.Interfaces;

public interface IPipelineBehavior<TState>
{
    Task<ErrorOr<Success>> HandleAsync(TState state, Func<Task<ErrorOr<Success>>> next, string stepName);
}