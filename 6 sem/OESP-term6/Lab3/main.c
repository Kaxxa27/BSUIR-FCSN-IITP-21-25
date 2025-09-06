#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "morse.h"


int main(int argc, char *argv[]) {
    if (argc != 3) {
        printf("Usage: %s <sourceFilePath> <destinationFilePath>\n", argv[0]);
        return 1;
    }

    char *sourceFilePath = argv[1];
    char *destinationFilePath = argv[2];
    FILE *sourceFile = fopen(sourceFilePath, "r");
    FILE *destinationFile = fopen(destinationFilePath, "w");

    if (sourceFile == NULL || destinationFile == NULL) {
        printf("Error opening files.\n");
        return 1;
    }

    char morse[10000]; // Maximum length of Morse code + '\0'
    memset(morse, 0, sizeof(morse)); // Initialize morse string with zeros

    while (fscanf(sourceFile, "%s", morse) != EOF) {
        printf("%s -> ", morse);
        const char character = morseToChar(morse);
        printf("%c\n\n", character);
        fputc(character, destinationFile);
    }

    printf("Morse code successfully converted to text.\n");

    fclose(sourceFile);
    fclose(destinationFile);

    return 0;
}