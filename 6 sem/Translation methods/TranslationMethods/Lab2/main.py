import re
import pandas as pd
from openpyxl import Workbook
    

# Define regular expressions for a wider range of C# syntax elements

sample_code = ""
try:
    with open('source.txt', 'r') as file:
        sample_code = file.read()
except Exception:
    print("Error reading from file")

    
token_specs = [
    ('COMMENT',   r'//.*'),                      # Single-line comment
    ('MULTICOMMENT', r'/\*[\s\S]*?\*/'),         # Multi-line comment
    ('WHITESPACE',r'\s+'),                       # Whitespace
    ('ERROR1', r'\b(?:class|int|float|double|string|char|bool|object)\b\s*[\d!@#=%^&*~_$][a-zA-Z_]*\w*\s*='), # var naming
    ('ERROR2', r'(\w+\s*(\+|-){3,})|((\+|-){3,}\s*\w+)|;\s*([+-]\s*\w+)|(\w+\s*[+-]);'), #inc, dec
    ('ERROR3', r'\b(int|float|double|string|bool|char|object)\b\s+\b(int|float|double|string|bool|char|object)\b\s*='), # data types as vars
    ('ERROR4', r'\b(string|char)\s+\w+\s*=\s*[\d]+\s*([+\-*/><~&|^]\s*[\d]+\s*)+;'),
    ('ERROR5', r'\b(bool)\s+\w+\s*=\s*[\d]+\s*([&^|%+\-*/]\s*[\d]+\s*)+;'),
    ('ERROR6', r'\b(int|float|double|decimal)\s+\w+\s*=\s*[\d]+\s*(((&&)|(\|\|)|([><])|(!=))\s*[\d]+\s*)+;'),
    ('STRING',    r'["\'](?:\\.|[^"\\\'\n])*["\']'),         # String literal
    ('NUMBER',    r'\b(?:\d{1,3}(?:_\d{3})*(?:\.\d+)?|\d+(?:\.\d+)?(?:[eE][-+]?\d+)?)(?:[dfmDFM])?\b'),           # Integer or decimal number
    ('KEYWORD',   r'\b(?:class|struct|var|int|float|double|decimal|char|string|public|private|protected|static|foreach|do|if|else|for|while|switch|case|return|void|bool|true|false|null|new|object|using|namespace|default|break|not|in)\b'), # Keywords
    ('IDENTIFIER',r'\b[A-Za-z_]\w*\b'),          # Identifiers
    ('OPERATOR',  r'[\+\-\*/=%<>^~!&:.|?]+'),         # Operators
    ('DELIMITER', r'[{}()\[\],;]')             # Delimiters
]

# Compile the regular expressions
token_regex = '|'.join('(?P<%s>%s)' % pair for pair in token_specs)
# print(token_regex)
token_re = re.compile(token_regex)


token_dict = {}

# Tokenization function
def tokenize_enhanced_csharp_code(code):
    tokens = []

    i = 1 
    for m in token_re.finditer(code):
        token_type = m.lastgroup
        token_value = m.group(token_type)

        if token_type in ['STRING', 'NUMBER', 'KEYWORD', 'IDENTIFIER']:
            values = token_dict.values()
            if token_value not in values:
                key = f"id{i}"
                token_dict[key] = token_value
                i += 1 

        if token_type not in ['WHITESPACE', 'COMMENT', 'MULTICOMMENT']:
            tokens.append((token_type, token_value))
    return tokens


# Tokenize the sample code
enhanced_tokens = tokenize_enhanced_csharp_code(sample_code)

def categorize_tokens(tokens):
    categories = {
        'ERROR1': [],
        'ERROR2': [],
        'ERROR3': [],
        'ERROR4': [],
        'ERROR5': [],
        'ERROR6': [],
        'KEYWORDS': [],
        'IDENTIFIERS': [],
        'OPERATORS': [],
        'NUMBERS': [],
        'STRINGS': [],
        'DELIMITERS': []
    }

    for token_type, token_value in tokens:
        if token_type == 'ERROR1':
            categories['ERROR1'].append(token_value)
        elif token_type == 'ERROR2':
            categories['ERROR2'].append(token_value)
        elif token_type == 'ERROR3':
            categories['ERROR3'].append(token_value)
        elif token_type == 'ERROR4':
            categories['ERROR4'].append(token_value)
        elif token_type == 'ERROR5':
            categories['ERROR5'].append(token_value)
        elif token_type == 'ERROR6':
            categories['ERROR6'].append(token_value)
        elif token_type == 'KEYWORD':
            categories['KEYWORDS'].append(token_value)
        elif token_type == 'IDENTIFIER':
            categories['IDENTIFIERS'].append(token_value)
        elif token_type == 'OPERATOR':
            categories['OPERATORS'].append(token_value)
        elif token_type == 'NUMBER':
            categories['NUMBERS'].append(token_value)
        elif token_type == 'STRING':
            categories['STRINGS'].append(token_value)
        elif token_type == 'DELIMITER':
            categories['DELIMITERS'].append(token_value)
    
        # Заполняем пустые списки
    for key, value in categories.items():
        if not value:
            categories[key] = [None]

    return categories


# Use the categorize_tokens function on the enhanced tokens
categorized_tokens = categorize_tokens(enhanced_tokens)


for i in categorized_tokens:
    print('\n\n', i, categorized_tokens[i])

for key, value in token_dict.items():
    sample_code = sample_code.replace(value, key)

print(sample_code)

# for key, value in token_dict.items():
#     print(key, value)


for key, value in categorized_tokens.items():
    print(f"{key}: {len(value)}")

max_length = max(len(value) for value in categorized_tokens.values())
filled_tokens = {key: value + [''] * (max_length - len(value)) for key, value in categorized_tokens.items()}
df_categorized_tokens = pd.DataFrame(filled_tokens)

# Запись DataFrame в Excel-файл
with pd.ExcelWriter('categorized_tokens.xlsx') as writer:
    df_categorized_tokens.to_excel(writer, sheet_name='Categorized Tokens', index=False)

print("\n\nDataFrame для категоризированных токенов:")
print(df_categorized_tokens)

# Создание DataFrame для таблицы соответствия идентификаторов и их значений
df_token_dict = pd.DataFrame(token_dict.items(), columns=['Identifier', 'Value'])

# Запись DataFrame в Excel-файл
with pd.ExcelWriter('token_data.xlsx') as writer:
    df_token_dict.to_excel(writer, sheet_name='Token Dictionary', index=False)

print("\n\nDataFrame для таблицы соответствия идентификаторов и их значений:")
print(df_token_dict)