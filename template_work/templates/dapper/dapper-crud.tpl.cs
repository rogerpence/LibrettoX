/*
Crud actions class for an entity. 

Database......{{DatabaseName}}
Table.........{{TableName}}

Generated on..{{datetime}}
Template......{{template}}
Schema........{{schema}}
*/

using DataAccess.DBAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class {{TableName}}Crud: I{{TableName}}Crud
{
    private readonly I{{TableName}}SqlDataAccess _db;

    public {{TableName}}Crud(I{{TableName}}SqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<{{TableName}}Model>> GetAll{{TableName}}() =>
        _db.LoadData<{{TableName}}Model, dynamic>(
            "dbo.{{TableName}}_GetAll",
            new {});

    public async Task<{{TableName}}Model?> Get{{TableName}}({{primaryKeyCSDeclaration}})
    {
        var results = await _db.LoadData<{{TableName}}Model, dynamic>(
            "dbo.{{TableName}}_Get",
             new { {{primaryKeyCSAssignment}} });
        return results.FirstOrDefault();
    }

    public async Task<{{csKeyType}}> Insert{{TableName}}({{TableName}}Model model) 
    {
        int id = await _db.SaveDataWithReturn("dbo.artist_Insert", model);
        return id;        
    }        

    public Task Update{{TableName}}({{TableName}}Model model) =>
        _db.SaveData("dbo.{{TableName}}_Update", model);

    public Task Delete{{TableName}}({{primaryKeyCSDeclaration}}) =>
        _db.SaveData(
            "dbo.{{TableName}}_Delete",
            new { {{primaryKeyCSAssignment}} });
}
