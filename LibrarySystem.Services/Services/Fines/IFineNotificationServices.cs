namespace LibrarySystem.Services.Services.Fines;
public interface IFineNotificationServices:IFineRepository
{
    Task AddFine();
}
