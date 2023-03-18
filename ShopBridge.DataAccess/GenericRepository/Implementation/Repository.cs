using Dapper;
using Microsoft.Extensions.Options;
using ShopBridge.DataAccess.GenericRepository.Contracts;
using ShopBridge.DataAccess.Models;
using System.Data;
using System.Reflection;

namespace ShopBridge.DataAccess.GenericRepository.Abstracts;

public class Repository : IRepository
{
    private readonly ConnectionStrings _conn; 
    private readonly IDbConnection _db;

    public Repository(IOptions<ConnectionStrings> connection)
    {
        _conn = connection.Value;
        _db = new System.Data.SqlClient.SqlConnection(_conn.DBSqlConnection);
    }

    public Task<T?> GetAsync<T>(string query, object? obj, CommandType? commandType)
    {
        return _db.QueryFirstOrDefaultAsync<T?>(query, obj, commandType: commandType);
    }

    public Task<IEnumerable<T?>> GetAllAsync<T>(string query, object? obj, CommandType? commandType)
    {
        commandType = commandType ?? CommandType.StoredProcedure;
        return _db.QueryAsync<T?>(query, obj, commandType: commandType);
    }
    public Task<SqlMapper.GridReader> GetMultipleAsync(string query, object? obj, CommandType? commandType)
    {
        commandType = commandType ?? CommandType.StoredProcedure;
        return _db.QueryMultipleAsync(query, obj, commandType: commandType);
    }

  
    public async Task<int> CreateOrUpdateAsync<T>(string query, T? obj, IDbTransaction? dbTransaction = null, int? timeout = null, CommandType? commandType = CommandType.StoredProcedure)
    {
        try
        {
            var param = new Dapper.DynamicParameters();

            if (obj is not null)
            {
                foreach (PropertyInfo p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    param.Add("@" + p.Name, p.GetValue(obj));
                }
            }

            int exec = await _db.ExecuteAsync(query, param, dbTransaction, timeout, commandType);
            return exec;
        }
        catch (Exception)
        {

            throw;
        }

    }

}