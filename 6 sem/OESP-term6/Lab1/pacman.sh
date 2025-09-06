#!/bin/bash

# Инициализация переменных
map_file="map.txt"
temp_file="temp.txt"
faq_file="faq.txt"
score=0
pacman_row=2
pacman_col=2
gameover=0
cell=-1
faq=""
map=""
temp_map=""

load_faq(){
  faq=$(< "$faq_file")
}

load_map(){
  map=$(< "$map_file")
  cp "$map_file" "$temp_file"  # Создаем копию карты в temp.txt
}

load_temp_map() {
  temp_map=$(< "$temp_file")
}

save_temp_map() {
  echo "$temp_map" > "$temp_file"
}
# Windows Yellow \x1b[93m
# Linux Yellow \e[93m
# Функция отрисовки карты
draw_map() {
  clear
  echo "$faq"
  echo
  local yellow_at="\x1b[93m@\x1b[0m"  # желтый цвет для символа собачки
  echo "$temp_map" | sed "${pacman_row}s/./$yellow_at/$pacman_col"
  echo "Score: $score"
}

# Проверка возможности движения в заданном направлении
move() {
  local new_row=$((pacman_row + $1))
  local new_col=$((pacman_col + $2))
  cell=$(echo "$temp_map" | sed -n "${new_row}s/./&/p" | cut -c "$new_col")
  
  # Проверка столкновения с монеткой
  if [ "$cell" == "0" ]; then
    score=$((score + 1))
    temp_map=$(echo "$temp_map" | sed "${new_row}s/./ /$new_col")
    save_temp_map
    draw_map
  fi

  if [ "$cell" != "#" ]; then
    pacman_row=$new_row
    pacman_col=$new_col
  fi
}

game_end()
{
  if [ "$1" == "win" ]; then
    echo "Игра окончена."
    echo "Вы победили!"
  elif [ "$1" == "exit" ]; then
    echo "Игра окончена."
  else
    echo "Игра окончена. Вы проиграли."
  fi
  exit 0
}

prepare_to_game(){
  load_faq
  load_map
  load_temp_map
}

# start game

prepare_to_game
# main loop
while true; do

  draw_map

  read -s -n 1 key

  case $key in
    "w")
      move -1 0
      ;;
    "s")
      move 1 0
      ;;
    "a")
      move 0 -1
      ;;
    "d")
      move 0 1
      ;;
    $'\e')
      game_end exit
      ;;
  esac

  # Проверка столкновения с монстром
  if [ "$cell" == "*" ]; then
    draw_map
    game_end
    break
  fi

  # Проверка выигрыша
  if [ "$score" -eq 3 ]; then
    game_end win
  fi

done
