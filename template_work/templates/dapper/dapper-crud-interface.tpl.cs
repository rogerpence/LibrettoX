/*
Crud actions class interface for an entity. 

Database......{{DatabaseName}}
Table.........{{TableName}}

Generated on..{{datetime}}
Template......{{template}}
Schema........{{schema}}
*/

using DataAccess.Models;

namespace DataAccess.Data;

public interface I{{TableName}}Crud
{
    Task Delete{{TableName|title}}({{primaryKeyCSDeclaration}});
    Task<{{TableName}}Model?> Get{{TableName}}({{primaryKeyCSDeclaration}});
    Task<IEnumerable<{{TableName}}Model>> GetAll{{TableName}}();
    Task<{{csKeyType}}> Insert{{TableName}}({{TableName}}Model model);
    Task Update{{TableName}}({{TableName}}Model model);
}