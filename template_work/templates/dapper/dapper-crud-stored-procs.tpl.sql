{% if Type == 'table' %}

/*
Crud stored procedures

Database......{{DatabaseName}}
Table.........{{TableName}}

Generated on..{{_datetime}}
Template......{{_template}}
Schema........{{_schema}}
*/

CREATE OR ALTER PROCEDURE [dbo].[{{TableName}}_Insert]
{{columnSqlDeclarations | indent(4, True)}}
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO [dbo].[{{TableName}}] 
    (
{{columnNamesSqlList | indent(8,True)}}, 
        [Added]
    ) 
    VALUES(
{{columnValuesSqlList | indent(8,True)}}, 
        CURRENT_TIMESTAMP
    );
END
SELECT CAST(SCOPE_IDENTITY() as int) as 'id'
GO

CREATE OR ALTER PROCEDURE [dbo].[{{TableName}}_Update]
{{columnSqlDeclarations | indent(4,True)}}
AS
BEGIN 
    UPDATE [dbo].[{{TableName}}]
    SET
{{columnValuesAssignmentSqlList | indent(8,True)}}, 
        [Updated] = CURRENT_TIMESTAMP
    WHERE {{primaryKeySqlAssignment}}
END
GO

CREATE OR ALTER PROCEDURE [dbo].[{{TableName}}_Delete]
{{primaryKeySqlDeclaration | indent(4,True)}}
AS
BEGIN
    DELETE FROM [dbo].[{{TableName}}] WHERE {{primaryKeySqlAssignment}}
    SELECT @@ROWCOUNT as [Delete];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[{{TableName}}_GetAll]
AS
BEGIN
    SELECT * FROM dbo.[{{TableName}}];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[{{TableName}}_Get]
{{primaryKeySqlDeclaration | indent(4,True)}}
AS
BEGIN
    SELECT * 
        FROM dbo.[{{TableName}}]
        WHERE {{primaryKeySqlAssignment}}
END
GO

{% endif %}