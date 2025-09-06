import re

from constants import keywords, data_types, operators, standart_function, standart_libraries, containers, classes, \
    special_symbols, parenthesis
from type_checking import is_integer_type, is_character_type, is_float_type, is_bool, is_string

list_data_types = []
list_containers = []
lexical_error_tokens = []
allowed_symbols = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890'

table_identifier = []
table_key_words = []
table_operators = []

pattern = r'\((.*?)\)'

def check_match(element: str, identifiers):
    ident = [iden[0] for iden in identifiers]
    keyws = list(keywords.keys())
    datas = list(data_types.keys())
    conts = list(containers.keys())

    base = ident + datas + keyws + conts
    max_match = ""
    max_count = 0

    for string in base:
        count = 0
        i = 0
        while i < min(len(string), len(element)):
            if string[i] == element[i]:
                count += 1
            else:
                break
            i += 1
        if count > max_count:
            max_count = count
            max_match = string

    return max_match

def classify_token(token, prev_token, next_token, token_table):
    if token in keywords:
        if prev_token in token_table and prev_token in data_types:
            token_table[token] = 'SYNTAX ERROR!'
            return 'SYNTAX ERROR!'
        token_table[token] = keywords[token]
        table_key_words.append(token)
        return keywords[token]
    elif token in standart_function:
        token_table[token] = standart_function[token]
        return standart_function[token]
    elif token in standart_libraries:
        token_table[token] = standart_libraries[token]
        return standart_libraries[token]
    elif token in containers:
        list_containers.append(token)
        token_table[token] = containers[token]
        return containers[token]
    elif token in classes:
        token_table[token] = classes[token]
        return classes[token]
    elif token in parenthesis:
        token_table[token] = parenthesis[token]
        return parenthesis[token]
    elif token in operators:
        table_operators.append(token)
        if token == '/':
            if prev_token is None:
                token_table[token] = f'SYNTAX ERROR! {token}'
                return f'SYNTAX ERROR! {token}'
            if prev_token:
                if prev_token in token_table and prev_token in (';', '{', '}'):
                    token_table[token] = f'SYNTAX ERROR! {prev_token} {token}'
                    return f'SYNTAX ERROR! {prev_token} {token}'
        if token in ('<', '>'):
            if prev_token:
                if prev_token in ('<<', '>>'):
                    token_table[token] = f'SYNTAX ERROR! {prev_token} {token}'
                    return f'SYNTAX ERROR! {prev_token} {token}'
        if token == '~':
            if token_table[next_token] == 'ClASS':
                token_table[token] = 'TILDA'
                return 'TILDA'
            else:
                token_table[token] = 'BITWISE OPERATOR'
                return 'BITWISE OPERATOR'
        if token == '<':
            if prev_token in containers:
                token_table[token] = 'SIGN'
                return 'SIGN'
            else:
                token_table[token] = 'COMPARISON OPERATOR'
                return 'COMPARISON OPERATOR'
        if token == '>':
            if prev_token in data_types or (prev_token in token_table and token_table[prev_token] == 'CLASS'):
                token_table[token] = 'SIGN'
                return 'SIGN'
            else:
                token_table[token] = 'COMPARISON OPERATOR'
                return 'COMPARISON OPERATOR'
        if token == '*':
            if prev_token in data_types:
                token_table[token] = 'ASTERIK'
                return 'ASTERIK'
            if prev_token in token_table and token_table[prev_token] == 'CLASS':
                token_table[token] = 'CLASS POINTER'
                return 'CLASS POINTER'
            if len(list_data_types) != 0:
                if next_token == '(':
                    token_table[token] = 'ARITHMETIC OPERATOR'
                    return 'ARITHMETIC OPERATOR'
                if prev_token == ')':
                    if next_token == '(':
                        token_table[token] = 'ARITHMETIC OPERATOR'
                        return 'ARITHMETIC OPERATOR'
                    return 'ARITHMETIC OPERATOR'
                if ((prev_token in token_table and f'VARIABLE ({list_data_types[-1].upper()})' in token_table[
                    prev_token]) \
                        or (prev_token in token_table and f'FUNCTION DEC (FOINTER {list_data_types[-1].upper()})' in
                            token_table[prev_token]) \
                        or (prev_token in token_table and f'POINTER' in token_table[
                    prev_token]) \
                        or (prev_token in token_table and f'METHOD OF CLASS') \
                        or (prev_token in token_table and f'OBJECT' in token_table[prev_token]) \
                        or (prev_token in token_table and f'ARRAY (PAINTER {list_data_types[-1]})' in token_table[prev_token])) \
                        and ((next_token in token_table and f'VARIABLE ({list_data_types[-1].upper()})' in token_table[
                    next_token]) \
                        or (next_token in token_table and f'FUNCTION DEC (FOINTER {list_data_types[-1].upper()})' in
                            token_table[next_token]) \
                        or (next_token in token_table and f'POINTER' in token_table[
                    next_token]) \
                        or (next_token in token_table and f'ARRAY (PAINTER {list_data_types[-1]})' in token_table[
                            next_token])\
                        or (next_token in token_table and f'METHOD OF CLASS') ):
                    return 'ARITHMETIC OPERATOR'
                if prev_token in data_types and (next_token in token_table and f'VARIABLE ({list_data_types[-1].upper()})' in token_table[next_token]) \
                    or (next_token in token_table and f'FUNCTION DEC (FOINTER {list_data_types[-1].upper()})' in token_table[next_token]) \
                    or (next_token in token_table and f'POINTER' in token_table[next_token]) \
                    or (next_token in token_table and f'METHOD OF CLASS')\
                    or (next_token in token_table and f'ARRAY (PAINTER {list_data_types[-1]})'):
                    token_table[token] = 'ASTERIK'
                    return 'ASTERIK'
                elif prev_token in data_types or (next_token in token_table and f'VARIABLE ({list_data_types[-1].upper()})' in token_table[next_token]) \
                    or (next_token in token_table and f'FUNCTION DEC (FOINTER {list_data_types[-1].upper()})' in token_table[next_token]) \
                    or (next_token in token_table and f'POINTER' in token_table[next_token]) \
                    or (next_token in token_table and f'METHOD OF CLASS'):
                    token_table[token] = 'ASTERIK'
                    return 'ASTERIK'
                return 'ARITHMETIC OPERATOR'
        if token in ('++', '--', '&&', '=='):
            if prev_token:
                if prev_token in ('+', '-', '&', '=', '++', '--', '&&', '=='):
                    return 'LEXICAL ERROR'
        if token in ('+', '-', '&', '='):
            if prev_token:
                if prev_token in ('&&', '++', '--', '=='):
                    return 'LEXICAL ERROR'
        if token == '&':
            if prev_token in data_types or (prev_token in token_table and token_table[prev_token] == 'CLASS') \
                    or (prev_token in classes) or prev_token in keywords:
                token_table[token] = 'REFERENCE OPERATOR'
                return 'REFERENCE OPERATOR'
            else:
                token_table[token] = 'BITWISE OPERATOR'
                return 'BITWISE OPERATOR'
        if token == '.':
            if prev_token:
                if is_float_type(prev_token):
                    token_table[token] = f'LEXICAL ERROR! {prev_token} {token}'
                    return 'LEXICAL ERROR'
        if token == '=':
            if prev_token:
                if prev_token in data_types:
                    token_table[token] = f'SYNTAX ERROR! {prev_token} {token}'
                    return f'SYNTAX ERROR! {prev_token} {token}'
            if next_token:
                if next_token in (';', ',', '+', '/', '%'):
                    token_table[token] = f'SYNTAX ERROR! {token} {next_token}'
                    return f'SYNTAX ERROR! {token} {next_token}'

        token_table[token] = operators[token]
        return operators[token]
    elif token in data_types:
        if prev_token in ('/*', '//'):
            token_table[token] = 'STRING OF COMMENT'
            return 'STRING OF COMMENT'
        token_table[token] = data_types[token]

        list_data_types.append(token)
        return data_types[token]
    elif token.endswith('[]'):
        token_table[token] = 'ARRAY'
        return 'ARRAY'
    elif is_string(token):
        if prev_token == '#include':
            token_table[token] = 'HEADER FILE'
            return 'HEADER FILE'
        # if prev_token == '<<':
        #     token_table[token] = 'STRING OUTPUT'
        #     return 'STRING OUTPUT'
        token_table[token] = 'STRING'
        return 'STRING'
    elif is_integer_type(token):
        if prev_token == '.':
            return 'LEXICAL ERROR'
        token_table[token] = 'INTEGER'
        return 'INTEGER'
    elif is_float_type(token):
        token_table[token] = 'FLOAT'
        return 'FLOAT'
    elif is_bool(token):
        token_table[token] = 'BOOLEAN'
        return 'BOOLEAN'
    elif is_character_type(token):
        token_table[token] = 'CHAR'
        return 'CHAR'
    elif token.isidentifier():
        table_identifier.append(token)
        if prev_token in ('/*', '//'):
            token_table[token] = 'STRING OF COMMENT'
            return 'STRING OF COMMENT'
        if not all(char in allowed_symbols for char in token):
            if token[0].isdigit():
                lexical_error_tokens.append(token)
                return 'LEXICAL ERROR! Identifier cannot start with a digit.'
            return 'LEXICAL ERROR! Token contains invalid symbols.'
        if any(char in token for char in special_symbols):
            return 'LEXICAL ERROR! Special symbols detected!'
        if token[0].isdigit():
            lexical_error_tokens.append(token)
            return 'LEXICAL ERROR! Identifier cannot start with a digit.'
        if token in token_table:
            if next_token == '(' and token_table[token] == 'CLASS':
                return 'CONSTUCTURE'
            if 'FUNCTION DEC' in token_table[token]:
                return 'FUNCTION CALL'
            # if 'VARIABLE' in token_table[token]:
            #     match = re.search(pattern, token_table[token])
            #     if match:
            #         data_type = match.group(1)
            #         if list_data_types[-1] != data_type.lower():
            #             token_table[token] = f'VARIABLE ({data_type})'
            return token_table[token]
        else:
            if prev_token == '#define':
                token_table[token] = 'VARIABLE'
                token_table[token] = 'VARIABLE'
                return 'VARIABLE'
            if prev_token == '~' and next_token == '(':
                # token_table[token] = 'DESCTRUCTURE'
                return 'DESCTRUCTURE'
            if prev_token == '::':
                token_table[token] = 'METHOD'
                return 'METHOD'
            if prev_token == '>' and (prev_token in token_table and token_table[prev_token] == 'SIGN'):
                if len(list_containers) != 0:
                    token_table[token] = f'CONTAINER {list_containers[-1].upper()}'
                    return f'CONTAINER {list_containers[-1].upper()}'
            elif prev_token == 'using':
                token_table[token] = 'ALIAS'
                return 'ALIAS'
            elif next_token == '[':
                if prev_token in data_types:
                    token_table[token] = f'ARRAY ({list_data_types[-1]})'
                    return f'ARRAY ({list_data_types[-1]})'
                elif prev_token == '*':
                    if prev_token in token_table and token_table[prev_token] == 'ASTERIK':
                        token_table[token] = f'ARRAY (PAINTER {list_data_types[-1]})'
                        return f'ARRAY (PAINTER {list_data_types[-1]})'
            elif next_token == '(':
                if prev_token in data_types:
                    token_table[token] = f'FUNCTION DEC ({list_data_types[-1].upper()})'
                    return f'FUNCTION DEC ({list_data_types[-1].upper()})'
                elif prev_token == '*':
                    if prev_token in token_table and token_table[prev_token] == 'CLASS POINTER':
                        token_table[token] = f'METHOD OF CLASS'
                        return f'METHOD OF CLASS'
                    if len(list_data_types) != 0:
                        token_table[token] = f'FUNCTION DEC (FOINTER {list_data_types[-1].upper()})'
                    else:
                        token_table[token] = f'FUNCTION DEC (FOINTER)'
                        return f'FUNCTION DEC (FOINTER)'
                    return f'FUNCTION DEC (FOINTER {list_data_types[-1].upper()})'
            else:
                if prev_token == '*' and (prev_token in token_table and token_table[prev_token] == 'CLASS POINTER'):
                    token_table[token] = f'METHOD OF CLASS'
                    return f'METHOD OF CLASS'
                if prev_token in data_types or prev_token == ',':
                    if len(list_data_types) != 0:
                        token_table[token] = f'VARIABLE ({list_data_types[-1].upper()})'
                        return f'VARIABLE ({list_data_types[-1].upper()})'
                    else:
                        token_table[token] = f'VARIABLE'
                        return f'VARIABLE'
                if prev_token:
                    if 'VARIABLE' in token_table[prev_token]:
                        token_table[token] = f'SYNTAX ERROR! {prev_token} {token}'
                        return f'SYNTAX ERROR! {prev_token} {token}'
                    if prev_token == '*':
                        if len(list_data_types) != 0:
                            token_table[token] = f'POINTER ({list_data_types[-1].upper()})'
                            return f'POINTER ({list_data_types[-1].upper()})'
                            # token_table[token] = f'POINTER'
                            # return f'POINTER'
                        else:
                            token_table[token] = f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
                            return f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
            if next_token == '{':
                if prev_token in 'class':
                    token_table[token] = 'CLASS'
                    return 'CLASS'
                if prev_token in 'struct':
                    token_table[token] = 'STRUCTURE'
                    return 'STRUCTURE'
            elif prev_token in token_table and token_table[prev_token] == 'REFERENCE OPERATOR':
                token_table[token] = 'REFERENCE'
                return 'REFERENCE'
            elif prev_token in token_table and token_table[prev_token] == 'CLASS' or prev_token in classes:
                token_table[token] = f'OBJECT OF {prev_token}'
                return f'OBJECT OF {prev_token}'
            elif prev_token == '.':
                token_table[token] = 'METHOD'
                return 'METHOD'
            else:
                match = check_match(token, token_table.items())
                if match:
                    token_table[token] = f'SIMILAR TO: {match}'
                    return f'LEXICAL ERROR! SIMILAR TO: {match}'
                else:
                    return f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
    else:
        if prev_token in ('/*', '//'):
            token_table[token] = 'STRING OF COMMENT'
            return 'STRING OF COMMENT'
        elif prev_token == '#include':
            token_table[token] = 'HEADER FILE'
            return 'HEADER FILE'
        elif prev_token == '.':
            token_table[token] = 'METHOD'
            return 'METHOD'
        elif token.endswith(('LL', 'll', 'UL', 'ul')):
            if is_integer_type(token[:-2]):
                token_table[token] = 'INTEGER'
                return 'INTEGER'
            elif is_float_type(token[:-2]):
                token_table[token] = 'FLOAT'
                return 'FLOAT'
            else:
                token_table[token] = f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
                return f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
        elif token.endswith(('ULL', 'ull')):
            if is_integer_type(token[:-3]):
                token_table[token] = 'INTEGER'
                return 'INTEGER'
            elif is_float_type(token[:-3]):
                token_table[token] = 'FLOAT'
                return 'FLOAT'
            else:
                token_table[token] = f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
                return f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
        elif token.endswith(('L', 'l', 'F', 'f', 'D', 'd')):
            if is_integer_type(token[:-1]):
                token_table[token] = 'INTEGER'
                return 'INTEGER'
            elif is_float_type(token[:-1]):
                token_table[token] = 'FLOAT'
                return 'FLOAT'
            else:
                if token[0].isdigit():
                    return 'LEXICAL ERROR! Identifier cannot start with a digit.'
                if any(char in token for char in special_symbols):
                    return 'LEXICAL ERROR! Special symbols detected!'
                token_table[token] = f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
                return f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
        else:
            if next_token == '[':
                if prev_token in data_types:
                    token_table[token] = f'ARRAY ({list_data_types[-1]})'
                    return f'ARRAY ({list_data_types[-1]})'
                elif prev_token == '*':
                    if prev_token in token_table and token_table[prev_token] == 'ASTERIK':
                        token_table[token] = f'ARRAY (PAINTER {list_data_types[-1]})'
                        return f'ARRAY (PAINTER {list_data_types[-1]})'
            match = check_match(token, token_table.items())
            if match:
                token_table[token] = f'SIMILAR TO: {match}'
                return f'LEXICAL ERROR! SIMILAR TO: {match}'
            else:
                token_table[token] = f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'
                return f'LEXICAL ERROR! UNRECOGNIZED IDENTIFIER'