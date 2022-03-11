
namespace DataAccess.DBAccess;

public interface I{{TableName}}SqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionID = "Default");
    Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
    Task<{{csKeyType}}> SaveDataWithReturn<T>(string storedProcedure, T parameters, string connectionId = "Default");
}