#include "morse.h"
#include "morse_dictionary.h"
#include <ctype.h>
#include <string.h>

char* charToMorse(char c) {
    if (isalpha(c)) {
        c = toupper(c);
        return strdup(morseCode[c - 'A']);
    } else if (isdigit(c)) {
        return strdup(morseCode[c - '0' + 26]);
    } else if (c == ' ') {
        return strdup(" ");
    } else {
        return strdup(""); 
    }
}

char morseToChar(const char *morse) {
    for (int i = 0; i < MORSE_CODE_SIZE; ++i) {
        if (strcmp(morse, morseCode[i]) == 0) {
            if (i < 26) {
                return 'A' + i;
            } else if (i < 36) {
                return '0' + (i - 26);
            }
        }
    }
    return ' '; // If no match found, return space
}

