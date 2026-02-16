using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;

namespace FR.Stateful.Pipeline.Implementations;

internal class PipelineStep<TState, TInput, TOutput>(
    IPipelineHandler<TInput, TOutput> handler,
    Func<TState, TInput> select,
    Action<TState, TOutput> update) : IPipelineStep<TState>
{
    private readonly IPipelineHandler<TInput, TOutput> _handler = handler;
    private readonly Func<TState, TInput> _inputSelector = select;
    private readonly Action<TState, TOutput> _stateUpdater = update;

    public async Task<ErrorOr<Success>> ExecuteAsync(TState state)
    {
        var result = await _handler.HandleAsync(_inputSelector(state));

        if (result.IsError) return result.Errors;

        _stateUpdater(state, result.Value);

        return Result.Success;
    }
}