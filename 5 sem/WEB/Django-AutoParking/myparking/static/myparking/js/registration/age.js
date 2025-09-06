const ageInput = document.getElementById('age');
const regButton = document.getElementById('reg_button');

const daysOfWeek = ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'];

// Функция для проверки, является ли строка корректной датой
const isValidDate = (str) => {
  const date = new Date(str);
  return !isNaN(date);
};

// Функция для вычисления возраста пользователя в годах
const getAge = (str) => {
  const birthDate = new Date(str);
  const currentDate = new Date();
  const diff = currentDate - birthDate;
  const age = Math.floor(diff / (1000 * 60 * 60 * 24 * 365));
  return age;
};

// Функция для получения дня недели из даты
const getDayOfWeek = (str) => {
  const date = new Date(str);

  const day = date.getDay();
  return daysOfWeek[day];
};

// Добавляем обработчик события потери фокуса для поля ввода даты
ageInput.addEventListener('blur', (e) => {
  const value = e.target.value;
  if (isValidDate(value)) {
    const age = getAge(value);
    if (age >= 18) {
      const day = getDayOfWeek(value);
      alert(`Вы уже взрослый! Вам ${age} лет. Ваш в день недели: ${day}.`);
    } else {
      alert('Упс! Вам еще нет 18. Позовите родителей ^_^');
    }
  } else {
    alert('Пожалуйста, введите корректную дату в формате YYYY-MM-DD.');
  }
});

