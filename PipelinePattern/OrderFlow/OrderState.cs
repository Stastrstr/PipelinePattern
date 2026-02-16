namespace PipelinePattern.OrderFlow;

public class OrderState
{
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
    public decimal TotalSum { get; set; }
    public decimal DiscountedSum { get; set; }
    public decimal Savings { get; set; }
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
}