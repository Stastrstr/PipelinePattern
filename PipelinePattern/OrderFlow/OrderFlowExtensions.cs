using FR.Stateful.Pipeline.Implementations;
using PipelinePattern.OrderFlow.Handlers;

namespace PipelinePattern.OrderFlow;

public static class OrderPipelineExtensions
{
    public static PipelineBuilder<T> AddCalculateTotal<T>(
        this PipelineBuilder<T> builder,
        CalculateTotalSumHandler handler) where T : OrderState
    {
        return builder.AddStep(
            handler,
            state => (state.Amount, state.Quantity),
            (state, result) => state.TotalSum = result,
            "CalculateTotal"
        );
    }

    public static PipelineBuilder<T> AddApplyDiscount<T>(
        this PipelineBuilder<T> builder,
        ApplyTenPercentDiscountHandler handler) where T : OrderState
    {
        return builder.AddStep(
            handler,
            state => state.TotalSum,
            (state, result) => state.DiscountedSum = result,
            "ApplyDiscount"
        );
    }

    public static PipelineBuilder<T> AddDetermineSavings<T>(
        this PipelineBuilder<T> builder,
        CalculateSavingsHandler handler) where T : OrderState
    {
        return builder.AddStep(
            handler,
            state => (state.TotalSum, state.DiscountedSum),
            (state, result) => state.Savings = result,
            "DetermineSavings"
        );
    }

    public static PipelineBuilder<T> AddPersistToAzure<T>(
        this PipelineBuilder<T> builder,
        SaveToAzureStorageHandler handler) where T : OrderState
    {
        return builder.AddStep(
            handler,
            state => state.Savings,
            (state, _) => { /* No state update needed for persistence */ },
            "AzurePersistence"
        );
    }
}