{#
    #prompt sqldb
#}

USE {{sqldb}}

DROP TABLE IF EXISTS {{file}}


If EXISTS(SELECT * 
          FROM sys.tables
		  WHERE SCHEMA_NAME(schema_id) LIKE 'dbo'
		  AND name LIKE '{{file}}');

CREATE TABLE {{file}} (
    {% for field in fields %}
    [{{field.name}}] {{field.sqlservertype}} {{field.sqlservernull}},
    {% endfor %}
    
    {% if not duplicatekeys == 'allowed'%}
    {% if keyfieldslist is defined and keyfieldslist %}
    primary key ({{keyfieldslist}})
    {% endif %}
    {% endif %}
);