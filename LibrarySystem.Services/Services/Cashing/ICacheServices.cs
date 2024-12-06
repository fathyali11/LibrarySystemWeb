namespace LibrarySystem.Services.Services.Cashing
{
    public interface ICacheServices
    {
        Task SetAsync<T>(string key, T value,CancellationToken cancellationToken=default);
        Task<T?> GetAsync<T>(string key,CancellationToken cancellationToken=default);
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
