using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;

namespace FR.Stateful.Pipeline.Implementations;

public class PipelineBuilder<TState>(TState state, IPipelineBehavior<TState>? behavior = null) where TState : class
{
    private readonly List<IPipelineStep<TState>> _steps = [];

    public PipelineBuilder<TState> AddStep<TIn, TOut>(
        IPipelineHandler<TIn, TOut> handler,
        Func<TState, TIn> stateSelector,
        Action<TState, TOut> stateUpdater,
        string? stepName)
    {
        IPipelineStep<TState> step = new PipelineStep<TState, TIn, TOut>(handler, stateSelector, stateUpdater);

        if (behavior != null)
        {
            step = new BehavioralStep<TState>(step, behavior, stepName ?? handler.GetType().Name);
        }

        _steps.Add(step);
        return this;
    }

    public async Task<ErrorOr<TState>> ExecuteAsync()
    {
        foreach (var step in _steps)
        {
            var res = await step.ExecuteAsync(state);

            if (res.IsError) return res.Errors;
        }

        return state;
    }
}
