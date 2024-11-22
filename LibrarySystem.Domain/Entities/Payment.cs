namespace LibrarySystem.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string UserId { get; set; }=string.Empty;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public bool IsDeleted {  get; set; }

    // Navigation property
    public Order Order { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
}
