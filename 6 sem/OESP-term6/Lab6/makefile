CC = gcc
TARGET = clientChat
BUILD_DIR = client

$(TARGET): client.o 
	$(CC) $(BUILD_DIR)/client.o -o $(BUILD_DIR)/$@

client.o: client.c
	$(CC) -c $< -o $(BUILD_DIR)/$@

# client.o: client.c
# 	$(CC) -c $< -o $(BUILD_DIR)/$@

clean:
	rm -f $(BUILD_DIR)/*