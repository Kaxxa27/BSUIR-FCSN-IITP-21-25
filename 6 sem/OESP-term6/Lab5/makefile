CC = gcc
TARGET = ABOBA_virus
BUILD_DIR = result

$(TARGET): main.o
	$(CC) $(BUILD_DIR)/main.o -o $(BUILD_DIR)/$@

main.o: main.c
	$(CC) -c $< -o $(BUILD_DIR)/$@

clean:
	rm -f $(BUILD_DIR)/*