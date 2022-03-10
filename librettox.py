import args_processor as ap
import jinja2
import yaml 
import os, glob, sys 
import colorama
from termcolor import colored
from utils import * 

def load_template(template_file):

    full_template_path = os.path.join('template_work', 'templates', template_file)
    if not os.path.exists(full_template_path):
        print_error(f'Template file not found: {template_file}')
        sys.exit(1)

    template_filename = os.path.basename(full_template_path)
    template_path = os.path.dirname(full_template_path)
    template_loader = jinja2.FileSystemLoader(searchpath=template_path, encoding='utf8')
    template_env = jinja2.Environment(loader=template_loader, lstrip_blocks=True, trim_blocks=True)
    loaded_template = template_env.get_template(template_filename)
    return loaded_template

def get_schema_files(schema_file):
    schema_files = []

    if not '*' in schema:
        schema_file = os.path.join('template_work', 'schemas', schema_file)
        if not os.path.exists(schema_file):
            print_error(f'Schema file not found: {schema_file}')
            sys.exit(1)
        schema_files.append(schema_file)
    else:
        schema_path = os.path.join('template_work', 'schemas', schema_file)
        schema_files = [f for f in glob.glob(schema_path)]
        if len(schema_files) == 0:
            print_error(f'No schema files match pattern {schema_file}')
            sys.exit(1)

    return schema_files

def get_schema_file_contents(schema_file):
    with open(schema_file, 'r', encoding="utf-8") as f:
        schema_contents = yaml.safe_load(f)

    return schema_contents        

def get_output_file_name(template_arg, schemafile_arg, outdir_arg):
    template_filename, template_extension = os.path.splitext(template_arg)    
    template_filename_only, template_extension_only = os.path.splitext(template_filename)
    base_template_name = os.path.basename(template_filename_only)

    path, filename = os.path.split(schemafile_arg)
    filename_only, filename_extension = os.path.splitext(filename)
    output_path = os.path.join('template_work', 'output', outdir_arg)

    return os.path.join(output_path, base_template_name + '.' + filename_only + template_extension)

def vscode_debugger_is_active() -> bool:
    """Return true if the debugger is currently active"""
    gettrace = getattr(sys, 'gettrace', lambda : None) 
    return gettrace() is not None

if __name__ == '__main__':
    colorama.init()

    if vscode_debugger_is_active():
        template = 'file-schema.tpl.txt'
        schema = r'datagate\*.json' 
        # schema = 'datagate\\examples.states.json' 
        outdir = 'datagate-examples'
    else:      
        template, schema, outdir =  ap.get_command_line_args()

    if not os.path.exists(os.path.join('template_work', 'output', outdir )):
        print_error(f'Ouput path does not exists: {outdir}')
        sys.exit(1)

    loaded_template = load_template(template)
    schema_files = get_schema_files(schema)

    for schema_file in schema_files:
        schema_file_contents = get_schema_file_contents(schema_file)
        source_output = loaded_template.render(schema_file_contents)
        output_file_name = get_output_file_name(template, schema_file, outdir)
        with open(output_file_name, 'w', encoding='utf-8' ) as outfile:
            outfile.write(source_output)
        print_success(f'Wrote file: {output_file_name}')            
