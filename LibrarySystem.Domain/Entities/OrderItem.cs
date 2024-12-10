namespace LibrarySystem.Domain.Entities;

public class OrderItem:ItemBase
{
    public int OrderId {  get; set; }
    public Order Order { get; set; }=default!;
}
