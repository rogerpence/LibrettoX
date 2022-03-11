    {% for file in files %}
    create-format-file.bat {{file.name}}
    {% endfor %}