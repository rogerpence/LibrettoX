using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.DBAccess;

public class {{TableName}}SqlDataAccess : I{{TableName}}SqlDataAccess
{
    private readonly IConfiguration _config;

    public {{TableName}}SqlDataAccess (IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure,
                                                     U parameters,
                                                     string connectionID = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionID));

        return await connection.QueryAsync<T>(storedProcedure,
                                              parameters,
                                              commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure,
                                  T parameters,
                                  string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<{{csKeyType}}> SaveDataWithReturn<T>(string storedProcedure,
                                  T parameters,
                                  string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        {{csKeyType}} id = await connection.ExecuteScalarAsync<{{csKeyType}}>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        return id;

    }





}
