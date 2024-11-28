using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Domain.Entities
{
    [Owned]
    public class RefreshToken
    {
        public string Token {  get; set; }=string.Empty;
        public DateTime ExpiresOn { get; set; }

        public DateTime CreatesOn { get; set; }=DateTime.UtcNow;
        public DateTime ?RevokedOn { get; set; }

        public bool IsExpired => ExpiresOn < DateTime.UtcNow;
        public bool IsActive=>RevokedOn is null&&!IsExpired;


        public string UserId { get; set; } = string.Empty;
    }
}
