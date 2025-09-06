from tkinter import filedialog, Tk


def file_dialog(save=False) -> str:
    root = Tk()
    root.withdraw()
    if save:
        file = filedialog.asksaveasfilename(title="Save encrypted file as")
    else:
        file = filedialog.askopenfilename(title="Select a file for encryption")
    return file


def process_file(filename, write=False, save_text='') -> str:
    if not filename:
        print("File selection canceled.")
        return

    try:
        if not write:
            with open(filename, 'r') as input_file:
                save_text = input_file.read()
        else:
            with open(filename, 'w') as output_file:
                output_file.write(save_text)
        return save_text
    except Exception as e:
        print(f"Error: {e}")
