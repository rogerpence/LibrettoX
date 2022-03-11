/*
Minimal Web API for entity.

Database......{{DatabaseName}}
Table.........{{TableName}}

Generated on..{{datetime}}
Template......{{template}}
Schema........{{schema}}
*/

using DataAccess.Data;
using DataAccess.Models;

namespace WebApi.Helpers
{
    public static class {{TableName}}CrudApi
    {
        public static void Configure{{TableName}}CrudApi(this WebApplication app)
        {
            app.MapGet("/{{TableName.lower()}}", GetAll{{TableName}});
            app.MapGet("/{{TableName.lower()}}/{%raw%}{{%endraw%}{{csKeyName}}}", Get{{TableName}});
            app.MapPost("/{{TableName.lower()}}", Insert{{TableName}});

            // For Ajax use:
            app.MapPut("/{{TableName.lower()}}", Update{{TableName}});
            app.MapDelete("/{{TableName.lower()}}/{%raw%}{{%endraw%}{{csKeyName}}}", Delete{{TableName}});

            // For HTML form use:
            app.MapPost("/{{TableName.lower()}}/update", Update{{TableName}});
            app.MapPost("/{{TableName.lower()}}/delete/{%raw%}{{%endraw%}{{csKeyName}}}", Delete{{TableName}});
        }

        private static async Task<IResult> GetAll{{TableName}}(I{{TableName}}Crud data)
        {
            try
            {
                return Results.Ok(await data.GetAll{{TableName}}());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Get{{TableName}}(
            {{primaryKeyCSDeclaration}},
            I{{TableName}}Crud data,
            HttpResponse resp)
        {
            try
            {
                var results = await data.Get{{TableName}}({{csKeyName}});
                if (results == null)
                {
                    resp.Headers.Add("App-Response-Info", $"Row key '{%raw%}{{%endraw%}{{csKeyName}}}' not found.");
                    return Results.NotFound();
                }
                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Insert{{TableName}}(
            {{TableName}}Model model,
            I{{TableName}}Crud data,
            HttpResponse resp)
        {
            try
            {
                var results = await data.Get{{TableName}}(model.{{csKeyName}});
                if (results != null)
                {
                    resp.Headers.Add("App-Response-Info", $"Row with key '{%raw%}{{%endraw%}model.{{csKeyName}}}' already exists.");
                    return Results.Conflict(results);
                }
            
                int id = await data.Insert{{TableName}}(model);
                resp.Headers.Add("App-New-Row-Id", id.ToString()); ;
                return Results.Ok(id);                
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Update{{TableName}}(
            {{TableName}}Model model,
            I{{TableName}}Crud data,
            HttpResponse resp)
        {
            try
            {
                var results = await data.Get{{TableName}}(model.{{csKeyName}});
                if (results == null)
                {
                    resp.Headers.Add("App-Response-Info", $"Row '{%raw%}{{%endraw%}model.{{csKeyName}}}' not found.");
                    return Results.NotFound();
                }

                await data.Update{{TableName}}(model);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> Delete{{TableName}}(
            {{primaryKeyCSDeclaration}},
            I{{TableName}}Crud data,
            HttpResponse resp)
        {
            try
            {
                var results = await data.Get{{TableName}}({{csKeyName}});
                if (results == null)
                {
                    resp.Headers.Add("App-Response-Info", $"Row key '{%raw%}{{%endraw%}{{csKeyName}}}' not found.");
                    return Results.NotFound();
                }

                await data.Delete{{TableName}}({{csKeyName}});
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
