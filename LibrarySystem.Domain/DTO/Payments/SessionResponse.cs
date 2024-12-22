namespace LibrarySystem.Domain.DTO.Payments;
public record SessionResponse(
        string Id,
        string Url,
        string SuccessUrl,
        string CancelUrl,
        string PayementIntentId
    );
