namespace Codebiz.Infrastructure.SalesDataService
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}