    {% for file in files %}
    ExportDGFileToCSV.exe -d "*Public/Kabinart_GXD" -l / -f {{file.name}} -t 
    {% endfor %}