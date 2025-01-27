namespace LibrarySystem.Services.Services.Fines;
public interface IFineServices:IFineRepository
{
    Task AddFine();
}
