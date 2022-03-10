import argparse

def get_command_line_args():
    parser = argparse.ArgumentParser(description='LibrettoX: Create a source file from a template and schema')
    parser.add_argument('-t','--template', 
                        type=str, 
                        help='Template file name', 
                        required=True)
    parser.add_argument('-s','--schema',   
                        type=str, 
                        help='[schema folder]/*.json or [schema folder]/myschema.json', 
                        required=True)
    parser.add_argument('-o','--outdir',   
                        type=str, 
                        help='Output directory', 
                        required=True)
    args = vars(parser.parse_args())
    template = args['template']
    schema = args['schema']
    outdir = args['outdir']
    return template, schema, outdir
