 // Ассоциативный массив для хранения информации о веществах
 const substances = [];

  document.getElementById('addSubstanceButton').addEventListener('click', addSubstance);

 // Функция для добавления вещества
 function addSubstance() {
     const substanceName = document.getElementById('substanceName').value.trim();
     const specificWeight = parseFloat(document.getElementById('specificWeight').value);
     const conductivity = document.getElementById('conductivity').value;

     // Проверка на пустые поля
     if (!substanceName || isNaN(specificWeight)) {
         alert('Пожалуйста, заполните все поля корректно.');
         return;
     }

     // Добавляем вещество в массив
     substances.push({
         name: substanceName,
         weight: specificWeight,
         conductivity: conductivity,
     });

     // Очищаем поля формы
     document.getElementById('substanceName').value = '';
     document.getElementById('specificWeight').value = '';
     document.getElementById('conductivity').value = 'conductor';

     // Показываем результаты
     showResults();
 }

 // Функция для вывода результатов
 function showResults() {
     const resultsDiv = document.getElementById('results');
     resultsDiv.innerHTML = '';

     // Находим удельные веса и названия всех полупроводников
     const semiconductorNames = [];
     const specificWeights = {};

     for (const substance of substances) {
         specificWeights[substance.name] = substance.weight;

         if (substance.conductivity === 'semiconductor') {
             semiconductorNames.push(substance.name);
         }
     }

     // Выводим результаты
     resultsDiv.innerHTML += '<h3>Удельные веса:</h3>';
     for (const [name, weight] of Object.entries(specificWeights)) {
         resultsDiv.innerHTML += `${name}: ${weight}<br>`;
     }

     resultsDiv.innerHTML += '<h3>Названия полупроводников:</h3>';
     resultsDiv.innerHTML += `${semiconductorNames.join(', ')}`;
 }

