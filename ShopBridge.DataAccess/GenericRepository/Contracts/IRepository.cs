using System.Data;

namespace ShopBridge.DataAccess.GenericRepository.Contracts;

public interface IRepository
{
    Task<int> CreateOrUpdateAsync<T>(string query, T? obj, IDbTransaction? dbTransaction = null, int? timeout = null, CommandType? commandType = CommandType.StoredProcedure);
    Task<T?> GetAsync<T>(string query, object? obj, CommandType? commandType);
    Task<IEnumerable<T?>> GetAllAsync<T>(string query, object? obj, CommandType? commandType);
}
