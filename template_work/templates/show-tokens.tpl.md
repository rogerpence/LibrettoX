### File level tokens
 
dbname.................. {{dbname}}
library................. {{library}}
file.................... {{file}}
format.................. {{format}}
description............. {{description}}
type.................... {{type}}
recordlength............ {{recordlength}}
keylength............... {{keylength}}
basefile................ {{basefile}}
sqlserveruniqueindex.... {{sqlserveruniqueindex}}
alias................... {{alias}}
keyfieldslist........... {{keyfieldslist}}
allfieldslist........... {{allfieldslist}}

### Field level tokens

    {% for field in fields %}
    name.................... {{field.name}}
    description............. {{field.description}}
    alias................... {{field.alias}}
    fulltype................ {{field.fulltype}}
    type.................... {{field.type}}
    length.................. {{field.length}}
    decimals................ {{field.decimals}}
    systemtype.............. {{field.systemtype}}
    iskey................... {{field.iskey}}
    keyposition............. {{field.keyposition}}
    allownull............... {{field.allownull}}
    sqlservertype........... {{field.sqlservertype}}
    sqlservernull........... {{field.sqlservernull}}
    sqlserverprimarykey..... {{field.sqlserverprimarykey}}

    {% endfor %}

