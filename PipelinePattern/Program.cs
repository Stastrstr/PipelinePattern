using FR.Stateful.Pipeline.Implementations;
using FR.Stateful.Pipeline.Implementations.Behaviors;
using Microsoft.Extensions.Logging;
using PipelinePattern.OrderFlow;
using PipelinePattern.OrderFlow.Handlers;

Console.WriteLine("Hello, World!");

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

var logger = loggerFactory.CreateLogger<OrderState>();
var loggingBehavior = new LoggingBehavior<OrderState>(logger);

await ProcessOrderWorkflow(new OrderState
{
    Amount = 100m,
    Quantity = 2
}, loggingBehavior);

static async Task ProcessOrderWorkflow(OrderState state, LoggingBehavior<OrderState> loggingBehavior)
{
    var result = await new PipelineBuilder<OrderState>(state, loggingBehavior)
        .AddCalculateTotal(new CalculateTotalSumHandler())
        .AddApplyDiscount(new ApplyTenPercentDiscountHandler())
        .AddDetermineSavings(new CalculateSavingsHandler())
        .AddPersistToAzure(new SaveToAzureStorageHandler())
        .ExecuteAsync();

    result.Switch(
        success => Console.WriteLine($"Flow Complete. Saved: {success.Savings}"),
        errors => Console.WriteLine($"Flow Halted: {errors.First().Description}")
    );
}
