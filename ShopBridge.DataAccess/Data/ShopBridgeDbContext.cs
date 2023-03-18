using Microsoft.Extensions.Options;
using ShopBridge.DataAccess.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShopBridge.DataAccess.Data;

public class ShopBridgeDbContext
{
    private readonly ConnectionStrings _connection;

    public ShopBridgeDbContext(IOptions<ConnectionStrings> connection)
    {
        _connection = connection.Value;
    }

    public IDbConnection CreateSqlServerConnection()
        => new SqlConnection(_connection.DBSqlConnection);
}
