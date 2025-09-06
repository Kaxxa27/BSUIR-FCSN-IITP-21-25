CC = gcc
TARGET = morse_convert
BUILD_DIR = result

$(TARGET): main.o morse.o morse_dictionary.o
	$(CC) $(BUILD_DIR)/main.o $(BUILD_DIR)/morse.o $(BUILD_DIR)/morse_dictionary.o -o $(BUILD_DIR)/$@

main.o: main.c
	$(CC) -c $< -o $(BUILD_DIR)/$@

morse_dictionary.o: morse_dictionary.c
	$(CC) -c $^ -o $(BUILD_DIR)/$@

morse.o: morse.c
	$(CC) -c $^ -o $(BUILD_DIR)/$@

clean:
	rm -f $(BUILD_DIR)/*

wclean:
	del /Q $(BUILD_DIR)\*
