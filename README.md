## LibrettoX 

Many years ago I made a little template-based code generator with ASNA Visual RPG called Libretto. That old version was based on .NET FW WinForms and used my own, _very crude_ template engine. 

![](https://rogerpence.dev/wp-content/uploads/2022/03/AVRWIN_GeneratorCharlie_YgEHRLPaOX.png)

<small>Figure 1. The old Libretto fat client, circa 2006.</small>

This project is a new, from the ground-up, code generator. It reads a Json schema and a [Jinja2 template](https://jinja.palletsprojects.com/en/3.0.x/) to produce a source file. 

This version of the Libretto solves several limitations of the old Libretto:

* **Generate many output files at once**. The old Libretto was constrained (severely!) to producing one output file for one schema. This version can read all of the schemas in a directory and produce and output file for each schema found. 
* **Language agnostic**. The old Libretto was at least someone constrained by schemas that reflected only IBM i database idioms and conventions. This version is agnostic and can work with different schemas (although, templates are limited to working with only one template type.)
* **Vastly better templating capabilities**. The old Libretto's template engine was very weak. This version uses the very popular [Jinja2 template engine](https://jinja2docs.readthedocs.io/en/stable/). You can use all of its cool filters and other features in Libretto templates. 

>Old Libertto templates don't work with LibrettoX, but it's not too hard to convert them.

This version of Libretto was primarily intended to be a command-line only version. This core engine is written in Python (which is freely available for all platforms including [Windows 10](https://www.python.org/downloads/windows/)). But I did recently add a point-and-click UI for it (see below for more information).

>LibrettoX is platform agnostic. Given the correct schema, it can produce _any_ kind of output. Libretto was originally written for the IBM i (and similar databases) that use the term `library` and `file` where in SQL Server (or other SQL databases) these terms would usually be `database` and `table`. For Libretto's documentation purposes, you can use these terms interchangably.

### What does LibrettoX do

Given a Json schema and a template, it produces an output file for use with your favorite compiler or intepreter. 

![](https://rogerpence.dev/wp-content/uploads/2022/03/l0CkJzFwxz.png)

Schema file: `examples.states.json`

```
{
  "dbname": "*public/dg net local",
  "library": "examples",
  "file": "states",
  "format": "rstates",
  "description": "",
  "type": "physical",
  "recordlength": "50",
  "keylength": "48",
  "basefile": "",
  "duplicatekeys": "not allowed",
  "sqlserveruniqueindex": "unique",
  "alias": "states",
  "keyfieldslist": "state",
  "allfieldslist": "state, abbrev",
  "fields": [
    {
      "name": "state",
      "description": "",
      "alias": "state",
      "fulltype": "Type(*char) Len(48)",
      "type": "*char",
      "length": "48",
      "decimals": "",
      "systemtype": "System.String",
      "iskey": true,
      "keyposition": 0,
      "allownull": false,
      "sqlservertype": "varchar(48)",
      "sqlservernull": "NOT NULL",
      "sqlserverprimarykey": "PRIMARY KEY"
    },
    {
      "name": "abbrev",
      "description": "",
      "alias": "abbrev",
      "fulltype": "Type(*char) Len(2)",
      "type": "*char",
      "length": "2",
      "decimals": "",
      "systemtype": "System.String",
      "iskey": false,
      "keyposition": -1,
      "allownull": false,
      "sqlservertype": "varchar(2)",
      "sqlservernull": "NOT NULL",
      "sqlserverprimarykey": ""
    }
  ],
  "keyfields": [
    {
      "name": "state",
      "description": "",
      "alias": "state",
      "fulltype": "Type(*char) Len(48)",
      "type": "*char",
      "length": "48",
      "decimals": "",
      "systemtype": "System.String",
      "iskey": true,
      "keyposition": 0,
      "allownull": false,
      "sqlservertype": "varchar(48)",
      "sqlservernull": "NOT NULL",
      "sqlserverprimarykey": "PRIMARY KEY"
    }
  ],
  "nonkeyfields": [
    {
      "name": "abbrev",
      "description": "",
      "alias": "abbrev",
      "fulltype": "Type(*char) Len(2)",
      "type": "*char",
      "length": "2",
      "decimals": "",
      "systemtype": "System.String",
      "iskey": false,
      "keyposition": -1,
      "allownull": false,
      "sqlservertype": "varchar(2)",
      "sqlservernull": "NOT NULL",
      "sqlserverprimarykey": ""
    }
  ]
}
```

Template file: `file-schema.tpl.txt`

```
Database Name.: {{dbname}}
Library.......: {{library}}
File..........: {{file}}
Format........: {{format}}
Key field(s)..: {{keyfieldslist}}
Type..........: {{type}}
Base file.....: {{basefile}}
Description...: {{description}}
Record length.: {{recordlength}}

Field name        Data type                    Description
----------------------------------------------------------------------------
{% if keyfields|length > 0 %}
{% for field in keyfields %}
{{field.name.ljust(17)}} {{field.fulltype.ljust(28)}} {{field.description}}
{% endfor %}

{% endif %}
{% for field in nonkeyfields %}
{{field.name.ljust(17)}} {{field.fulltype.ljust(28)}} {{field.description}}
{% endfor %}
----------------------------------------------------------------------------
```

The Schema and Template above produce this file: `file-schema.examples.states.txt`

```
Database Name.: *public/dg net local
Library.......: examples
File..........: states
Format........: rstates
Key field(s)..: state
Type..........: physical
Base file.....: 
Description...: 
Record length.: 50

Field name        Data type                    Description
----------------------------------------------------------------------------
state             Type(*char) Len(48)          
abbrev            Type(*char) Len(2)           
----------------------------------------------------------------------------
```

### Naming conventions

The template naming file convention is:

```
*.tpl.xxx
```

where '*' is the file name and 'xxx' is the extension used for the output file. 

The convention for schema file names is:

```
[database].[table].json
```

If template is named `file-schema.tpl.txt`. Using the schema named `examples.cmastnewl2.json` with that template results in this output file name:

```
file-schema.examples.cmastnewl2.txt
```

Generally, you should be able to look at an output file and be able to quickly determine:

The template name from which it was derived
The database and table for which the output file applies
What language the template its is for (with its extension)

### Producing the output 

To produce this output you would either: 

Run this command line from the `libretto.py` parent folder:

```
Python librettox.py -t "file-schema.tpl.txt" -s "datagate_examples\examples_states.json" -o "datagate"
```
or use the LibrettoXUI point-and-click UI:

![](https://rogerpence.dev/wp-content/uploads/2022/03/LibrettoUI-2_JDLQCnt93S.png)

The LibrettoUIX point-and-click interface is a relatively new addition to Libretto. [Read more about it here.](https://github.com/rogerpence/librettoUIX) This UI is a Windows WPF application that is essentially a front-end that builds the command lines that `librettox.py` needs. This UI doesn't directly perform template translation, under the covers it launches a process that runs `libretto.py` to do that. () 

### Command line arguments

#### --template, -t

This specifies the Jinja template to use. 

#### --schema, -s 

Schema files provide the primary data for a Jinja2 template. Schema files can be either `Json` or `Yaml` format. 

Schema files are expected to be in a folder immediately under the folder where `librettoiii.py` is located.  

This specifies the schema file used. It can be either be a single file:

    -s schema/examples_cmastnewl2.yaml

or multiple files 

    -s schema/examples_*.json

When multiple files are specified, the template processor prompts for prompted values once before all the schema files are processed (thereby ensuring that the template uses the same value for every schema file in the group).

#### --outdir, -o 

The output directory. 