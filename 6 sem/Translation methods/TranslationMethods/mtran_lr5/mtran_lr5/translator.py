from sys import executable
from parser_ import parse
from function import write_output_to_file

import subprocess

class Translator:
    def __init__(self):
        self.code_python = ''
        self.private_member = []
        self.variable_list = []
        self.init_list = []
        self.temp_list = ''
        self.init_code = []
        self.in_class = False
        self.in_if = False
        self.in_for = False
        self.in_access = False
        self.in_init = False
        self.depth = 0
        self.temp_depth = 0
        self.count_compare = 0

    def declare_translator(self, node):
        if self.code_python[-1] == '\n':
            self.code_python += '\t' * self.depth
        # self.init_code += '\t' * self.depth
        variable_exists = False
        for name, _ in self.variable_list:
            if node.name == name:
                variable_exists = True
                break

        if not variable_exists:
            if self.in_class:
                self.variable_list.append((node.name, True))
            else:
                self.variable_list.append((node.name, False))

        for name, check in self.variable_list:
            if node.name == name:
                if check:
                    if not self.in_access:
                        self.code_python += f'self.{node.name}'
                    else:
                        self.init_code.append('self.'+node.name)
                        self.in_access = False
                else:
                    self.code_python += f'{node.name}'
                break

        for member_declare in node.children:
            if member_declare.type == 'Assignment':
                assignment_node = member_declare.name
                self.code_python += f' {assignment_node} '
            if member_declare.type == 'Value':
                if member_declare.name == "' '":
                    self.code_python += f', {member_declare.name}'
                else:
                    value_node = member_declare.name
                    self.code_python += f'{value_node}'
            if member_declare.type == 'Variable':
                self.declare_translator(member_declare)
            if member_declare.type == 'Operator':
                operator_node = member_declare.name
                self.code_python += f' {operator_node}'
            if member_declare.type == 'DotOperator':
                self.code_python += member_declare.name
            if member_declare.type == 'Method f':
                if member_declare.name == 'size':
                    temp_len = len(node.name) + 1
                    self.code_python = self.code_python[:-temp_len - 1] + f' len({node.name})'
                else:
                    self.code_python += member_declare.name
                    self.function_translator(member_declare)
            if member_declare.type == 'Comparison':
                if self.in_for and not self.in_if:
                    self.code_python = self.code_python[:-1]
                else:
                    self.code_python += member_declare.name
            if member_declare.type == 'Block' and (member_declare.name == 'Declare Array' or member_declare.name == 'Array'):
                self.code_python += '['
                self.block_translator(member_declare)
            if member_declare.type == 'End Block':
                self.code_python += ']'
            if member_declare.type == 'Square Block' and node.type == 'Array':
                self.code_python += '['
                self.block_translator(member_declare)
            if member_declare.type == 'End Square Block' and node.type == 'Array':
                self.code_python += ']'
            if member_declare.type == 'Array':
                self.declare_translator(member_declare)
            if 'Semantic' in member_declare.type or 'SEMANTIC' in member_declare.type:
                #print(self.code_python)
                self.code_python += member_declare.type
                break
            if 'Syntax' in member_declare.type or 'SYNTAX' in member_declare.type:
                self.code_python += member_declare.type
                #print(self.code_python)
                break
            if 'Lexical' in member_declare.type or 'LEXICAL' in member_declare.type:
                self.code_python += member_declare.type
                #print(self.code_python)
                break

    def parameters_translator(self, node):
        for member_param in node.children:
            if member_param.type == 'Variable':
                self.declare_translator(member_param)
            if member_param.type == 'Declare':
                if self.in_init:
                    self.init_list.append(member_param.name)
                if self.in_class:
                    self.in_class = False
                    self.declare_translator(member_param)
                    self.in_class = True
                else:
                    self.declare_translator(member_param)
            if member_param.type == 'Array':
                self.declare_translator(member_param)
            if member_param.type == 'Comma':
                self.code_python += member_param.name
            if member_param.type == 'Value':
                self.declare_translator(member_param)
            if 'Semantic' in member_param.type or 'SEMANTIC' in member_param.type:
                #print(self.code_python)
                self.code_python += member_param.type
                break
            if 'Syntax' in member_param.type or 'SYNTAX' in member_param.type:
                self.code_python += member_param.type
                #print(self.code_python)
                break
            if 'Lexical' in member_param.type or 'LEXICAL' in member_param.type:
                self.code_python += member_param.type
                #print(self.code_python)
                break

    def translate(self, file):
        source_file = r"C:\Users\Lenovo\Desktop\test.cpp"
        executable_file = r"C:\Study_BSUIR\Term 6\Translation methods\TranslationMethods\mtran_lr5\mtran_lr5\test\test.exe"
        compile_command = r"C:\Tools\MSYS2\MSYS64\ucrt64\bin\g++.exe {} -o test\test.exe".format(source_file)
        subprocess.run(compile_command, shell=True)
        subprocess.run(executable_file, shell=True)

    def function_translator(self, node):
        for member_function in node.children:
            if member_function.type == 'Parameters':
                self.temp_depth = self.depth
                self.depth = 0
                if self.in_class:
                    self.code_python += '(self, '
                    self.parameters_translator(member_function)
                    self.code_python += ')'
                else:
                    self.code_python += '('
                    self.parameters_translator(member_function)
                    self.code_python += ')'
                self.depth = self.temp_depth
            if member_function.type == 'Block':
                self.depth += 1
                self.code_python += f':\n'
                self.block_translator(member_function)
            if member_function.type == 'End Block':
                if self.in_init:
                    self.in_init = False
                self.depth -= 1
                self.code_python += '\n'
            if 'Semantic' in member_function.type or 'SEMANTIC' in member_function.type:
                #print(self.code_python)
                self.code_python += member_function.type
                break
            if 'Syntax' in member_function.type or 'SYNTAX' in member_function.type:
                self.code_python += member_function.type
                #print(self.code_python)
                break
            if 'Lexical' in member_function.type or 'LEXICAL' in member_function.type:
                self.code_python += member_function.type
                #print(self.code_python)
                break

    def main_translator(self, node):
        for member_function in node.children:
            if member_function.type == 'Block':
                self.depth += 1
                self.code_python += f':\n'
                self.block_translator(member_function)
            if member_function.type == 'End Block':
                self.depth -= 1
                self.code_python += '\n'
            if 'Semantic' in member_function.type or 'SEMANTIC' in member_function.type:
                #print(self.code_python)
                self.code_python += member_function.type
                break
            if 'Syntax' in member_function.type or 'SYNTAX' in member_function.type:
                self.code_python += member_function.type
                #print(self.code_python)
                break
            if 'Lexical' in member_function.type or 'LEXICAL' in member_function.type:
                self.code_python += member_function.type
                #print(self.code_python)
                break

    def access_specifier_translator(self, node):
        for member_access in node.children:
            if member_access.type == 'Declare':
                self.in_access = True
                self.declare_translator(member_access)
            if member_access.type == 'Statement':
                self.code_python += '\n'
            if member_access.type == 'Constructure':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += f'def __init__'
                self.in_init = True
                self.function_translator(member_access)
            if member_access.type == 'Function':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += f'def {member_access.name}'
                self.function_translator(member_access)
            if 'Semantic' in member_access.type or 'SEMANTIC' in member_access.type:
                #print(self.code_python)
                self.code_python += member_access.type
                break
            if 'Syntax' in member_access.type or 'SYNTAX' in member_access.type:
                self.code_python += member_access.type
                #print(self.code_python)
                break
            if 'Lexical' in member_access.type or 'LEXICAL' in member_access.type:
                self.code_python += member_access.type
                #print(self.code_python)
                break

    def return_translator(self, node):
        for member_return in node.children:
            if member_return.type == 'Variable':
                self.declare_translator(member_return)
            if member_return.type == 'Statement':
                self.code_python += '\n'
            if member_return.type == 'Value':
                self.declare_translator(member_return)
            if 'Semantic' in member_return.type or 'SEMANTIC' in member_return.type:
                #print(self.code_python)
                self.code_python += member_return.type
                break
            if 'Syntax' in member_return.type or 'SYNTAX' in member_return.type:
                self.code_python += member_return.type
                #print(self.code_python)
                break
            if 'Lexical' in member_return.type or 'LEXICAL' in member_return.type:
                self.code_python += member_return.type
                #print(self.code_python)
                break

    def object_translator(self, node):
        for member_object in node.children:
            if member_object.type == 'DotOperator':
                self.code_python += member_object.name
            if member_object.type == 'Function Call':
                self.code_python += member_object.name
                self.function_translator(member_object)
            if 'Semantic' in member_object.type or 'SEMANTIC' in member_object.type:
                #print(self.code_python)
                self.code_python += member_object.type
                break
            if 'Syntax' in member_object.type or 'SYNTAX' in member_object.type:
                self.code_python += member_object.type
                #print(self.code_python)
                break
            if 'Lexical' in member_object.type or 'LEXICAL' in member_object.type:
                self.code_python += member_object.type
                #print(self.code_python)
                break

    def print_translator(self, node):
        for member_print in node.children:
            if member_print.type == 'Value':
                self.declare_translator(member_print)
                self.code_python += ', '
            if member_print.type == 'Object':
                self.code_python += member_print.name
                self.object_translator(member_print)
                self.code_python += ', '
            if member_print.type == 'Variable':
                self.declare_translator(member_print)
                self.code_python += ', '
            if member_print.type == 'Array':
                self.declare_translator(member_print)
                self.code_python += ', '
            if member_print.type == 'Operator':
                if self.code_python[-2] == ',' and self.code_python[-1] == ' ':
                    self.code_python = self.code_python[:-2] + member_print.name
                # self.code_python += member_print.name
            if 'Semantic' in member_print.type or 'SEMANTIC' in member_print.type:
                #print(self.code_python)
                self.code_python += member_print.type
                break
            if 'Syntax' in member_print.type or 'SYNTAX' in member_print.type:
                self.code_python += member_print.type
                #print(self.code_python)
                break
            if 'Lexical' in member_print.type or 'LEXICAL' in member_print.type:
                self.code_python += member_print.type
                #print(self.code_python)
                break

    def for_param_translator(self, node):
        count = 0
        count_comparison = 0
        for member_param in node.children:
            if member_param.type == 'Statement':
                count += 1
            if count == 0:
                if member_param.type in ('Declare', 'Variable'):
                    self.code_python += member_param.name
            if count == 1:
                if member_param.type in ('Declare', 'Variable'):
                    self.code_python += ' in range(3)'
            if 'Semantic' in member_param.type or 'SEMANTIC' in member_param.type:
                #print(self.code_python)
                self.code_python += member_param.type
                break
            if 'Syntax' in member_param.type or 'SYNTAX' in member_param.type:
                self.code_python += member_param.type
                #print(self.code_python)
                break
            if 'Lexical' in member_param.type or 'LEXICAL' in member_param.type:
                self.code_python += member_param.type
                #print(self.code_python)
                break

    def for_translator(self, node):
        for member_for in node.children:
            if member_for.type == 'Parameters':
                self.for_param_translator(member_for)
            if member_for.type == 'Block':
                self.depth += 1
                self.code_python += f':\n'
                self.block_translator(member_for)
            if member_for.type == 'End Block':
                self.depth -= 1
                self.in_for = False
                self.code_python += '\n'
            if 'Semantic' in member_for.type or 'SEMANTIC' in member_for.type:
                #print(self.code_python)
                self.code_python += member_for.type
                break
            if 'Syntax' in member_for.type or 'SYNTAX' in member_for.type:
                self.code_python += member_for.type
                #print(self.code_python)
                break
            if 'Lexical' in member_for.type or 'LEXICAL' in member_for.type:
                self.code_python += member_for.type
                #print(self.code_python)
                break

    def if_translator(self, node):
        for member_if in node.children:
            if member_if.type == 'Parameters':
                self.parameters_translator(member_if)
            if member_if.type == 'Block':
                self.depth += 1
                self.code_python += f':\n'
                self.block_translator(member_if)
            if member_if.type == 'End Block':
                self.in_if = False
                self.depth -= 1
                self.code_python += '\n'
            if 'Semantic' in member_if.type or 'SEMANTIC' in member_if.type:
                #print(self.code_python)
                self.code_python += member_if.type
                break
            if 'Syntax' in member_if.type or 'SYNTAX' in member_if.type:
                self.code_python += member_if.type
                #print(self.code_python)
                break
            if 'Lexical' in member_if.type or 'LEXICAL' in member_if.type:
                self.code_python += member_if.type
                #print(self.code_python)
                break

    def cin_translator(self, node):
        for member_cin in node.children:
            if member_cin.type == 'Variable' and member_cin.data_type == 'int':
                self.code_python += f'{member_cin.name} = int({self.temp_list})'
            if 'Semantic' in member_cin.type or 'SEMANTIC' in member_cin.type:
                #print(self.code_python)
                self.code_python += member_cin.type
                break
            if 'Syntax' in member_cin.type or 'SYNTAX' in member_cin.type:
                self.code_python += member_cin.type
                #print(self.code_python)
                break
            if 'Lexical' in member_cin.type or 'LEXICAL' in member_cin.type:
                self.code_python += member_cin.type
                #print(self.code_python)
                break

    def block_translator(self, node):
        for member, value in zip(self.init_code, self.init_list):
            if self.in_init:
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += f'{member} = {value}\n'

        parent_node = node.parent
        for member_block in node.children:
            if member_block.name in ('public', 'protected', 'private'):
                self.access_specifier_translator(member_block)
            if member_block.type == 'Variable':
                self.declare_translator(member_block)
            if member_block.type == 'Declare':
                if member_block.children:
                    self.declare_translator(member_block)
                else:
                    if self.code_python[-1] == '\n':
                        self.code_python += '\t' * self.depth

                    variable_exists = False
                    for name, _ in self.variable_list:
                        if member_block.name == name:
                            variable_exists = True
                            break

                    if not variable_exists:
                        if self.in_class:
                            self.variable_list.append((member_block.name, True))
                        else:
                            self.variable_list.append((member_block.name, False))

                    for name, check in self.variable_list:
                        if member_block.name == name:
                            if check:
                                self.code_python += f'self.{member_block.name}'
                            else:
                                self.code_python += f'{member_block.name}'
                            break

                    if member_block.data_type in ('int', 'long long', 'long', 'short', 'unsigned short', 'unsigned int', \
                                          'unsigned long long', 'unsigned long', 'float', 'double', 'long double'):
                        self.code_python += f' = 0'
                    if member_block.data_type in ('str', 'string', 'signed char', 'char', 'unsigned char', 'wchar_t', 'char8_t', 'char16_t', 'char32_t'):
                        self.code_python += f"= ''"
            if member_block.type == 'Declare Array':
                self.declare_translator(member_block)
            if member_block.type == 'Array':
                self.declare_translator(member_block)
            if member_block.type == 'Statement':
                if self.code_python[-2] == ',' and self.code_python[-1] == ' ':
                    self.code_python = self.code_python[:-2] + ',end="")'
                if self.code_python[-1] == '(':
                    self.code_python += ')'
                # self.init_code += '\n'
                self.code_python += '\n'
            if member_block.type == 'Block':
                self.code_python += '['
                self.block_translator(member_block)
            if member_block.type == 'End Block':
                self.code_python += ']'
                self.block_translator(member_block)
            if member_block.type == 'ReturnStatement':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                if parent_node.name != 'main':
                    self.code_python += f'{member_block.name} '
                    self.return_translator(member_block)
                else:
                    continue
            if member_block.type == 'Function Call':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += member_block.name
                self.function_translator(member_block)
            if member_block.type == 'Class':
                self.temp_list = member_block
                self.class_translator(member_block)
            if member_block.type == 'Cout':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += 'print('
                self.print_translator(member_block)
            if member_block.type == 'Cin':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.temp_list = 'input()'
                self.cin_translator(member_block)
            if member_block.type == 'Endl':
                if self.code_python[-2] == ',' and self.code_python[-1] == ' ':
                    self.code_python = self.code_python[:-2] + ')'
                else:
                    self.code_python += ')'
            if member_block.type == 'Object':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += member_block.name
                self.object_translator(member_block)
            if member_block.type == 'ForLoop':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.in_for = True
                self.code_python += 'for '
                self.for_translator(member_block)
            if member_block.type == 'IfStatement':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.in_if = True
                self.code_python += 'if '
                self.if_translator(member_block)
            if member_block.type == 'ElseIfStatement':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.in_if = True
                self.code_python += 'elif '
                self.if_translator(member_block)
            if member_block.type == 'ElseStatement':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += 'else'
                self.if_translator(member_block)
            if member_block.type == 'Break':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += 'break'
            if member_block.type == 'Value':
                self.declare_translator(member_block)
            if member_block.type == 'Comma':
                self.code_python += ', '
            if 'Semantic' in member_block.type or 'SEMANTIC' in member_block.type:
                #print(self.code_python)
                self.code_python += member_block.type
                break
            if 'Syntax' in member_block.type or 'SYNTAX' in member_block.type:
                self.code_python += member_block.type
                #print(self.code_python)
                break
            if 'Lexical' in member_block.type or 'LEXICAL' in member_block.type:
                self.code_python += member_block.type
                #print(self.code_python)
                break


    def class_translator(self, node):
        for member_class in node.children:
            if member_class.type == 'Block':
                self.depth += 1
                self.code_python += f':\n'
                self.block_translator(member_class)
            if member_class.type == 'End Block':
                self.depth -= 1
                self.in_class = False
                self.code_python += '\n'
            if member_class.type == 'Object':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.code_python += f'{member_class.name} =  {self.temp_list.name}'
                self.function_translator(member_class)
            if 'Semantic' in member_class.type or 'SEMANTIC' in member_class.type:
                #print(self.code_python)
                self.code_python += member_class.type
                break
            if 'Syntax' in member_class.type or 'SYNTAX' in member_class.type:
                self.code_python += member_class.type
                #print(self.code_python)
                break
            if 'Lexical' in member_class.type or 'LEXICAL' in member_class.type:
                self.code_python += member_class.type
                #print(self.code_python)
                break

    def replace_goto_with_while(code):
        lines = code.split('\n')
        if self.code_python[-1] == '\n':
            variable_exists = False
            for name, _ in self.variable_list:
                if member_block.name == name:
                    variable_exists = True
                    break

            if not variable_exists:
                if self.in_class:
                    self.variable_list.append((member_block.name, True))
                else:
                    self.variable_list.append((member_block.name, False))

        for line in lines:
            if 'goto' in line:
                line = line.replace('goto', 'while True:')
            self.new_code.append(line)

    def translator(self, syntax_tree):
        root = syntax_tree.children
        for member_root in root:
            if member_root.type == 'Class':
                self.in_class = True
                self.code_python += f'class {member_root.name}'
                self.class_translator(member_root)
            if member_root.type == 'Statement':
                self.code_python += '\n'
            if member_root.type == 'Function':
                if member_root.name == 'main':
                    self.code_python += f'if __name__ == "__main__"'
                    self.main_translator(member_root)
                else:
                    if self.code_python[-1] == '\n':
                        self.code_python += '\t' * self.depth
                    self.code_python += f'def {member_root.name}'
                    self.function_translator(member_root)
            if member_root.type == 'Declare':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.declare_translator(member_root)
            if member_root.type == 'Declare array':
                if self.code_python[-1] == '\n':
                    self.code_python += '\t' * self.depth
                self.declare_translator(member_root)
            if 'Semantic' in member_root.type or 'SEMANTIC' in member_root.type:
                #print(self.code_python)
                self.code_python += member_root.type
                break
            if 'Syntax' in member_root.type or 'SYNTAX' in member_root.type:
                self.code_python += member_root.type
                #print(self.code_python)
                break
            if 'Lexical' in member_root.type or 'LEXICAL' in member_root.type:
                self.code_python += member_root.type
                #print(self.code_python)
                break

if __name__ == "__main__":
    translator = Translator()
    translator.translator(parse())
    translator.translate('output_translator.py')
    codeInString = translator.code_python
    # write_output_to_file(codeInString, 'output_translator.py')
    # codeObject = compile(codeInString, 'output_translator.py', 'exec')