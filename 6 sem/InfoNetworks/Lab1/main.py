from FileProvider import *
from Caesar import *
from Vishiner import *

if __name__ == '__main__':

    print("Select the encryption method:")
    print("1. The Caesar")
    print("2. The Vishiner")
    choice_file = input("Enter the number of the algorithm: ")

    print("Select the [encryption or decryption]:")
    print("1. Encryption")
    print("2. Decryption")
    choice_status = input("Enter the status enc/dec: ")

    input_filename = file_dialog()
    output_filename = file_dialog()
    if choice_file == '1':
        shift = int(input("Enter the Caesar shift value: "))
        data = process_file(input_filename)
        if choice_status == '1':
            print("Original data:")
            print(data)
            enc_data = (encrypt_caesar(data, shift))
            print("Encryption data:")
            print(enc_data)
            process_file(output_filename, True, enc_data)
        elif choice_status == '2':
            print("Encryption data:")
            print(data)
            dec_data = (decrypt_caesar(data, shift))
            print("Original data:")
            print(dec_data)
            process_file(output_filename, True, dec_data)
    elif choice_file == '2':
        key = input("Enter the Vishiner key: ")
        data = process_file(input_filename)
        if choice_status == '1':
            print("Original data:")
            print(data)
            enc_data = (encrypt_vigenere(data, key))
            print("Encryption data:")
            print(enc_data)
            process_file(output_filename, True, enc_data)
        elif choice_status == '2':
            print("Encryption data:")
            print(data)
            dec_data = (decrypt_vigenere(data, key))
            print("Original data:")
            print(dec_data)
            process_file(output_filename, True, dec_data)
    else:
        print("Invalid choice. Exiting.")





