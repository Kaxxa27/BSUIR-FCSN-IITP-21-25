#include "stringlib.h"

size_t My_strlen(const char *str)
{
    size_t result = 0;
    while (*str != '\0')
    {
        str++;
        ++result;
    }
    return result;
}

void *My_memcpy(void *dest, const void *src, size_t len)
{
    char* cdest = (char*)dest;
    const char* csrc = (char*)src;

    for (size_t i = 0; i < len; ++i)
    {*cdest++ = *csrc++;}

    return dest;
}

void *My_memmove(void *dest, const void *src, size_t count)
{
    if ((dest == NULL) || (src == NULL))
    {
        throw "Invalide arguments!";
    }
    char* cdest = (char*)dest;
    const char* csrc = (char*)src;
    void* ret = dest;
    if (cdest < csrc)
    {
        while (count--)
        {
            *cdest++ = *csrc++;
        }
    }
    else
    {
        cdest += count;
        csrc += count;
        while (count--)
        {
            *--cdest = *--csrc;
        }
    }
    return ret;
}

int My_memcmp(const void *buf1, const void *buf2, size_t count)
{
    if (!count)
        return(0);

    while (--count && *(char*)buf1 == *(char*)buf2) {
        buf1 = (char*)buf1 + 1;
        buf2 = (char*)buf2 + 1;
    }

    return(*((unsigned char*)buf1) - *((unsigned char*)buf2));
}

void *My_memset(void *buf, char z, size_t bytes)
{
    if (buf)
    {
        char* tmp_mem = (char*)buf;
        while (bytes--)
        {
            *tmp_mem++ = z;
        }
    }

    return buf;
}

char *My_strcpy(char *str1, const char *str2)
{
    int i = 0;
    while (*(str2 + i) != '\0')
    {
        *(str1 + i) = *(str2 + i);
        i++;
    }
    *(str1 + i) = '\0';
    return str1;
}

char *My_strncpy(char *dst, const char *src, size_t n)
{
    char* temp = dst;

    if (n > My_strlen(src))
    {
        n = My_strlen(src);
    }
    for (size_t i = 0; i < n; i++)
        *dst++ = *src++;
    return temp;
}

char *My_strcat(char *str1, char *str2)
{
    if ((str1 == NULL) || (str2 == NULL)) throw "Invalide arguments!";
    char* pt = str1;
    while (*str1 != '\0') str1++;
    while (*str2 != '\0') *str1++ = *str2++;
    *str1++ = '\0';
    return pt;
}

char *My_strncat(char *str1, char *str2, size_t n)
{
    if ((str1 == NULL) || (str2 == NULL)) throw "Invalide arguments!";
    char* pt = str1;
    while (*str1 != '\0') str1++;
    while (*str2 != '\0' || n != 0) {
        *str1++ = *str2++;
        n--;
    }
    *str1++ = '\0';
    return pt;
}

int My_strcmp(const char *s1, const char *s2)
{
    while (*s1 && *s1 == *s2) ++s1, ++s2;

    return ((unsigned char)*s1 > (unsigned char)*s2) -
            ((unsigned char)*s1 < (unsigned char)*s2);
}

int My_strncmp(const char *s1, const char *s2, size_t n)
{
    while (*s1 && *s1 == *s2 && n != 0) ++s1, ++s2, n--;

    return ((unsigned char)*s1 > (unsigned char)*s2) -
            ((unsigned char)*s1 < (unsigned char)*s2);
}

char *my_strtok(char *str, const char *delim)
{
    static char* last = 0;
    if (str) last = str;
    if ((last == 0) || (*last == 0)) return 0;
    char* c = last;
    while (strchr(delim, *c)) ++c;
    if (*c == 0) return 0;
    char* start = c;
    while (*c && (strchr(delim, *c) == 0)) ++c;
    if (*c == 0)
    {
        last = c;
        return start;
    }
    *c = 0;
    last = c + 1;
    return start;
}

