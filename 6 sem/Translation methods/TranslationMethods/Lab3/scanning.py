def tokenize(code):
    tokens = []
    current_token = ''
    inside_string = False
    inside_comment = False
    line_number = 1
    char_position = 0

    i = 0
    while i < len(code):
        char = code[i]
        char_position += 1

        if code[i] == '\n':
            line_number += 1
            char_position = 0
            inside_comment = False

        if char == '\\':
            if i + 1 < len(code):
                next_char = code[i + 1]
                if next_char in ('r', 'n', 't', '\\'):
                    current_token += char + next_char
                    i += 1
                else:
                    current_token += char
            else:
                current_token += char
        elif (char == '"' or char == "'") and not inside_string:
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
            current_token += char
            inside_string = True
        elif (char == '"' or char == "'") and inside_string:
            current_token += char
            tokens.append((current_token, line_number))
            current_token = ''
            inside_string = False
        elif inside_string:
            current_token += char
        elif char == '/' and i + 1 < len(code) and code[i + 1] == '/':
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
            current_token += char + code[i + 1]
            i += 1
            tokens.append((current_token, line_number))
            current_token = ''
            i += 1
            while i < len(code) and code[i] != '\n':
                current_token += code[i]
                i += 1
            tokens.append((current_token.strip(), line_number))
            # if code[i] == '\n':
            #     line_number += 1
            current_token = ''
        elif char == '/' and i + 1 < len(code) and code[i + 1] == '*':
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
            tokens.append(('/*', line_number))
            inside_comment = True
            i += 1
        elif char == '*' and i + 1 < len(code) and code[i + 1] == '/':
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
            current_token = '*/'
            inside_comment = False
            tokens.append((current_token, line_number))
            current_token = ''
            i += 1
        elif inside_comment:
            current_token += char
        elif char in ('№','@','#','$'):
            current_token += char
            i += 1
            while i < len(code) and not code[i].isspace():
                current_token += code[i]
                i += 1
            tokens.append((current_token, line_number))
            current_token = ''
        elif char in ('(', ')', '{', '}', '[', ']', ',', ';', '<', '>'):
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
            tokens.append((char, line_number))
        elif char.isspace():
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
        elif char in ('(', ')', '{', '}', '[', ']', ',', ';', '<', '>'):
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
            tokens.append((char, line_number))
        elif char.isspace():
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
        elif char == '.':
            if current_token.isdigit() and i + 1 < len(code) and code[i + 1].isdigit():
                current_token += char
            elif current_token:
                tokens.append((current_token, line_number))
                tokens.append((char, line_number))
                current_token = ''
            else:
                tokens.append((char, line_number))
        elif char.isalnum() or char == '_':
            current_token += char
        elif char.isdigit() and i + 1 < len(code) and code[i + 1] in ('e', 'E'):
            current_token += char
            current_token += code[i + 1]
            i += 1
            if i + 1 < len(code) and code[i + 1] in ('+', '-'):
                current_token += code[i + 1]
                i += 1
            while i + 1 < len(code) and code[i + 1].isdigit():
                current_token += code[i + 1]
                i += 1
            tokens.append((current_token, line_number))
            current_token = ''
        elif char.isdigit() and current_token.endswith(('l', 'L', 'u', 'U')):
            tokens.append((current_token[:-1], line_number))
            tokens.append((current_token[-1], line_number))
            current_token = ''
            current_token += char
        elif current_token.endswith(('ll', 'LL', 'uu', 'UU', 'el', 'EL')):
            tokens.append((current_token[:-2], line_number))
            tokens.append((current_token[-2], line_number))
            current_token = ''
            current_token += char
        else:
            if current_token:
                tokens.append((current_token, line_number))
                current_token = ''
            tokens.append((char, line_number))

        i += 1

    if current_token:
        tokens.append((current_token, line_number))

    combined_tokens = []
    i = 0

    while i < len(tokens):
        list_tokens = [token for token, _ in tokens]
        token_lines = [line for _, line in tokens]

        if i > 0 and list_tokens[i - 1] == '#include' and list_tokens[i] == '<' and list_tokens[i + 1] != '<':
            end_index = i + 1
            while end_index < len(list_tokens) and list_tokens[end_index] != '>':
                end_index += 1

            if end_index < len(list_tokens) and list_tokens[end_index] == '>':
                combined_tokens.append(('<' + ''.join(list_tokens[i + 1:end_index]) + '>', token_lines[i]))
                i = end_index + 1
        elif list_tokens[i:i + 2] == ['<', '<']:
            combined_tokens.append(('<<', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['>', '>']:
            combined_tokens.append(('>>', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == [':', ':']:
            combined_tokens.append(('::', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['-', '>']:
            combined_tokens.append(('->', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['+', '+']:
            combined_tokens.append(('++', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['-', '-']:
            combined_tokens.append(('--', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['+', '=']:
            combined_tokens.append(('+=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['-', '=']:
            combined_tokens.append(('-=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['*', '=']:
            combined_tokens.append(('*=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['/', '=']:
            combined_tokens.append(('/=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['%', '=']:
            combined_tokens.append(('%=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['=', '=']:
            combined_tokens.append(('==', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['!', '=']:
            combined_tokens.append(('!=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['>', '=']:
            combined_tokens.append(('>=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['<', '=']:
            combined_tokens.append(('<=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['&', '&']:
            combined_tokens.append(('&&', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['|', '|']:
            combined_tokens.append(('||', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 3] == ['>', '>', '=']:
            combined_tokens.append(('>>=', token_lines[i]))
            i += 3
        elif list_tokens[i:i + 3] == ['<', '<', '=']:
            combined_tokens.append(('<<=', token_lines[i]))
            i += 3
        elif list_tokens[i:i + 2] == ['&', '=']:
            combined_tokens.append(('&=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['|', '=']:
            combined_tokens.append(('|=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['^', '=']:
            combined_tokens.append(('^=', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['/', '/']:
            combined_tokens.append(('//', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['/', '*']:
            combined_tokens.append(('/*', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['*', '/']:
            combined_tokens.append(('*/', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 3] == ['unsigned', 'long', 'long']:
            combined_tokens.append(('unsigned long long', token_lines[i]))
            i += 3
        elif list_tokens[i:i + 2] == ['unsigned', 'char']:
            combined_tokens.append(('unsigned char', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['unsigned', 'short']:
            combined_tokens.append(('unsigned short', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['unsigned', 'int']:
            combined_tokens.append(('unsigned int', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['unsigned', 'long']:
            combined_tokens.append(('unsigned long', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['long', 'long']:
            combined_tokens.append(('long long', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['long', 'int']:
            combined_tokens.append(('long int', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['short', 'int']:
            combined_tokens.append(('short int', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['long', 'double']:
            combined_tokens.append(('long double', token_lines[i]))
            i += 2
        elif list_tokens[i:i + 2] == ['signed', 'char']:
            combined_tokens.append(('signed char', token_lines[i]))
            i += 2
        else:
            combined_tokens.append((list_tokens[i], token_lines[i]))
            i += 1


    return combined_tokens

