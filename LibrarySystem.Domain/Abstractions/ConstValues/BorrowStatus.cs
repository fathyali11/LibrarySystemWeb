namespace LibrarySystem.Domain.Abstractions.ConstValues;
public static class BorrowStatus
{
    public const string borrowed=nameof(borrowed);
    public const string returned = nameof(returned);
    public const string overdue = nameof(overdue);
    public const int BorrowAllowedDays = 14;
}
