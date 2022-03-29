

:: Refresh stored procedures
:: -S = server
:: -d = database
:: -E = use trusted connection
:: -i = SQL script 
:: -o = output file


sqlcmd -S DESKTOP-SKCVHEJ -d sugarfoot -E -i "dapper-crud-stored-procs_sugarfoot-Artist.sql" -o output.txt
sqlcmd -S DESKTOP-SKCVHEJ -d sugarfoot -E -i "dapper-crud-stored-procs_sugarfoot-Song.sql" -o output.txt