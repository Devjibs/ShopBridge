using Microsoft.Extensions.Options;
using ShopBridge.DataAccess.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShopBridge.DataAccess.Data;

public class DapperContext
{
    private readonly ConnectionStrings _connection;

    public DapperContext(IOptions<ConnectionStrings> connection)
    {
        _connection = connection.Value;
    }

    public IDbConnection CreateSqlServerConnection()
        => new SqlConnection(_connection.DBSqlConnection);
}
