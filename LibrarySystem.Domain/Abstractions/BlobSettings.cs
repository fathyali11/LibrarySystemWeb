namespace LibrarySystem.Domain.Abstractions;
public class BlobSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ContainerNameImages { get; set; } = string.Empty;
    public string ContainerNameFiles { get; set; } = string.Empty;
}
