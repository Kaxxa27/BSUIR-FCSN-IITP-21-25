def is_integer_type(s):
    try:
        int(s)
        return True
    except ValueError:
        return False


def is_float_type(s):
    try:
        float(s)
        return True
    except ValueError:
        return False


def is_character_type(s):
    return isinstance(s, str) and s.startswith('"') and s.endswith('"') and len(s) == 1


def is_bool(s):
    return s.lower() in ['true', 'false']


def is_string(s):
    return (isinstance(s, str) and s.startswith('"') and s.endswith('"')) or (isinstance(s, str) and s.startswith("'") and s.endswith("'"))

def is_library(s):
    return isinstance(s, str) and s.startswith('<') and s.endswith('>')