# from colorama import init 
from termcolor import colored

def print_error(text):
    print(colored(f'{text}', 'red'))

def print_info(text):
    print(colored(f'{text}', 'cyan'))

def print_warning(text):
    print(colored(f'{text}', 'yellow'))            

def print_success(text):
    print(colored(f'{text}', 'green'))

def print_input_prompt(text):
    print(colored(f'{text}', 'magenta'))

def is_key_in_dict(key, dict):
    if not key in dict:
        return False
    return True
