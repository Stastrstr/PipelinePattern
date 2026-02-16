using ErrorOr;
using FR.Stateful.Pipeline.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FR.Stateful.Pipeline.Implementations.Behaviors;

public class LoggingBehavior<TState>(ILogger logger) : IPipelineBehavior<TState>
{
    // Pre-compiled log message for maximum performance
    private static readonly Action<ILogger, string, double, Exception?> _logStats =
        LoggerMessage.Define<string, double>(LogLevel.Information, 0, "Step {StepName} finished in {ElapsedMs}ms");

    public async Task<ErrorOr<Success>> HandleAsync(TState state, Func<Task<ErrorOr<Success>>> next, string stepName)
    {
        var sw = Stopwatch.StartNew();

        var result = await next();

        sw.Stop();
        _logStats(logger, stepName, sw.Elapsed.TotalMilliseconds, null);

        return result;
    }
}