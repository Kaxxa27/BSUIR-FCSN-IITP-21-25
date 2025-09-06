#!/bin/bash

# Массив папок для удаления
TARGET_DIRS=("bin" "obj" "Debug" "Release" "packages", "node_modules", ".vs")

# Формируем условие для find
condition=""
for dir in "${TARGET_DIRS[@]}"; do
    if [ -n "$condition" ]; then
        condition="$condition -o -name \"$dir\""
    else
        condition="-name \"$dir\""
    fi
done

echo "Поиск папок: ${TARGET_DIRS[*]}..."
eval "folders=\$(find . -type d \( $condition \) 2>/dev/null)"

if [ -z "$folders" ]; then
    echo "Папки не найдены"
    exit 0
fi

echo "Найдены следующие папки:"
echo "$folders"
echo ""
echo "Всего папок: $(echo "$folders" | wc -l)"
read -p "Удалить все эти папки? (y/N): " confirm

if [[ $confirm == [yY] || $confirm == [yY][eE][sS] ]]; then
    echo "Удаление..."
    eval "find . -type d \( $condition \) -exec rm -rf {} +"
    echo "Готово!"
else
    echo "Отменено"
fi