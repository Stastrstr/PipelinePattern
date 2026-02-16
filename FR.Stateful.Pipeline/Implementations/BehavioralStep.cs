using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;

namespace FR.Stateful.Pipeline.Implementations;

public class BehavioralStep<TState>(IPipelineStep<TState> innerStep, IPipelineBehavior<TState> behavior, string name)
    : IPipelineStep<TState>
{
    public Task<ErrorOr<Success>> ExecuteAsync(TState state)
    {
        return behavior.HandleAsync(state, () => innerStep.ExecuteAsync(state), name);
    }
}