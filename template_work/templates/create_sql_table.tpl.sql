{#
    #prompt sqldb
#}

USE {{sqldb}}

DROP TABLE IF EXISTS {{file}}

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