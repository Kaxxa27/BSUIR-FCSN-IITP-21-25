import re
import zipfile
from collections import defaultdict

def analyze_text(input_file, output_file, zip_file):
    with open(input_file, 'r', encoding='utf-8') as f:
        text = f.read()

    results = []

    sentences = re.findall(r'[^.!?]+[.!?]', text)
    total_sentences = len(sentences)
    results.append(f"общее количество предложений: {total_sentences}")

    sentence_types = defaultdict(int)
    for sentence in sentences:
        if sentence.strip().endswith('.'):
            sentence_types['повествовательные'] += 1
        elif sentence.strip().endswith('?'):
            sentence_types['вопросительные'] += 1
        elif sentence.strip().endswith('!'):
            sentence_types['побудительные'] += 1

    for stype, count in sentence_types.items():
        results.append(f"количество {stype} предложений: {count}")

    total_chars = 0
    total_words = 0
    for sentence in sentences:
        words = re.findall(r'\b\w+\b', sentence)
        total_words += len(words)
        total_chars += sum(len(word) for word in words)
    
    avg_sentence_len = total_chars / total_sentences if total_sentences > 0 else 0
    results.append(f"средняя длина предложения в символах (только слова): {avg_sentence_len:.2f}")

    avg_word_len = total_chars / total_words if total_words > 0 else 0
    results.append(f"средняя длина слова в тексте: {avg_word_len:.2f}")

    smileys = re.findall(r'[:;]-*[(\[\])]+', text)
    valid_smileys = [s for s in smileys if re.fullmatch(r'[:;]-*([(\[\])])\1*', s)]
    results.append(f"количество смайликов: {len(valid_smileys)}")

    dates = re.findall(r'\b\d{4}\b', text)
    results.append(f"список дат: {', '.join(dates)}")

    vowels = 'аеёиоуыэюяaeiouy'
    consonants = 'бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxz'
    pattern = fr'\b\w*[{consonants}][{vowels}]\w\b'
    special_words = re.findall(pattern, text, re.IGNORECASE)
    results.append(f"слова с 3-й с конца согласной и предпоследней гласной: {', '.join(special_words)}")

    words = re.findall(r'\b\w+\b', text)
    results.append(f"общее количество слов: {len(words)}")

    if words:
        longest_word = max(words, key=len)
        longest_index = words.index(longest_word) + 1
        results.append(f"самое длинное слово: '{longest_word}' (позиция {longest_index})")

    odd_words = [word for i, word in enumerate(words) if i % 2 == 0]
    results.append("каждое нечетное слово: " + ', '.join(odd_words))

    with open(output_file, 'w', encoding='utf-8') as f:
        f.write('\n'.join(results))

    with zipfile.ZipFile(zip_file, 'w') as zf:
        zf.write(output_file)

    print('\n'.join(results))

class Task2:
    
    @classmethod
    def run(cls):

        input_file = "Task2/input.txt"
        output_file = "Task2/output.txt"
        zip_file = "Task2/results.zip"
        analyze_text(input_file, output_file, zip_file)