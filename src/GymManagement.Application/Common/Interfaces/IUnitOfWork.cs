namespace GymManagement.Application;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}