# Fluent Stateful Pipeline for .NET

A lightweight, SOLID-compliant implementation of the **Mediator** and **Decorator** patterns. This architecture replaces complex conditional logic and "if-statement hell" with a type-safe, observable business workflow.

## 🚀 The Problem
In complex business processes, "Specification Pattern" isn't enough because it only returns a boolean. Traditional "Chain of Responsibility" often leads to forced downcasting and "God Object" states. 

This library provides a **Stateful Pipeline** that allows each step to:
1.  **Select** only the specific data it needs (Interface Segregation).
2.  **Short-circuit** the flow using `ErrorOr`.
3.  **Access** results from any previous step without tight coupling.

---

## 🏗️ Architecture

The solution is built on three pillars:

- **The State:** A simple DTO that travels through the pipeline.
- **The Handlers:** Pure business logic units that are unaware of the global state.
- **The Wiring:** Extension methods that act as the "Technical Glue," mapping state properties to handler inputs.



---

## 💻 Example Usage

The orchestration reads like a high-level business requirements document.

```csharp
// 1. Initialize your state
var state = new OrderState { Amount = 100, Quantity = 5 };

// 2. Build and Execute
var result = await new PipelineBuilder<OrderState>(state, loggingBehavior)
    .CalculateTotal(new CalculateTotalSumHandler())
    .ApplyDiscount(new ApplyTenPercentDiscountHandler())
    .DetermineSavings(new CalculateSavingsHandler())
    .PersistToAzure(new SaveToAzureStorageHandler())
    .ExecuteAsync();

// 3. Handle the outcome (Railway Oriented Programming)
result.Switch(
    success => Console.WriteLine($"Flow Complete. Savings: {success.Savings}"),
    errors  => Console.WriteLine($"Flow Halted: {errors.First().Description}")
);