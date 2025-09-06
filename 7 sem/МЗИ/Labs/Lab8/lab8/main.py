from PIL import Image
import os


def set_bit(number, bit_position, bit_value):
    if bit_value == 1:
        return number | (1 << bit_position)
    else:
        return number & ~(1 << bit_position)


def get_message_bits(message):
    message_utf16 = message.encode("utf-16be")  # каждый символ - 2 байта
    message_bits = []
    for byte in message_utf16:
        for i in range(7, -1, -1):
            message_bits.append((byte >> i) & 1)
    return message_bits


def encode_message(image_path, message, output_path):
    with Image.open(image_path) as img:
        bmp = img.convert("RGB")
        pixels = bmp.load()
        width, height = bmp.size

        # Проверка длины сообщения в битах
        message_bits = get_message_bits(message)
        if len(message_bits) > width * height:
            print("Сообщение слишком длинное для кодирования в этом изображении.")
            return

        # Сохраняем длину сообщения в байтах
        len_file_name = f"len_{os.path.splitext(os.path.basename(output_path))[0]}.txt"
        with open(len_file_name, "w") as len_file:
            len_file.write(str(len(message_bits) // 8))

        bit_index = 0
        for y in range(height):
            for x in range(width):
                if bit_index >= len(message_bits):
                    break

                r, g, b = pixels[x, y]
                r = set_bit(r, 0, message_bits[bit_index]) #установка младшего бита красного канала тукущего пикселя
                pixels[x, y] = (r, g, b)
                bit_index += 1

        bmp.save(output_path, format="PNG")
        print("Изображение сохранено с закодированным сообщением.")


def decode_message(image_path):
    with Image.open(image_path) as bmp:
        bmp = bmp.convert("RGB")
        pixels = bmp.load()
        width, height = bmp.size

        len_file_name = f"len_{os.path.splitext(os.path.basename(image_path))[0]}.txt"
        if not os.path.exists(len_file_name):
            print(f"Файл с длиной сообщения не найден: {len_file_name}")
            return ""

        with open(len_file_name, "r") as len_file:
            message_length = int(len_file.read())  # Длина в байтах

        # Рассчитываем общее количество бит
        total_bits = message_length * 8
        message_bytes = []
        byte_value = 0 #сюда накапливается извлкаемый по каждому пикселю младший бит красного канала
        bit_index = 0

        for y in range(height):
            for x in range(width):
                if bit_index >= total_bits:
                    break

                r, _, _ = pixels[x, y]
                bit = r & 1
                byte_value = (byte_value << 1) | bit
                bit_index += 1

                if bit_index % 8 == 0: #набралось 8 битоа
                    message_bytes.append(byte_value)
                    byte_value = 0

        decoded_message = bytes(message_bytes).decode("utf-16be")
        return decoded_message


def main():
    while True:
        print("\nВыберите действие:")
        print("1 - Закодировать сообщение")
        print("2 - Декодировать сообщение")
        print("3 - Выход")

        try:
            choice = int(input())
        except ValueError:
            print("Неверный ввод, пожалуйста, введите число от 1 до 3.")
            continue

        if choice == 1:
            input_path = input("Введите путь к изображению: ")
            if not os.path.exists("message.txt"):
                print("Файл 'message.txt' не найден!")
                continue
            with open("message.txt", "r", encoding="utf-8") as message_file:
                message = message_file.read()
            output_path = input("Введите путь для сохранения закодированного изображения: ")
            encode_message(input_path, message, output_path)
            print("Сообщение успешно закодировано!")
        elif choice == 2:
            input_path = input("Введите путь к изображению для декодирования: ")
            decoded_message = decode_message(input_path)
            print("Декодированное сообщение:", decoded_message)
        elif choice == 3:
            print("Выход из программы.")
            break
        else:
            print("Неверный выбор, пожалуйста, введите число от 1 до 3.")


if __name__ == "__main__":
    main()
