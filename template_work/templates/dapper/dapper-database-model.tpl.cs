/*
Model definition for an entity. 

Database......{{DatabaseName}}
Table.........{{TableName}}
Generated on..{{_datetime}}
Template......{{_template}}
Schema........{{_schema}}
*/

namespace DataAccess.Models
{
    public class {{TableName}}
    {
        {% for column in columns %}
        {% if column.ColumnName.lower() == 'added' or column.ColumnName.lower() == 'updated' %}
        public {{column.CSType}}? {{column.ColumnName}} {get;set;} = null;
        {% else %}
        public {{column.CSType}} {{column.ColumnName}} {get;set;} 
        {% endif %}
        {% endfor %}
    }
}