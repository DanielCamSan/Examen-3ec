namespace _3ecexamen.Repositories
{
    public interface IRoomRepository
    {
        Task<bool> ExistsAsync(int id);
    }
}
