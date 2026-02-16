Fluent Stateful Pipeline in .NET
A lightweight, SOLID-compliant implementation of the Mediator and Decorator patterns designed to replace complex conditional logic with a type-safe, observable business workflow.

🚀 Overview
This project demonstrates a "Stateful Pipeline" architecture. Unlike a standard Chain of Responsibility, this pattern uses a shared State Object and Selector Delegates to ensure that data flows through business handlers without the need for type casting or heavy reflection.

Key Features:
Type Safety: Uses generic constraints to ensure Handlers only receive the specific data they need.

Railway Oriented Programming: Integrated with the ErrorOr library for clean short-circuiting and error handling.

Observability: Built-in Decorator Pattern for cross-cutting concerns (Logging, Performance Monitoring) via high-performance LoggerMessage definitions.

Fluent API: Extension-based orchestration that reads like a business requirements document.

SOLID Principles: High adherence to Interface Segregation and Single Responsibility.

🏗️ Architecture
The solution is divided into three distinct layers:

Core Infrastructure: Generic interfaces (IPipelineHandler, IStep) and the PipelineBuilder.

Business Logic: Highly focused, single-purpose Handlers that perform calculations or persistence.

Wiring Layer: Extension methods that map the global state to individual handler inputs.

💻 Example Usage
The following snippet shows a workflow that calculates order totals, applies discounts, determines savings, and persists the result to Azure.

C#
var state = new OrderState { Amount = 100, Quantity = 5 };

var result = await new PipelineBuilder<OrderState>(state, loggingBehavior)
    .CalculateTotal(new CalculateTotalSumHandler())
    .ApplyDiscount(new ApplyTenPercentDiscountHandler())
    .DetermineSavings(new CalculateSavingsHandler())
    .PersistToAzure(new SaveToAzureStorageHandler())
    .ExecuteAsync();

result.Switch(
    success => Console.WriteLine($"Success: {success.Savings}"),
    errors  => Console.WriteLine($"Failed: {errors.First().Description}")
);
🛠️ Performance Highlights
Zero Reflection: The pipeline uses pre-compiled delegates (Func<TState, TInput>), making it significantly faster than reflection-based mediators.

Allocation-Free Logging: The LoggingBehavior utilizes LoggerMessage.Define to minimize heap allocations during telemetry capture.

📦 Dependencies
ErrorOr - Functional error handling.

Microsoft.Extensions.Logging - Extensible logging framework.